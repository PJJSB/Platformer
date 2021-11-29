using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;

    public bool active;
    public float rotationSpeed;

    void Update()
    {
        if (active)
        {
            /*Rotate around some axis

            PlatformObject.transform.localRotation = Quaternion.Euler(x, y , z)


            */
        }
    }
}
