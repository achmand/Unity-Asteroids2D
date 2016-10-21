using UnityEngine;

public sealed class KeyboardInputManager : MonoBehaviour {

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
        get { return Input.GetAxis("Mouse X"); }
    }

    public bool GetKeyPress(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public bool GetKeyPress(string keyCode)
    {
        return Input.GetButton(keyCode);
    }
}
