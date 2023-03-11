using UnityEngine;

public class CarEngine : MonoBehaviour
{
    [SerializeField] private float _maxVelocity = 50f;
    [SerializeField] private float _maxPower = 1000f;
    [SerializeField] private AnimationCurve _powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private float _sideWayForce = 100f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.centerOfMass = _centerOfMass.localPosition;
        
        var power = _powerCurve.Evaluate(_rigidbody.velocity.magnitude / _maxVelocity) * _maxPower;
        _rigidbody.AddForce(transform.forward * power);

        var localVelocity = transform.InverseTransformVector(_rigidbody.velocity);
        var localForce = new Vector3(-localVelocity.x * _sideWayForce, 0f, 0f);
        var worldForce = transform.TransformVector(localForce);
        
        _rigidbody.AddForce(worldForce);
    }
}
