using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private const float MinDelay = 2;
    private const float MaxDelay = 6;

    private bool _isTouched;

    private IEnumerator _delayToDestroyCoroutine;

    private WaitForSeconds _waitForSeconds;

    public event Action<Cube> Touched;
    public event Action<Cube> Destroyed;

    public Rigidbody Rigidbody { get ; private set; }
    public Renderer Renderer { get ; private set; }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 spawnPositoion)
    {
        _delayToDestroyCoroutine = DelayToDisable();
        _waitForSeconds = new WaitForSeconds(UnityEngine.Random.Range(MinDelay, MaxDelay));

        transform.SetPositionAndRotation(spawnPositoion, Quaternion.identity);
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        _isTouched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isTouched == false)
        {
            StartCoroutine(_delayToDestroyCoroutine);
            Touched?.Invoke(this);
            _isTouched = true;
        }
    }

    private IEnumerator DelayToDisable()
    {
        yield return _waitForSeconds;

        Destroyed?.Invoke(this);
    }
}