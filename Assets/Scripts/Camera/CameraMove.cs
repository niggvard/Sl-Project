using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private AnimationCurve smooth; // animation curve lerp factor
    new private Transform camera;
    private Camera cam;
    private bool isLocked = false;

    private void Start()
    {
        camera = GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        if (isLocked) return;
        SetCameraSettings(camAngle : Mathf.Lerp(GetSettings().camAngle, 14, smooth.Evaluate(0.05f)), fov : Mathf.Lerp(GetSettings().fov, 25, smooth.Evaluate(0.05f))); 
        camera.position = new Vector3(Vector3.SmoothDamp(camera.transform.position, target.transform.position, ref velocity, smoothSpeed).x, camera.position.y, camera.position.z);
    }

    /// <summary> Change setting for camera. </summary>
    public void SetCameraSettings(float fov = -1, bool lockMode = false, float posX = 0, float posY = 0, float camAngle = 0)
    {
        if (fov != -1) cam.fieldOfView = fov;
        if (posX != 0) camera.position = new Vector2(posX, camera.position.y);
        if (posY != 0) camera.position = new Vector2(camera.position.x, posY);
        if (camAngle != 0) camera.rotation = Quaternion.Euler(0, 0, camAngle);

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
