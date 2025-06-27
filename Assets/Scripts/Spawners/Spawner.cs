using System;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public abstract class Spawner<T> : MonoBehaviour where T : Figure
{
    [SerializeField] protected int PoolCapacity;
    [SerializeField] protected int MaxPoolCapacity;
    [SerializeField] protected T Prefab;
    protected int SpawnFigures = 0;
    protected int ActiveFigures = 0;
    protected PoolFigures PoolFigures;
    protected ColorChanger ColorChanger;
    public event Action<Figure> CreatedFigure;
    public event Action<Vector3> DestroyedFigure;
    public event Action<int> ChangedSpawnedFigures;
    public event Action<int> ChangedActiveFigures;
    public event Action<int> ChangedCountFiguresInPool;

    protected void OnCreatedFigure(Figure figure) => CreatedFigure?.Invoke(figure);
    protected void OnDestroyedFigure(Vector3 position) => DestroyedFigure?.Invoke(position);
    protected void OnChangedSpawnedFigures(int count) => ChangedSpawnedFigures?.Invoke(count);
    protected void OnChangedActiveFigures(int count) => ChangedActiveFigures?.Invoke(count);
    protected void OnChangedCountFiguresInPool(int count) => ChangedCountFiguresInPool?.Invoke(count);

    protected virtual void Awake()
    {
        PoolFigures = gameObject.AddComponent<PoolFigures>();
        PoolFigures.Init(PoolCapacity, MaxPoolCapacity, Prefab);
        ColorChanger = GetComponent<ColorChanger>();
    }

    virtual protected void MakeFigure(Figure figure, Vector3 position)
    {
        figure.Init(position);
        figure.Destroyed += DestroyFigure;
        OnChangedSpawnedFigures(++SpawnFigures);
        OnChangedActiveFigures(++ActiveFigures);
        OnChangedCountFiguresInPool(PoolFigures.GetCountObjectsInPool());
        ColorChanger.SetDefaultColor(figure);
    }

    virtual protected void DestroyFigure(Figure figure)
    {
        figure.Destroyed -= DestroyFigure;
        OnChangedActiveFigures(--ActiveFigures);
        PoolFigures.ReturnInstance(figure);
    }
}