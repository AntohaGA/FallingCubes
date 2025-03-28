using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RandomizerPoint))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(PoolCubes))]
public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private float Delay;

    private bool isContinue = true;

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

        _waitForSeconds = new WaitForSeconds(Delay);
        _initCubesCoroutine = MakeCubesByDelay();
    }

    private void Start()
    {
        StartCoroutine(_initCubesCoroutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(_initCubesCoroutine);
    }

    public void Subscribe(Cube cube)
    {
        cube.Touched += _colorChanger.SetRandomColor;
        cube.Destroyed += _poolCubes.ReturnCoob;
    }

    private IEnumerator MakeCubesByDelay()
    {
        Cube cube;

        while (isContinue)
        {
            yield return _waitForSeconds;

            cube = _poolCubes.GetCube();
            _colorChanger.SetDefaultColor(cube);
            cube.Init(_spawnRandomizer.GetSpawn());
        }
    }
}