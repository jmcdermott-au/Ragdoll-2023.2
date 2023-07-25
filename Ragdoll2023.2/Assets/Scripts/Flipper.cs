using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public KeyCode keyCode;
    public Vector3 offsetCenter;
    public Vector3 axis;
    public float forceMultiplier;

    private Rigidbody _rigidbody;
    private bool _isKeyPressed = false;

    public float rotationSpeed = 30f;
    public float activeRotation;
/*    private float _inactiveRotation;
    private Quaternion _quatActiveRotation;
    private Quaternion _quatInactiveRotation;*/

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = offsetCenter;

/*        _quatActiveRotation = _rigidbody.rotation * Quaternion.Euler(0, activeRotation, 0);
        _quatInactiveRotation = _rigidbody.rotation;*/
    }

    void Update()
    {
        _isKeyPressed = Input.GetKey(keyCode);


    }

    float currentT = 0;
    private float currentForce = 0;
    private void FixedUpdate()
    {
        int t = _isKeyPressed ? 1 : 0;
        currentT = Mathf.Lerp(currentT, t, Time.deltaTime * rotationSpeed);

        currentForce = Mathf.Lerp(currentForce, forceMultiplier, currentT);

        //_rigidbody.rotation = Quaternion.Lerp(_quatInactiveRotation, _quatActiveRotation, currentT);


        _rigidbody.AddTorque(axis * forceMultiplier, ForceMode.Impulse);

}


}
