using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    new private Transform camera;
    private Camera cam;
    private bool isLocked = false;

    private void Start()
    {
        camera = GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    Vector3 velocity3 = Vector3.zero;
    private void LateUpdate()
    {
        if (isLocked) return;
        camera.position = new Vector3(Vector3.SmoothDamp(camera.transform.position, target.transform.position, ref velocity3, smoothSpeed).x, camera.position.y, -10);
    }

    /// <summary> Change setting for camera. </summary>
    public void SetCameraSettings(float fov = -1, bool lockMode = false, float posX = 0, float posY = 0, float camAngle = 0)
    {
        if (fov != -1) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, 0.05f);

        Vector3 posCurrent = new Vector3(camera.position.x, camera.position.y, -10);
        Vector3 posNew = new Vector3(posX, posY, -10);

        if (posX != 0 || posY != 0) camera.transform.position = Vector3.Lerp(posCurrent, posNew, 0.05f);

        Quaternion rotCurrent = Quaternion.Euler(0, 0, camera.rotation.eulerAngles.z);
        Quaternion rotNew = Quaternion.Euler(0, 0, camAngle);

        if (camAngle != 0) camera.rotation = Quaternion.Lerp(rotCurrent, rotNew, 0.05f);

        isLocked = lockMode;
    }

    /// <summary> You can get info about cam </summary> <returns> CameraSettings object</returns>
    public CameraSettings GetSettings()
    {
        CameraSettings set = new();
        set.fov = cam.fieldOfView;
        set.lockMode = isLocked;
        set.position = camera.position;
        set.camAngle = camera.rotation.eulerAngles.z;
        return set;
    }

    public struct CameraSettings
    {
        public float fov;
        public bool lockMode;
        public Vector2 position;
        public float camAngle;
    }
}
