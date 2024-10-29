using UnityEngine;
using Unity.Cinemachine;
public class MgCam : MonoBehaviour
{
    [SerializeField] CinemachineRotationComposer camOffset;
    [SerializeField] CinemachineFollowZoom ZoomCam;
    [SerializeField] InputManager InputManager;
    [SerializeField] GameObject firstController;
    [SerializeField] GameObject thirdController;
    public CinemachineInputAxisController FirstCamInput;
    public CinemachinePanTilt firstPantilt;
    private void Awake()
    {
        firstController.SetActive(true);
        thirdController.SetActive(false);
    }
    private void Update()
    {
        firstController.SetActive(InputManager.isFirstPerson);
        thirdController.SetActive(!InputManager.isFirstPerson);
    }
}
