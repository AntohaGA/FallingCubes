using TMPro;
using UnityEngine;

public class SpawnerInformer<T> : MonoBehaviour where T : Figure
{
    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TextMeshProUGUI _textAllSpawn;
    [SerializeField] private TextMeshProUGUI _textActiveOnScene;
    [SerializeField] private TextMeshProUGUI _textAllObjectsInPool;

    private void OnEnable()
    {
        _spawner.ChangedSpawnedFigures += UpdateTextAllSpawn;
        _spawner.ChangedActiveFigures += UpdateTextActiveOnScene;
        _spawner.ChangedCountFiguresInPool += UpdateTextAllObjectsInPool;
    }

    private void OnDisable()
    {
        _spawner.ChangedSpawnedFigures -= UpdateTextAllSpawn;
        _spawner.ChangedActiveFigures -= UpdateTextActiveOnScene;
        _spawner.ChangedCountFiguresInPool -= UpdateTextAllObjectsInPool;
    }

    public void UpdateTextAllSpawn(int newValue)
    {
        _textAllSpawn.text = "Всего заспавнено - " + newValue.ToString();
    }

    public void UpdateTextActiveOnScene(int newValue)
    {
        _textActiveOnScene.text = "Активно на сцене - " + newValue.ToString();
    }

    public void UpdateTextAllObjectsInPool(int newValue)
    {
        _textAllObjectsInPool.text = "Количество объектов в пуле - " + newValue.ToString();
    }
}