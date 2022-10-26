using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    new private Transform camera;

    private void Start()
    {
        camera = GetComponent<Transform>();
    }

    Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        camera.position = new Vector3(Vector3.SmoothDamp(camera.transform.position, target.transform.position, ref velocity, smoothSpeed).x, 0, camera.position.z);
    }
}
