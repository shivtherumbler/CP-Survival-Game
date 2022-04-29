using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
        #region Singleton
		public static StarterAssetsInputs Instance { get; private set; }

        private void Awake()
        {
			if (Instance == null)
				Instance = this;
			else
            {
				Destroy(gameObject);
            }
        }
        #endregion

        [Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

		public Button collectButton;

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void EnableCollection(bool enable)
		{
			Debug.Log("Enabling");
			collectButton.gameObject.SetActive(true);
			collectButton.interactable = true;
		}

		public void DisableCollection(bool enable)
		{
			Debug.Log("Disabling");
			collectButton.interactable = false;
			collectButton.gameObject.SetActive(false);
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

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void EnableCollection(bool enable)
        {
			Debug.Log("Enabling");
			collectButton.gameObject.SetActive(true);
			collectButton.interactable = true;
        }

		public void DisableCollection(bool enable)
		{
			Debug.Log("Disabling");
			collectButton.interactable = false;
			collectButton.gameObject.SetActive(false);
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
#else
		// old input sys if we do decide to have it (most likely wont)...
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

		public void CollectItem(Interactable interactable)
        {
			interactable.pickup = true;
			interactable.Collect();
        }

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}