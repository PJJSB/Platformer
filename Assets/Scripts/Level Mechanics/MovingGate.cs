using UnityEngine;

public class MovingGate : MonoBehaviour
{
    public GameObject Gate;
    public GameObject CollisionBox;

    
    public float WayPoint;
    public float MinDistance = 0.05f;
    public float MoveSpeed = 5f;

    private bool playerCollision = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            playerCollision = true; 

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
