using UnityEngine;
using Unity.Cinemachine;
public class MgCam : MonoBehaviour
{
    [SerializeField] InputManager InputManager;
    [SerializeField] GameObject firstController;
    [SerializeField] GameObject thirdController;
    public CinemachineInputAxisController thirdCamInput;
    private void Awake()
    {
        firstController.SetActive(true);
        thirdController.SetActive(false);
        
        LockCursor(InputManager.cursorInputForLook);
    }
    private void Update()
    {
        firstController.SetActive(InputManager.isFirstPerson);
        thirdController.SetActive(!InputManager.isFirstPerson);
 
    }
    public void LockCursor(bool lockCursor)
    {
        
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;
            InputManager.cursorInputForLook = true;
            thirdCamInput.enabled = true;
        }
        else
        {
            thirdCamInput.enabled = false;
            Cursor.lockState = CursorLockMode.None;   
            Cursor.visible = true;
            InputManager.cursorInputForLook = false;
        }
    }
}
