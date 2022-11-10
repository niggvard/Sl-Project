using System.Collections;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private CameraMove cm;
    [SerializeField] private PlayerController pc;
    [SerializeField] private float cms;
    private bool active = false;
    void OnTriggerEnter2D()
    {
        StartCoroutine(corutine());
    }
    private void LateUpdate()
    {
        if (active)
        {
            cm.SetCameraSettings(50, true, 5, 0, 0, cms);
            pc.isMoving = false;
        }
        else 
        {
            pc.isMoving = true;
            cm.SetCameraSettings(32, false, 0, 0, 0, cms);
        }
    }
    IEnumerator corutine() 
    {
        active = true;

        yield return new WaitForSeconds(3);

        active = false;

        yield return null;  
    }   
}
