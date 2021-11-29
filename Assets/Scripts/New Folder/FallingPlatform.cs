using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;

    public bool isFalling;
    public Vector3 fallAccel;
    public Vector3 position;

    void Update()
    {
        if (isFalling)
        {
            /*Fall in some direction

            position += fallAccel * Time.deltaTime;
            PlatformObject.transform.localRotation = Quaternion.Euler(x, y , z)


            */
        }
    }
}
