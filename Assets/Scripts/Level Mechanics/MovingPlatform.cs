using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;

    public bool active = false;
    public GameObject[] WayPoints = new GameObject[2];
    public int WayPointIndex = 0;
    public float MinDistance = .05f;
    public float MoveSpeed = 5f;

    void Start()
    {
    }

    void Update()
    {
        if (active)
        {
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

    public void ToggleMoving()
    {
        active = !active;
    }
}
