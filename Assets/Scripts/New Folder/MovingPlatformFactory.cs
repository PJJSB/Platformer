using UnityEngine;

public class MovingPlatformFactory : PlatformFactory
{
    public GameObject PrefabMovingPlatform;

    public override GameObject GetNewInstance(Vector3 newposition)
    {
        if (PrefabMovingPlatform == null)
        {
            throw new System.ArgumentNullException("The prefab was not configured in the factory.");
        }

        GameObject Instance = Instantiate(PrefabMovingPlatform);
        MovingPlatform Script = Instance.GetComponent<MovingPlatform>();

        Instance.transform.position = newposition;

        return Instance;
    }
}
