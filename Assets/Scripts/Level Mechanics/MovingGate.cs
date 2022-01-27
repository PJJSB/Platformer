using UnityEngine;

public class MovingGate : MonoBehaviour
{
    public GameObject Gate;
    public GameObject CollisionBox;

    
    public float WayPoint;
    public float MinDistance = 0.05f;
    public float MoveSpeed = 5f;
    public GameObject gateLockedTrigger;

    private bool playerCollision = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            playerCollision = true;
            if (gateLockedTrigger != null)
            {
                gateLockedTrigger.SetActive(false);
            }
        }
    }
    

    private void Update()
    {
        if (playerCollision)
        {
            float direction = WayPoint - Gate.transform.position.y;
            if (Mathf.Abs(direction) > MinDistance)
            {
                Gate.transform.position += new Vector3(0, MoveSpeed * Time.deltaTime * direction,0);
            }

            else
            {
                playerCollision = false;
            }
        }
    }
}
