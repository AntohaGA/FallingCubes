using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FinderObjects))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionRadius;

    private FinderObjects _finderObjects;
    private int _countExplodeForCheck = 0;

    private void Awake()
    {
        _finderObjects = GetComponent<FinderObjects>();
    }

    public void Explode(Figure explodeFigure)
    {
        List<Rigidbody> explodableObjects = _finderObjects.FindExplodableObjectsInArea(explodeFigure.transform.position, _explosionRadius);

        foreach (Rigidbody rigidBody in explodableObjects)
        {
            rigidBody.AddExplosionForce(explodeFigure.ExplodeForse, explodeFigure.transform.position, _explosionRadius);
        }

        Debug.Log(++_countExplodeForCheck);
    }
}