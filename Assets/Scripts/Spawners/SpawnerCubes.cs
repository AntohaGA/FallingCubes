using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RandomizerPoint))]
public class SpawnerCubes : Spawner<Cube>
{
    private float _spawnDelay = 0.5f;
    private RandomizerPoint _spawnRandomizer;
    private WaitForSeconds _waitForSeconds;
    private IEnumerator _initCubesCoroutine;

    protected override void Awake()
    {
        base.Awake();
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
        base.MakeFigure(figure, positionFigure);
        OnCreatedFigure(figure);
    }

    protected override void DestroyFigure(Figure figure)
    {
        figure.Touched -= ColorChanger.SetRandomColor;
        base.DestroyFigure(figure);
        OnDestroyedFigure(figure.transform.position);
    }
}