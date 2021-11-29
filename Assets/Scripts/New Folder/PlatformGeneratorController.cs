using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneratorController : MonoBehaviour
{
    public PlatformFactory platformFactory;
    public Vector3 demoPosition;

    void Start()
    {
        platformFactory = ScriptableObject.CreateInstance<MovingPlatformFactory>();
        demoPosition = new Vector3(-550, 1, 230);
        platformFactory.GetNewInstance(demoPosition);
    }

    void Update()
    {
        
    }
}
