using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;
    public GameObject CollisionBox;
    
    public GameObject[] WayPoints = new GameObject[2];
    public int WayPointIndex = 0;
    public float MinDistance = 0.05f;
    public float MoveSpeed = 5f;

    private bool playerCollision = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "character")
        {
            playerCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "character")
        {
            playerCollision = false;
        }
    }

    private void Update()
    {
        if (playerCollision)
        {
            Debug.Log("on it");
            Vector3 direction = WayPoints[WayPointIndex].transform.position - PlatformObject.transform.position;

            if (direction.magnitude < MinDistance)
            {
                WayPointIndex++;

                if (WayPointIndex == WayPoints.Length)
                {
                    WayPointIndex = 0;
                }
            }
            else
            {
                PlatformObject.transform.position += MoveSpeed * Time.deltaTime * direction.normalized;
            }
        }
    }
}
