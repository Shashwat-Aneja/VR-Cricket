using UnityEngine;

public class VRHeadTracker : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform vrCamera; // Assign the VR camera (head)

    private bool gyroEnabled = false;
    private Gyroscope gyro;

    private Quaternion rotFix;

    void Start()
    {
        gyroEnabled = EnableGyro();
    }

    bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            // Orientation fix for typical mobile VR direction
            rotFix = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Smartphone gyroscope rotations + fix for VR orientation
            vrCamera.localRotation = gyro.attitude * rotFix;
        }
    }
}
