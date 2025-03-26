using UnityEngine;

public class RandomizerPoint : MonoBehaviour
{
    [SerializeField] private Transform _startRandomPoint;

    [SerializeField] private float _maxXDeviation;
    [SerializeField] private float _maxYDeviation;
    [SerializeField] private float _maxZDeviation;

    public Vector3 GetSpawn()
    {
        float XCoordinateSpawn;
        float YCoordinateSpawn;
        float ZCoordinateSpawn;

        XCoordinateSpawn = _startRandomPoint.position.x + Random.Range( - _maxXDeviation, _maxXDeviation);
        YCoordinateSpawn = _startRandomPoint.position.y + Random.Range( - _maxYDeviation, _maxYDeviation);
        ZCoordinateSpawn = _startRandomPoint.position.z + Random.Range( - _maxZDeviation, _maxZDeviation);

        return new Vector3(XCoordinateSpawn, YCoordinateSpawn, ZCoordinateSpawn);
    }
}
