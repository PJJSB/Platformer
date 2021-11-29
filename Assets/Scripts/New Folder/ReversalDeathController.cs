using UnityEngine;

public class ReversalDeathController : MonoBehaviour
{
    //killer object
    public GameObject PreFabRef;
    private GameObject _ReversalDeathObject;
    private DeathZoneScript _ReversalDeathScript;

    //Flag for activy
    private bool _IsChasing;
    //The speed the killing zone chases at
    private float _ChaseSpeed;
    

    void Start()
    {
        _IsChasing = false;
        _ChaseSpeed = 1.0f;
    }

    void Update()
    {
        if (_ReversalDeathObject != null)
        {
            Vector3 spawn = _ReversalDeathScript.respawnAnchorReturn.transform.position;
            Vector3 subject = _ReversalDeathScript.respawnAnchor.transform.position;
            //Destination - Origin
            Vector3 direction = subject - spawn;
            //Normalize to get a usable direction vector
            direction = direction.normalized;
            //Move the killzone
            _ReversalDeathObject.transform.position += direction * Time.deltaTime * _ChaseSpeed;
        }
    }

    public void ChangeChaseSpawnPoint(GameObject ChaseSpawnPoint)
    {
        _ReversalDeathScript.respawnAnchorReturn = ChaseSpawnPoint;
    }

    public void ChangeChaseSpeed(float speed)
    {
        _ChaseSpeed = speed;
    }

    public void ChangeChaseSubject(GameObject ChaseSubject)
    {
        _ReversalDeathScript.respawnAnchor = ChaseSubject;
    }

    public void ReStartReversalChase(Vector3 position)
    {
        if (_IsChasing)
        {
            _ReversalDeathObject.transform.position = position;
        }
    }

    public void StartReversalChase(GameObject ChaseSpawnPoint, GameObject ChaseSubject)
    {
        //Set the flag
        _IsChasing = true;
        //Instantiate death zone and set references
        _ReversalDeathObject =  Instantiate(PreFabRef, ChaseSpawnPoint.transform.position, Quaternion.identity);
        _ReversalDeathScript = _ReversalDeathObject.GetComponent<DeathZoneScript>();
        ChangeChaseSpawnPoint(ChaseSpawnPoint);
        ChangeChaseSubject(ChaseSubject);
    }

    public void StopReversalChase()
    {
        _IsChasing = false;
        Destroy(_ReversalDeathObject);
    }
}
