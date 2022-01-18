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
    public float fallDelay = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            Debug.Log(":)");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (fallDelay > 0)
        {
            Debug.Log(fallDelay.ToString());
            fallDelay -= Time.deltaTime;
        }
        else
        {
            Fall();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        fallDelay = 2.0f;
        Debug.Log("exit");
    }

    private void Fall()
    {
        if (fallDelay > 0)
        {
            fallDelay -= Time.deltaTime;
        }
        else
        {
            FallDirection += FallAccel * Time.deltaTime;
            FallingObject.transform.position += FallDirection * Time.deltaTime;
        }
    }
}
