using UnityEngine;

public class SensitivityController : MonoBehaviour
{
    public Player_Movement PlayerController;
    public float CurSensitivity;
    public float MinSensitivity = 1;
    public float MaxSensitivity = 10;
    [Space(5)]
    public KeyCode IncKeyCode;
    public KeyCode DecKeyCode;

    void Start()
    {
        CurSensitivity = PlayerPrefs.GetFloat("Sens",5f);
        PlayerController.lookSpeed = CurSensitivity;
    }

    void Update()
    {
        if(Input.GetKeyDown(IncKeyCode))
        {
            CurSensitivity += 1;
            CurSensitivity = Mathf.Clamp(CurSensitivity,MinSensitivity,MaxSensitivity);
            PlayerController.lookSpeed = CurSensitivity;
            PlayerPrefs.SetFloat("Sens",CurSensitivity);
        }
        else if(Input.GetKeyDown(DecKeyCode))
        {
            CurSensitivity -= 1;
            CurSensitivity = Mathf.Clamp(CurSensitivity,MinSensitivity,MaxSensitivity);
            PlayerController.lookSpeed = CurSensitivity;
            PlayerPrefs.SetFloat("Sens",CurSensitivity);
        }
        
    }
}
