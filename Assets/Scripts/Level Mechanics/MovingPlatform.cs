using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;
    public GameObject CollisionBox;

    private Vector3 startingLocation;

    /// <summary>
    /// The delay used for both the time it take to fall and raise the platform
    /// </summary>
    public float Delay = 1.0f;

    private float timeOnPlatform;

    public GameObject WayPoint;
    public float MinDistance = 0.05f;
    public float MoveSpeed = 5f;

    private bool playerCollision = false;

    private Vector3 playerPosition;

    private void Start()
    {
        startingLocation = PlatformObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            playerPosition = other.transform.position;
            playerCollision = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            timeOnPlatform = 0;
            playerCollision = false;
        }
    }

    private void Update()
    {
        if (playerCollision)
        {
            // Small delay before the platform starts moving
            if (timeOnPlatform < Delay)
            {
                timeOnPlatform += Time.deltaTime;
            }
            else
            {
               //doe hier iets met de player position
                MovementTick(WayPoint.transform.position - PlatformObject.transform.position);
            }
        }
        else
        {
            if (startingLocation == PlatformObject.transform.position)
            {
                // Resets every variables that got changed for further usage
                timeOnPlatform = 0;
            }
            else
            {
                if (timeOnPlatform < Delay)
                {
                    timeOnPlatform += Time.deltaTime;
                }
                else
                {
                    // Moves the platform back to its starting positions back to its starting position
                    MovementTick(startingLocation - PlatformObject.transform.position);
                }
            }
        }
    }

    private void MovementTick(Vector3 direction)
    {
        if (direction.magnitude > MinDistance)
        {
            PlatformObject.transform.position += MoveSpeed * Time.deltaTime * direction.normalized;
        }
    }
}
