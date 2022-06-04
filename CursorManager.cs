using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public CursorLockMode LockMode = CursorLockMode.Locked;
    public bool Visible = false;

    void Start()
    {
        Cursor.lockState = LockMode;
        Cursor.visible = Visible;
    }

}
