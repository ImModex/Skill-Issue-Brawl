using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerControllerScript : MonoBehaviour
{
	private SpellManagerScript spellManager;

	/// <summary>
	/// Buttons that were pressed in game.
	/// For element combinations.
	/// </summary>
	public List<Button> SelectedButtons = new();
	/// <summary>
	/// Buttons mapped to the elements, that were selected by the player at the start of the game.
	/// </summary>
	public Dictionary<Button, Element> SelectedElements = new();

	public float MoveSpeed = 2.0f;
	public float SprintSpeed = 5.335f;
	public float SpeedChangeRate = 10.0f;

	[Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;

	public AudioClip LandingAudioClip;
	public AudioClip[] FootstepAudioClips;
	[Range(0, 1)] public float FootstepAudioVolume = 0.5f;

	public bool Grounded = true;
	public float GroundedOffset = -0.14f;
	public float GroundedRadius = 0.28f;
	public LayerMask GroundLayers;

	public GameObject CinemachineCameraTarget;
	public float TopClamp = 70.0f;
	public float BottomClamp = -30.0f;
	public float CameraAngleOverride = 0.0f;
	public bool LockCameraPosition = false;

	// cinemachine
	private float _cinemachineTargetYaw;
	private float _cinemachineTargetPitch;

	// player
	private float _speed;
	private float _animationBlend;
	private float _targetRotation = 0.0f;
	private float _rotationVelocity;
	private float _verticalVelocity;
	private Vector2 _moveInput;

	// animation IDs
	private int _animIDSpeed;
	private int _animIDGrounded;
	private int _animIDMotionSpeed;
	private int _animIDFire;

#if ENABLE_INPUT_SYSTEM
	private PlayerInput _playerInput;
#endif
	private Animator _animator;
	private CharacterController _controller;
	private GameObject _mainCamera;

	private const float _threshold = 0.01f;

	private bool _hasAnimator;

	private bool IsCurrentDeviceMouse =>
#if ENABLE_INPUT_SYSTEM
			_playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif


	private void Awake()
	{
		if (_mainCamera == null)
		{
			_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}
	}

	private void Start()
	{
		_cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

		spellManager = GameObject.Find("SpellManager").GetComponent<SpellManagerScript>();

		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController>();
#if ENABLE_INPUT_SYSTEM
		_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

		AssignAnimationIDs();
		AssignElementMapping();
	}

	private void Update()
	{
		GroundedCheck();
		Move();
	}

	private void AssignAnimationIDs()
	{
		_animIDSpeed = Animator.StringToHash("Speed");
		_animIDGrounded = Animator.StringToHash("Grounded");
		_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
		_animIDFire = Animator.StringToHash("Fire");

		Debug.Log($"_animIDGrounded: {_animIDGrounded}");
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

	private void OnFire()
	{
		if (SelectedButtons.Count < 2)
		{
			Debug.Log("< 2 elements selected. no spell.");
			return;
		}

		spellManager.Cast(SelectedElements[SelectedButtons[0]], SelectedElements[SelectedButtons[1]], gameObject);
		Debug.Log($"woohoo i did a shoot with {SelectedElements[SelectedButtons[0]]} and {SelectedElements[SelectedButtons[1]]}. (button selection cleared)");
		SelectedButtons.Clear();
	}

	private void OnSelectElement1()
	{
		Debug.Log($"u selected {SelectedElements[Button.North]}. SUBLIME.");
		AddElementToSelection(Button.North);
	}

	private void OnSelectElement2()
	{
		Debug.Log($"u selected {SelectedElements[Button.East]}. SUBLIME.");
		AddElementToSelection(Button.East);
	}

	private void OnSelectElement3()
	{
		Debug.Log($"u selected {SelectedElements[Button.South]}. SUBLIME.");
		AddElementToSelection(Button.South);
	}

	private void OnSelectElement4()
	{
		Debug.Log($"u selected {SelectedElements[Button.West]}. SUBLIME.");
		AddElementToSelection(Button.West);
	}

	private void AddElementToSelection(Button selectedElement)
	{
		if (SelectedButtons.Count >= 2)
		{
			Debug.Log($"nuh uh. only 2 elements can be selected. current selection {SelectedButtons[0]} and {SelectedButtons[1]}");
			return;
		}
		SelectedButtons.Add(selectedElement);
	}

	private void OnMove(InputValue value)
	{
		_moveInput = value.Get<Vector2>();
	}

	private void Move()
	{
		Vector2 moveInput = _moveInput;
		float targetSpeed = MoveSpeed;

		if (moveInput == Vector2.zero)
		{
			targetSpeed = 0.0f;
		}

		float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

		float speedOffset = 0.1f;
		float inputMagnitude = 1f;

		if (currentHorizontalSpeed < targetSpeed - speedOffset ||
			currentHorizontalSpeed > targetSpeed + speedOffset)
		{
			_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
				Time.deltaTime * SpeedChangeRate);

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

		Vector3 inputDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

		if (moveInput != Vector2.zero)
		{
			_targetRotation = (Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg) +
							  _mainCamera.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
				RotationSmoothTime);

			transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		}

		Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

		_ = _controller.Move((targetDirection.normalized * (_speed * Time.deltaTime)) +
						 (new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime));

		if (_hasAnimator)
		{
			_animator.SetFloat(_animIDSpeed, _animationBlend);
			_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
		}
	}

	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f)
		{
			lfAngle += 360f;
		}

		if (lfAngle > 360f)
		{
			lfAngle -= 360f;
		}

		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	private void OnDrawGizmosSelected()
	{
		Color transparentGreen = new(0.0f, 1.0f, 0.0f, 0.35f);
		Color transparentRed = new(1.0f, 0.0f, 0.0f, 0.35f);

		Gizmos.color = Grounded ? transparentGreen : transparentRed;

		// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
		Gizmos.DrawSphere(
			new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
			GroundedRadius);
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
