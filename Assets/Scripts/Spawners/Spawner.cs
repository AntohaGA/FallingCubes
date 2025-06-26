using System;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected int PoolCapacity;
    [SerializeField] protected int MaxPoolCapacity;
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

    abstract protected void MakeFigure(Figure figure, Vector3 position);
    abstract protected void DestroyFigure(Figure figure);
}