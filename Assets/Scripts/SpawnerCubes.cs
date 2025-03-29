using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RandomizerPoint))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(PoolCubes))]
public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private float _delay;

    private bool _isContinue = true;

    private PoolCubes _poolCubes;
    private WaitForSeconds _waitForSeconds;
    private RandomizerPoint _spawnRandomizer;
    private ColorChanger _colorChanger;

    private IEnumerator _initCubesCoroutine;

    private void Awake()
    {
        _poolCubes = GetComponent<PoolCubes>();
        _spawnRandomizer = GetComponent<RandomizerPoint>();
        _colorChanger = GetComponent<ColorChanger>();

        _waitForSeconds = new WaitForSeconds(_delay);
        _initCubesCoroutine = MakeCubesByDelay();
    }

    private void Start()
    {
        StartCoroutine(_initCubesCoroutine);
    }

    private void UnSubscribe(Cube cube)
    {
        cube.Touched -= _colorChanger.SetRandomColor;
        cube.Destroyed -= UnSubscribe;
        _poolCubes.ReturnCoob(cube);
    }

    private IEnumerator MakeCubesByDelay()
    {
        Cube cube;

        while (_isContinue)
        {
            yield return _waitForSeconds;

            cube = _poolCubes.GetCube();
            cube.Touched += _colorChanger.SetRandomColor;
            cube.Destroyed += UnSubscribe;
            cube.Init(_spawnRandomizer.GetSpawn());
            _colorChanger.SetDefaultColor(cube);
        }
    }
}