using UnityEngine;

namespace Assets.Scripts
{
    public sealed class KeyboardInputManager : MonoBehaviour
    {
        private bool rightMouseClickHold;

        public float HorizontalAxis
        {
            get { return Input.GetAxis("Horizontal"); }
        }
        public float VeticalAxis
        {
            get { return Input.GetAxis("Vertical"); }
        }

        public float MouseX
        {
            get { return Input.GetAxis("Mouse Y"); }
        }

        public bool GetKeyPress(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }

        private bool RightMouseClick()
        {
            return Input.GetMouseButtonDown(0);
        }

        private bool RightMouseUp()
        {
            return Input.GetMouseButtonUp(0);
        }

        public bool RightClickHold()
        {
            if (RightMouseClick())
            {
                rightMouseClickHold = true;
            }
            if (RightMouseUp())
            {
                rightMouseClickHold = false;
            }
            return rightMouseClickHold;
        }

        public bool GetKeyPress(string keyCode)
        {
            return Input.GetButton(keyCode);
        }
    }
}
