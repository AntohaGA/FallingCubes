using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private const float MinDelay = 2;
    private const float MaxDelay = 6;

    public bool IsTouched;

    public event Action<Cube> Touched;
    public event Action<Cube> Destroyed;

    private IEnumerator _initCubesCoroutine;

    public WaitForSeconds waitForSeconds;

    public Renderer GetRenderer()
    {
        return GetComponent<Renderer>();
    }

    private Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }

    public void Init(Vector3 spawnPositoion)
    {
        _initCubesCoroutine = DelayToDisable();
        waitForSeconds = new WaitForSeconds(UnityEngine.Random.Range(MinDelay, MaxDelay));

        transform.SetPositionAndRotation(spawnPositoion, Quaternion.identity);
        GetRigidbody().velocity = Vector3.zero;
        GetRigidbody().angularVelocity = Vector3.zero;
        IsTouched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(IsTouched == false)
        {
            StartCoroutine(_initCubesCoroutine);
            Touched?.Invoke(this);
            IsTouched = true;
        }
    }

    private IEnumerator DelayToDisable()
    {
        yield return waitForSeconds;

        Destroyed?.Invoke(this);
    }
}