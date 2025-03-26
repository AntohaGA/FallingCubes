using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RandomizerPoint))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(PoolCubes))]
public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private float Delay = 5f;
    [SerializeField] Color defaultColor = Color.red;

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

    private IEnumerator MakeCubesByDelay()
    {
        while (true)
        {
            yield return _waitForSeconds;

            Cube cube = _poolCubes.GetCube();

            cube.Init(_spawnRandomizer.GetSpawn(), defaultColor);

            cube.Touched += _colorChanger.ChangeColor;
            cube.Destroyed += _poolCubes.ReturnCoob;
        }
    }
}