using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;

    public bool isActive = true;
    public Vector3 fallAccel = new Vector3(0, -1, 0);
    public Vector3 fallDirection = new Vector3(0, 0, 0);
    public float fallDelay = 5.0f;

    void Update()
    {
        if (isActive)
        {
            if (fallDelay > 0)
            {
                fallDelay -= Time.deltaTime;
            }
            else
            {
                fallDirection += fallAccel * Time.deltaTime;
                PlatformObject.transform.position += fallDirection * Time.deltaTime;
            }
        }
    }
}
