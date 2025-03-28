using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(SpawnerCubes))]
public class PoolCubes : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private SpawnerCubes _spawnerCubes;

    private ObjectPool<Cube> _poolCubes;

    private void Awake()
    {
        _spawnerCubes = GetComponent<SpawnerCubes>();
        _poolCubes = new ObjectPool<Cube>(CreateCube, TakeFromPool, ReturnToPool,
                                          DestroyCube, true, _poolCapacity, _poolMaxSize);
    }

    public Cube GetCube()
    {
        return _poolCubes.Get();
    }

    public void ReturnCoob(Cube cube)
    {
        _poolCubes.Release(cube);
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_prefab);

        _spawnerCubes.Subscribe(cube);

        return cube;
    }

    private void TakeFromPool(Cube cube)
    {
        cube.gameObject.SetActive(true);
    }

    private void ReturnToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}