using UnityEngine;

[RequireComponent(typeof(SpawnerCubes))]
[RequireComponent(typeof(Exploder))]
public class SpawnerBombs : Spawner<Bomb>
{
    private SpawnerCubes _spawnerCubes;
    private Exploder _exploder;

    protected override void Awake()
    {
        base.Awake();
        _spawnerCubes = GetComponent<SpawnerCubes>();
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

    protected override void MakeFigure(Figure figure, Vector3 position)
    {
        base.MakeFigure(figure, position);
        ColorChanger.Fade(figure);
    }

    protected override void DestroyFigure(Figure figure)
    {
        base.DestroyFigure(figure);
        _exploder.Explode(figure);
    }
}