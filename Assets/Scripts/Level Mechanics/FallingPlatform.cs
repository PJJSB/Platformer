using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    /// <summary>
    /// This object checks whether the player is on top of the platform which will fall
    /// </summary>
    public GameObject CollisionBox;
    public GameObject FallingObject;

    public Vector3 FallAccel = new Vector3(0, -1, 0);
    public Vector3 FallDirection = new Vector3(0, 0, 0);
    
    /// <summary>
    /// The delay used for both the time it take to fall and raise the platform
    /// </summary>
    public float Delay = 1.0f;

    private float timeOnPlatform;
    private float startingHeight;

    private Vector3 startingFallAccel;
    private Vector3 startingFallDirection;

    private bool isGoingUp = false;

    private void Start()
    {
        startingHeight = FallingObject.transform.position.y;
        startingFallAccel = FallAccel;
        startingFallDirection = FallDirection;
    }

    private void OnTriggerStay(Collider other)
    {
        // Delay period before falling
        if (timeOnPlatform < Delay)
        {
            timeOnPlatform += Time.deltaTime;
        }
        else
        {
            // Lets the platform drop 
            FallDirection += FallAccel * Time.deltaTime;
            FallingObject.transform.position += FallDirection * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FallDirection = startingFallDirection;
        FallAccel = startingFallAccel;

        timeOnPlatform = 0;

        isGoingUp = true;
    }

    private void Update()
    {
        if (isGoingUp)
        {
            //vgm werkt dit nog niet
            Debug.Log(timeOnPlatform.ToString());
            if (timeOnPlatform < Delay)
            {
                timeOnPlatform += Time.deltaTime;
            }
            else
            {
                if (FallingObject.transform.position.y <= startingHeight)
                {
                    // Raises the platform up to its original height
                    FallDirection += FallAccel * Time.deltaTime;
                    FallingObject.transform.position -= FallDirection * Time.deltaTime;
                }
                else
                {
                    // The endpoint of all of this code, here every variable that got changed along the way is reset and ready for the next collision
                    FallDirection = startingFallDirection;
                    FallAccel = startingFallAccel;
                    timeOnPlatform = 0;
                    isGoingUp = false;
                }
            }
        }
    }
}
