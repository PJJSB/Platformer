using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Object the camera should follow
    public GameObject leaderObject;
    //Camera offset from the object its following 
    public Vector3 cameraOffset = new Vector3(1, 1, -5);

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = leaderObject.transform.position + cameraOffset;
    }
}
