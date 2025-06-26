using System.Collections.Generic;
using UnityEngine;

public class FinderObjects : MonoBehaviour
{
    public List<Rigidbody> FindExplodableObjectsInArea(Vector3 explodeCenter, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(explodeCenter, radius);

        List<Rigidbody> colliders = new();

        foreach (Collider collider in hits)
        {
            if (collider.attachedRigidbody != null)
            {
                colliders.Add(collider.attachedRigidbody);
            }
        }

        return colliders;
    }
}