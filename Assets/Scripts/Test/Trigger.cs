using System.Collections;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private CameraMove cm;
    [SerializeField] private PlayerController pc;
    [SerializeField] private float cms;
    private bool active = false;
    void OnTriggerStay2D()
    {
        cm.SetCameraSettings(lockMode: true, posX:0, posY:0, CamSmoother: 1);
    }
    void OnTriggerExit2D()
    {
        cm.SetCameraSettings(lockMode: false, posX: 0, posY: 0, CamSmoother: 1);
    }
}