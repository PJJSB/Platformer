using UnityEngine;

public class RotatingPlatformFactory : PlatformFactory
{
    public GameObject PrefabMovingPlatform;

    public override GameObject GetNewInstance(Vector3 newposition)
    {
        if (PrefabMovingPlatform == null)
        {
            throw new System.ArgumentNullException("The prefab was not configured in the factory.");
        }

        GameObject Instance = Instantiate(PrefabMovingPlatform);
        RotatingPlatform Script = Instance.GetComponent<RotatingPlatform>();

        Instance.transform.position = newposition;

        return Instance;
    }
}
