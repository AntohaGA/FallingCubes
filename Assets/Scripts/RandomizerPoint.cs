using UnityEngine;

public class RandomizerPoint : MonoBehaviour
{
    [SerializeField] private Transform _startRandomPoint;

    [SerializeField] private float _maxXDeviation;
    [SerializeField] private float _maxYDeviation;
    [SerializeField] private float _maxZDeviation;

    public Vector3 GetSpawn()
    {
        return new Vector3
            (_startRandomPoint.position.x + Random.Range(-_maxXDeviation, _maxXDeviation),
             _startRandomPoint.position.y + Random.Range(-_maxYDeviation, _maxYDeviation),
             _startRandomPoint.position.z + Random.Range(-_maxZDeviation, _maxZDeviation));
    }
}