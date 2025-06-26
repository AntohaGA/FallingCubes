using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Figure : MonoBehaviour
{
    [SerializeField] protected float MinDelay;
    [SerializeField] protected float MaxDelay;

    protected WaitForSeconds WaitForSeconds;
    public float ExplodeForse { get; private set; } = 200;

    protected bool IsTouched;

    public event Action<Figure> Touched;
    public event Action<Figure> Destroyed;

    public Rigidbody Rigidbody { get; protected set; }
    public Renderer Renderer { get; protected set; }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected void OnDestroyed()
    {
        Destroyed?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsTouched == false)
        {
            Touched?.Invoke(this);
            IsTouched = true;
        }
    }

    public virtual void Init(Vector3 position)
    {
        transform.SetPositionAndRotation(position, Quaternion.identity);
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        WaitForSeconds = new WaitForSeconds(UnityEngine.Random.Range(MinDelay, MaxDelay));
    }
}