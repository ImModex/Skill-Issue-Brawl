using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using Assets.Scripts.Enums;

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(PlayerInput))]
	public class ThirdPersonController : MonoBehaviour
	{
		private SpellManagerScript spellManager;
		private Statscript statschanges;

		/// <summary>
		/// Buttons that were pressed in game.
		/// For element combinations.
		/// </summary>
		public List<Button> SelectedButtons = new();

		/// <summary>
		/// Buttons mapped to the elements, that were selected by the player at the start of the game.
		/// </summary>
		public Dictionary<Button, Element> SelectedElements = new();


		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 8.0f;

		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;

		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		public AudioClip[] FootstepAudioClips;
		[Range(0, 1)] public float FootstepAudioVolume = 0.5f;

		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;

		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;

		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.28f;

		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		// player
		private float _speed;
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _verticalVelocity;

		// animation IDs
		private int _animIDSpeed;
		private int _animIDGrounded;
		private int _animIDMotionSpeed;
		private int _animIDFire;

		private PlayerInput _playerInput;
		private Animator _animator;
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private bool _hasAnimator;

		private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

		private void Awake()
		{
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			spellManager = GameObject.Find("SpellManager").GetComponent<SpellManagerScript>();
			statschanges = GetComponent<Statscript>();
			_hasAnimator = TryGetComponent(out _animator);
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
			_playerInput = GetComponent<PlayerInput>();

			AssignAnimationIDs();
			AssignElementMapping();
		}

		private void Update()
		{
			if (_mainCamera == null) _mainCamera = Camera.main.gameObject;
			
			GroundedCheck();
			Move();
			ClearButtonSelection();
			HandleElementSelection();
			Fire();
		}

		private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
			_animIDFire = Animator.StringToHash("Fire");
		}

		private void AssignElementMapping()
		{
			// TODO: change once main menu element selection has been implemented
			SelectedElements.Add(Button.North, Element.Fire);
			SelectedElements.Add(Button.East, Element.Water);
			SelectedElements.Add(Button.South, Element.Earth);
			SelectedElements.Add(Button.West, Element.Air);
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new(transform.position.x, transform.position.y - GroundedOffset,
				transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
				QueryTriggerInteraction.Ignore);

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetBool(_animIDGrounded, Grounded);
			}
		}

		private void Fire()
		{
			if (statschanges.Stunned)
			{
				return;
			}

			if (_input.fire)
			{
				_input.fire = false;

				if (SelectedButtons.Count < 2)
				{
					Debug.Log("<2 elements selected -> no shot");
					return;
				}

				_animator.SetTrigger(_animIDFire);
				spellManager.Cast(SelectedElements[SelectedButtons[0]], SelectedElements[SelectedButtons[1]], gameObject);
				Debug.Log($"woohoo i did a shoot with {SelectedElements[SelectedButtons[0]]} and {SelectedElements[SelectedButtons[1]]}. (button selection cleared)");
				SelectedButtons.Clear();
			}
		}

		private void HandleElementSelection()
		{
			if (_input.selectElement1)
			{
				Debug.Log($"u selected {SelectedElements[Button.North]}. SUBLIME.");
				_input.selectElement1 = false;
				AddElementToSelection(Button.North);
			}

			if (_input.selectElement2)
			{
				Debug.Log($"u selected {SelectedElements[Button.East]}. SUBLIME.");
				_input.selectElement2 = false;
				AddElementToSelection(Button.East);
			}

			if (_input.selectElement3)
			{
				Debug.Log($"u selected {SelectedElements[Button.South]}. SUBLIME.");
				_input.selectElement3 = false;
				AddElementToSelection(Button.South);
			}

			if (_input.selectElement4)
			{
				Debug.Log($"u selected {SelectedElements[Button.West]}. SUBLIME.");
				_input.selectElement4 = false;
				AddElementToSelection(Button.West);
			}
		}

		private void ClearButtonSelection()
		{
			if (_input.clearButtonSelection)
			{
				SelectedButtons.Clear();
				Debug.Log($"Button selection has been cleared: {SelectedButtons.Count == 0}");
				_input.clearButtonSelection = false;
			}
		}

		private void AddElementToSelection(Button selectedElement)
		{
			if (SelectedButtons.Count >= 2)
			{
				Debug.Log($"nuh uh. only 2 elements can be selected. current selection {SelectedElements[SelectedButtons[0]]} and {SelectedElements[SelectedButtons[1]]}");
				return;
			}
			SelectedButtons.Add(selectedElement);
		}


		private void Move()
		{
			if (statschanges.Stunned)
			{
				//Animator set to dizzy or smth
				return;
			}

			float targetSpeed = MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero)
			{
				targetSpeed = 0.0f;
			}

			targetSpeed *= statschanges.moveSpeedMultiplyer;
			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset ||
				currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
					Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}


			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
			if (_animationBlend < 0.01f)
			{
				_animationBlend = 0f;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
			Vector3 lookDirection = new Vector3(_input.look.x, 0.0f, _input.look.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.look != Vector2.zero)
			{
				float lookRotation = (Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg) +
								  _mainCamera.transform.eulerAngles.y;

				_targetRotation = (Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg) +
								  _mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookRotation, ref _rotationVelocity,
					RotationSmoothTime);

				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}
			else if (_input.move != Vector2.zero)
			{
				_targetRotation = (Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg) +
								  _mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
					RotationSmoothTime);

				// rotate to face input direction relative to camera position
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}


			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;


			// move the player

			_ = _controller.Move((targetDirection.normalized * (_speed * Time.deltaTime/* * statschanges.moveSpeedMultiplyer*/)) +
							 (new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime));

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}
		}

		private void OnFootstep(AnimationEvent animationEvent)
		{
			if (animationEvent.animatorClipInfo.weight > 0.5f)
			{
				if (FootstepAudioClips.Length > 0)
				{
					int index = Random.Range(0, FootstepAudioClips.Length);
					AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
				}
			}
		}
	}
}