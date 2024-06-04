using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool fire;
		public bool selectSpell;
		public bool selectElement1;
		public bool selectElement2;
		public bool selectElement3;
		public bool selectElement4;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnFire(InputValue value)
		{
			FireInput(value.isPressed);
		}

		public void OnSelectSpell(InputValue value)
		{
			SpellSelectInput(value.isPressed);
		}

		public void OnSelectSpell1(InputValue value)
		{
			SelectElement1Input(value.isPressed);
		}

		public void OnSelectSpell2(InputValue value)
		{
			SelectElement2Input(value.isPressed);
		}

		public void OnSelectSpell3(InputValue value)
		{
			SelectElement3Input(value.isPressed);
		}

		public void OnSelectSpell4(InputValue value)
		{
			SelectElement4Input(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void FireInput(bool newFireState)
		{
			fire = newFireState;
		}

		public void SpellSelectInput(bool newSpellSelectState)
		{
			selectSpell = newSpellSelectState;
		}

		public void SelectElement1Input(bool newSpellSelectState)
		{
			selectElement1 = newSpellSelectState;
		}

		public void SelectElement2Input(bool newSpellSelectState)
		{
			selectElement2 = newSpellSelectState;
		}

		public void SelectElement3Input(bool newSpellSelectState)
		{
			selectElement3 = newSpellSelectState;
		}

		public void SelectElement4Input(bool newSpellSelectState)
		{
			selectElement4 = newSpellSelectState;
		}

		private void OnApplicationFocus()
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}