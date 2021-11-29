using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject PlatformObject;

    public bool active;
    public GameObject[] WayPoints;
    public int WayPointIndex;
    public float MinDistance;
    public float MoveSpeed;

    void Start()
    {
        WayPoints = new GameObject[2];
        WayPointIndex = 0;
        MinDistance = .1f;
        MoveSpeed = 1f;
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

    public void SetWaypoints(Vector3[] positions)
    {
        GameObject[] NewWayPoints = new GameObject[positions.Length];

        for (int i = 0; i < NewWayPoints.Length; i++)
        {
            //NewWayPoints[i] = Instantiate
        }
    }
}
