using UnityEngine;

public abstract class PlatformFactory : ScriptableObject
{
    public abstract GameObject GetNewInstance(Vector3 position);
}
