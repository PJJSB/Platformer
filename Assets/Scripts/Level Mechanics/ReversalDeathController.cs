using UnityEngine;

public class ReversalDeathController : MonoBehaviour
{
    //Killer object this script should be attached to
    public GameObject Killer;
    //Object to chase
    public GameObject Subject;
    
    //Flag for activy
    public bool _IsChasing = false;
    //The speed the killing zone chases at
    public float _ChaseSpeed = 5.0f;
    //Waypoint for spawning the killer
    public GameObject KillerSpawnPoint;
    

    void Start()
    {
    }

    void Update()
    {
        if (_IsChasing)
        {
            //Direction = Destination - Origin
            Vector3 direction = Subject.transform.position - Killer.transform.position;
            //Normalize to get a usable direction vector
            direction = direction.normalized;
            //Chase
            Killer.transform.position += direction * Time.deltaTime * _ChaseSpeed;
        }
    }

    public void ChangeChaseSpawnPoint(GameObject ChaseSpawnPoint)
    {
        KillerSpawnPoint = ChaseSpawnPoint;
    }

    public void ChangeChaseSpeed(float speed)
    {
        _ChaseSpeed = speed;
    }

    public void ChangeChaseSubject(GameObject ChaseSubject)
    {
        Subject = ChaseSubject;
    }
}
