using Assets.Scripts.Player;
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
    private bool isGoingDown = false;

    private Vector3 lastDirection;
    private CharacterController _characterController;
    private PlayerStats playerStats;
    private float startingDeaths;
    private float difference;

    private void Start()
    {
        startingHeight = FallingObject.transform.position.y;
        startingFallAccel = FallAccel;
        startingFallDirection = FallDirection;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        isGoingDown = true;
        isGoingUp = false;
        _characterController = other.GetComponent<CharacterController>();
        playerStats = other.GetComponent<PlayerStats>();
        startingDeaths = other.GetComponent<PlayerStats>().deathCount;

    }

    private void OnTriggerExit(Collider other)
    {
        FallDirection = startingFallDirection;
        FallAccel = startingFallAccel;

        timeOnPlatform = 0;
        isGoingUp = true;
        isGoingDown = false;
    }

    private void FixedUpdate()
    {
        // Moving downwards
        if (isGoingDown)
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
                _characterController.transform.position += FallDirection * Time.deltaTime;
            }
            
            // If object somehow gets stuck
            if ((lastDirection == FallDirection && FallDirection.magnitude > 0) || playerStats.deathCount > startingDeaths)
            {
                isGoingDown = false;
                isGoingUp = true;
            }

            lastDirection = FallDirection;
        }
        
        // Going up
        if (isGoingUp)
        {
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
