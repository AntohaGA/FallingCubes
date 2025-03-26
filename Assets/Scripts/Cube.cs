using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool IsTouched;

    public event Action<Cube> Touched;
    public event Action<Cube> Destroyed;

    private IEnumerator _initCubesCoroutine;

    public WaitForSeconds waitForSeconds;

    public Renderer GetRenderer()
    {
        return GetComponent<Renderer>();
    }

    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }

    public void Init(Vector3 spawnPositoion, Color startColor)
    {
        _initCubesCoroutine = DelayToDisable();
        waitForSeconds = new WaitForSeconds(1);

        GetRenderer().material.SetColor("_Color", startColor);
        transform.position = spawnPositoion;
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