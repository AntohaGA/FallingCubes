using UnityEngine;

[RequireComponent(typeof(SpawnerCubes))]
[RequireComponent(typeof(Exploder))]
public class SpawnerBombs : Spawner
{
    [SerializeField] Bomb _prefub;

    private SpawnerCubes _spawnerCubes;
    private Exploder _exploder;

    private void Awake()
    {
        PoolFigures = gameObject.AddComponent<PoolFigures>();
        PoolFigures.Init(PoolCapacity, MaxPoolCapacity, _prefub);
        _spawnerCubes = GetComponent<SpawnerCubes>();
        ColorChanger = GetComponent<ColorChanger>();
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _spawnerCubes.CreatedFigure += Subscribe;
    }

    private void OnDisable()
    {
        _spawnerCubes.CreatedFigure -= Subscribe;
    }

    private void Subscribe(Figure figure)
    {
        figure.Destroyed += MakeBombInsteadCube;
    }

    private void MakeBombInsteadCube(Figure cube)
    {
        cube.Destroyed -= MakeBombInsteadCube;
        MakeFigure(PoolFigures.GetInstance(), cube.transform.position);
    }

    protected override void MakeFigure(Figure figure, Vector3 positionFigure)
    {
        figure.Init(positionFigure);
        figure.Destroyed += DestroyFigure;
        OnChangedSpawnedFigures(++SpawnFigures);
        OnChangedActiveFigures(++ActiveFigures);
        OnChangedCountFiguresInPool(PoolFigures.GetCountObjectsInPool());
        ColorChanger.SetDefaultColor(figure);
        ColorChanger.Fade(figure);
    }

    protected override void DestroyFigure(Figure figure)
    {
        figure.Destroyed -= DestroyFigure;
        _exploder.Explode(figure);
        OnChangedActiveFigures(--ActiveFigures);
        PoolFigures.ReturnInstance(figure);
    }
}