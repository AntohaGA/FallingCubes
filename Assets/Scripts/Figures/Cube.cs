using System.Collections;
using UnityEngine;

public class Cube : Figure
{
    public override void Init(Vector3 spawnPosition)
    {
        base.Init(spawnPosition);
        StartCoroutine(DelayToDisable());
        IsTouched = false;
    }

    private IEnumerator DelayToDisable()
    {
        yield return WaitForSeconds;

        OnDestroyed();
    }
}