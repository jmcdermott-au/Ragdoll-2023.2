using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float forceBuild;
    private bool _shot = false;
    private bool _applyForce = false;
    public float _currentForce = 0;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shot)
        {
            return;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            _currentForce += Time.deltaTime * forceBuild;
            
        }

        if(_currentForce != 0 && Input.GetKeyUp(KeyCode.Space))
        {
            _applyForce = true;
        }
    }

    private void FixedUpdate()
    {
        if(!_applyForce)
        {
            return;
        }

        Vector3 forward = transform.right;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(forward * _currentForce, ForceMode.Impulse);

        _currentForce = 0;
        _applyForce = false;
        _shot = true;
    }
}
