using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		// All of these are triggered by the player. The player has an input component attached to it.

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool interact;
		public bool FreeMouse;
		public bool escp;
		public bool pointer;
		public bool LeftClick;
		public int count=0;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private Pause pause;
		private FirstPersonController firstPersonController;
		private ThirdPersonController thirdPersonController;
		void Start(){
			pause = FindObjectOfType<Pause>();
			firstPersonController = FindObjectOfType<FirstPersonController>();
			thirdPersonController = FindObjectOfType<ThirdPersonController>();
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
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

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}

		public void OnEscape(InputValue value)
		{
			if(pause.GameIsPaused){
				pause.resume();
			}else{
				pause.pause();
			}
		}

		public void OnPointer(InputValue value)
		{
			PointerInput(value.isPressed);
		}

		public void OnLeftClick(){

		}

		// When C is pressed, it frees the cursor and makes it visible. C can be pressed again to lock it.
		// This is different from pausing the game, as it doesn't set the timescale of the simulation to 0.
		public void OnFreeMouse(){
			if(cursorInputForLook == true){
				cursorInputForLook = false;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				firstPersonController.isCameraRotationEnabled = false;
			}else{
				cursorInputForLook = true;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				firstPersonController.isCameraRotationEnabled = true;
			}
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

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}

		public void OnApplicationFocus(bool hasFocus)
		{
			cursorInputForLook = true;
			SetCursorState(cursorLocked);
		}

		public void lockMouse(){
			cursorInputForLook = true;
			SetCursorState(true);
		}

		public void PointerInput(bool newPointerState)
		{

		}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}
