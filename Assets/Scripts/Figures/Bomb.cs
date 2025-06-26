using System.Collections;
using UnityEngine;

public class Bomb : Figure
{
    private Coroutine DelayToDestroyCoroutine;

    public override void Init(Vector3 spawnPosition)
    {
        base.Init(spawnPosition);

        if (DelayToDestroyCoroutine != null)
            StopCoroutine(DelayToDestroyCoroutine);

        DelayToDestroyCoroutine = StartCoroutine(DelayToDisable());
    }

    private IEnumerator DelayToDisable()
    {
        yield return WaitForSeconds;

        OnDestroyed();
    }
}