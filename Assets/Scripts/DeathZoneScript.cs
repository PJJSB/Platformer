using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    public GameObject respawnAnchor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            var controller = other.GetComponent<CharacterController>();
            
            //'respawn' player at respawnAnchor location
            // the jank is real
            controller.enabled = false;
            other.transform.position = respawnAnchor.transform.position;
            controller.enabled = true;
        }
    }
}
