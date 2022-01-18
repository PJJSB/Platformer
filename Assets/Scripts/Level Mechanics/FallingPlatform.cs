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

    private float timeOnPlatform = 0f;
    private bool isGoingUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            Debug.Log(":)");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (timeOnPlatform < Delay)
        {
            timeOnPlatform += Time.deltaTime;
        }
        else
        {
            //Debug.Log(timeOnPlatform.ToString());
            Fall();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("up you go");
        isGoingUp = true;
    }

    private void Update()
    {
        if (isGoingUp)
        {

        }
    }

    private void Fall()
    {
        FallDirection += FallAccel * Time.deltaTime;
        FallingObject.transform.position += FallDirection * Time.deltaTime;
    }
}
