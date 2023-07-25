using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public float flipperStrength = 30f;
    public float maxAngle = 45f;
    public KeyCode flipperKey = KeyCode.A;
    public Transform centerOfMass;

    private float _startAngle;
    private float _endAngle;
    private Rigidbody _rigidbody;
    private Quaternion _initialRotation;
    private bool _isFlipping = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialRotation = _rigidbody.rotation;
        _rigidbody.centerOfMass = centerOfMass.localPosition;
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
        _startAngle = transform.rotation.eulerAngles.y;
        _endAngle = _startAngle + maxAngle;
    }

    void Update()
    {
        _isFlipping = Input.GetKey(flipperKey);
    }

    private void FixedUpdate()
    {
        float signedAngle = Mathf.DeltaAngle(transform.eulerAngles.y, _startAngle);
        float t1AtStart = Mathf.InverseLerp(180, 0, signedAngle);

        if (_isFlipping)
        {
            if (signedAngle <= maxAngle)
            {
                _rigidbody.AddTorque(transform.up * (flipperStrength * -1 * (t1AtStart)), ForceMode.VelocityChange);
            }
            else
            {
                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.rotation = _initialRotation * Quaternion.Euler(0, _startAngle - maxAngle, 0);
            }
        }
        else
        {
            if (signedAngle > 0f)
            {
                _rigidbody.AddTorque(transform.up * (flipperStrength * (1 - t1AtStart)), ForceMode.VelocityChange);

            }
            else
            {
                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.rotation = _initialRotation;
            }
        }
    }
}
