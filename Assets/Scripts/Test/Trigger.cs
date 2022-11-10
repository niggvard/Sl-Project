using System.Collections;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private CameraMove cm;
    [SerializeField] private PlayerController pc;
    private bool active = false;
    void OnTriggerEnter2D()
    {
        StartCoroutine(corutine());
    }
    private void LateUpdate()
    {
        if (active)
        {
            cm.SetCameraSettings(50, true, 5, 0);
            pc.isMoving = false;
        }
        else 
        {
            pc.isMoving = true;
            cm.SetCameraSettings(32, false, 0, 0);
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
