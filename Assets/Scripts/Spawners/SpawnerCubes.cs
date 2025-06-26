using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RandomizerPoint))]
public class SpawnerCubes : Spawner
{
    [SerializeField] Cube _prefab;

    private RandomizerPoint _spawnRandomizer;

    private float _spawnDelay = 0.5f;

    private WaitForSeconds _waitForSeconds;
    private IEnumerator _initCubesCoroutine;

    private void Awake()
    {
        PoolFigures = gameObject.AddComponent<PoolFigures>();
        PoolFigures.Init(PoolCapacity, MaxPoolCapacity, _prefab);
        ColorChanger = GetComponent<ColorChanger>();
        _waitForSeconds = new WaitForSeconds(_spawnDelay);
        _spawnRandomizer = GetComponent<RandomizerPoint>();
        _initCubesCoroutine = MakeCubesByDelay();
    }

    private void Start()
    {
        StartCoroutine(_initCubesCoroutine);
    }

    private IEnumerator MakeCubesByDelay()
    {
        while (enabled)
        {
            yield return _waitForSeconds;

            MakeFigure(PoolFigures.GetInstance(), _spawnRandomizer.GetSpawn());
        }
    }

    protected override void MakeFigure(Figure figure, Vector3 positionFigure)
    {
        figure.Touched += ColorChanger.SetRandomColor;
        figure.Destroyed += DestroyFigure;
        figure.Init(positionFigure);
        OnCreatedFigure(figure);
        OnChangedSpawnedFigures(++SpawnFigures);
        OnChangedActiveFigures(++ActiveFigures);
        OnChangedCountFiguresInPool(PoolFigures.GetCountObjectsInPool());
        ColorChanger.SetDefaultColor(figure);
    }

    protected override void DestroyFigure(Figure figure)
    {
        figure.Touched -= ColorChanger.SetRandomColor;
        figure.Destroyed -= DestroyFigure;
        OnDestroyedFigure(figure.transform.position);
        OnChangedActiveFigures(--ActiveFigures);
        PoolFigures.ReturnInstance(figure);
    }
}