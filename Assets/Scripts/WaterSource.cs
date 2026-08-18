using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaterSource : MonoBehaviour
{
    public bool isWater => true;

    public Vector3 ClosestWaterSource(Vector3 currentPosition)
    {
        Collider collider = GetComponent<Collider>();
        return collider != null ? collider.ClosestPoint(currentPosition) : transform.position;
    }
}
