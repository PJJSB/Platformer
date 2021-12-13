using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;
    private Vector3 OrientationVector = new Vector3(0, 0, 0);

    public bool active;
    public float rotationSpeed = 20f;
    public Vector3 RotationVector = new Vector3(0, 0, 0);

    void Update()
    {
        if (active)
        {
            OrientationVector.x += RotationVector.x * rotationSpeed * Time.deltaTime;
            OrientationVector.y += RotationVector.y * rotationSpeed * Time.deltaTime;
            OrientationVector.z += RotationVector.z * rotationSpeed * Time.deltaTime;
            PlatformObject.transform.localRotation = Quaternion.Euler(OrientationVector.x, OrientationVector.y, OrientationVector.z);
        }
    }
}
