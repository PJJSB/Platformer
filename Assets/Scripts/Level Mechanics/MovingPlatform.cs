using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;
    public GameObject CollisionBox;

    private Vector3 startingLocation;

    /// <summary>
    /// The delay used for both the time it take to fall and raise the platform
    /// </summary>
    public float Delay = 1f;

    private float timeOnPlatform;

    public Vector3 WayPoint;
    public float MinDistance = 0.05f;
    public float MoveSpeed = 5f;

    private bool playerCollision = false;
    private CharacterController _characterController;

    private void Start()
    {
        startingLocation = PlatformObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            _characterController = other.GetComponent<CharacterController>();
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
                var direction = WayPoint - PlatformObject.transform.position;
                MovementTick(direction);
                direction.y = 0;
                
                if (direction.sqrMagnitude > MinDistance * MinDistance)
                {
                    _characterController.SimpleMove(direction.normalized * MoveSpeed);
                }
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
