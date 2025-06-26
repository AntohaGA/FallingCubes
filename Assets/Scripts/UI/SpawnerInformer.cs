using TMPro;
using UnityEngine;

public class SpawnerInformer : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

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
        _textAllSpawn.text = "����� ���������� - " + newValue.ToString();
    }

    public void UpdateTextActiveOnScene(int newValue)
    {
        _textActiveOnScene.text = "������� �� ����� - " + newValue.ToString();
    }

    public void UpdateTextAllObjectsInPool(int newValue)
    {
        _textAllObjectsInPool.text = "���������� �������� � ���� - " + newValue.ToString();
    }
}