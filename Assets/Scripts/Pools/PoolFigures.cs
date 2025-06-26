using UnityEngine;
using UnityEngine.Pool;

public class PoolFigures : MonoBehaviour
{
    private int _poolCapacity;
    private int _poolMaxSize;

    private Figure _prefab;
    private ObjectPool<Figure> _pool;

    public void Init(int poolCapacity, int poolMaxSize, Figure prefab)
    {
        _poolCapacity = poolCapacity;
        _poolMaxSize = poolMaxSize;
        _prefab = prefab;
        _pool = new ObjectPool<Figure>(CreateInstance, TakeFromPool, ReturnToPool,
                                  DestroyInstance, true, _poolCapacity, _poolMaxSize);
    }

    public Figure GetInstance()
    {
        return _pool.Get();
    }

    public void ReturnInstance(Figure figure)
    {
        _pool.Release(figure);
    }

    public int GetCountObjectsInPool()
    {
        return _pool.CountAll;
    }

    private Figure CreateInstance()
    {
        return Instantiate(_prefab);
    }

    private void TakeFromPool(Figure figure)
    {
        figure.gameObject.SetActive(true);
    }

    private void ReturnToPool(Figure figure)
    {
        figure.gameObject.SetActive(false);
    }

    private void DestroyInstance(Figure figure)
    {
        Destroy(figure.gameObject);
    }
}