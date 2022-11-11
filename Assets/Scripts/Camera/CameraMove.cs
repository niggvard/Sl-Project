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
    public void SetCameraSettings(float fov = -1, bool lockMode = false, float posX = 0, float posY = 0, float camAngle = 0, float CamSmoother = 0f)
    {
        if (fov != -1) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, CamSmoother * Time.deltaTime);

        if (posX != 0 || posY != 0) camera.transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, transform.position.z), CamSmoother * Time.deltaTime);

        if (camAngle != 0) camera.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, camAngle), CamSmoother * Time.deltaTime);

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
