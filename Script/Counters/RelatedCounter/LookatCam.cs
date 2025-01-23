using UnityEngine;

public class LookatCam : MonoBehaviour
{
    private enum Mode{
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraInverted,
    }

    [SerializeField] private Mode mode;
    private void LateUpdate() {
        switch(mode){
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCam = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCam);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
        //transform.LookAt(Camera.main.transform);
    }
}
