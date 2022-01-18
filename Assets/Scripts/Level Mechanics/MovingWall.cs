using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public GameObject PlatformObject;
    public GameObject CollisionObject;

    public float MovementSpeed = 10f;
    public float WidthLimit = 3f;

    private float startingWidth;
    private bool hasExtended;
    private float timeOnPlatform = 0.00f;

    public float Delay = 1.5f;

    private void Start()
    {
        startingWidth = PlatformObject.transform.localScale.x;
        hasExtended = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // Delay period before extending
        if (timeOnPlatform < Delay)
        {
            timeOnPlatform += Time.deltaTime;
        }
        else
        {
            if (PlatformObject.transform.localScale.x < WidthLimit)
            {
                // Lets the platform extend 
                PlatformObject.transform.localScale += new Vector3(MovementSpeed, 0, 0) * Time.deltaTime;
                CollisionObject.transform.localPosition -= new Vector3(MovementSpeed, 0, 0) * Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timeOnPlatform = 0;
        hasExtended = true;
    }

    void Update()
    {
        if (hasExtended && PlatformObject.transform.localScale.x > startingWidth)
        {
            // Delay period before reclining
            if (timeOnPlatform < Delay)
            {
                timeOnPlatform += Time.deltaTime;
            }
            else
            {
                // Lets the platform shrink back down until its back in its original place
                PlatformObject.transform.localScale -= new Vector3(MovementSpeed / 10, 0, 0) * Time.deltaTime;
                CollisionObject.transform.localScale += new Vector3(MovementSpeed / 10, 0, 0) * Time.deltaTime;
            }
        }
    }
}
