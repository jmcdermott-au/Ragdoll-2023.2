using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public float flipperStrength = 1000f;
    public float maxAngle = 45f;

    public float startAngle; 
    public float endAngle;
    
    //public float returnSpeed = 300f; 
    public KeyCode flipperKey = KeyCode.Space; 

    private Rigidbody flipperRigidbody;
    private Quaternion initialRotation;
    //private Quaternion endRotation;
    private bool isFlipping = false;

    public Transform centerOfMass;

    void Start()
    {

        flipperRigidbody = GetComponent<Rigidbody>();
        
        initialRotation = flipperRigidbody.rotation;

        flipperRigidbody.centerOfMass = centerOfMass.localPosition;
        flipperRigidbody.maxAngularVelocity = Mathf.Infinity;



        startAngle = transform.rotation.eulerAngles.y;
        endAngle = startAngle + maxAngle;
    }
    
    void Update()
    {

        isFlipping = Input.GetKey(flipperKey);
    }

    private void FixedUpdate()
    {
     //   float currentAngle = transform.rotation.eulerAngles.y;

        float signedAngle = Mathf.DeltaAngle( transform.eulerAngles.y ,startAngle);
     
       float t1AtStart = Mathf.InverseLerp(180, 0,  signedAngle);



       //float currentAngle = Quaternion.Angle(initialRotation, flipperRigidbody.rotation);
       //float currentAngle = SignedAngle(initialRotation, flipperRigidbody.rotation, transform.up);
        
       // Debug.Log(t + " | " + Mathf.Abs( startAngle - currentAngle) );
        
      // float rotationDiffEnd = Mathf.DeltaAngle(transform.eulerAngles.y, endAngle);
       
      //  float limitedRotationDiff = Mathf.Clamp(rotationDiff, 0, maxAngle);
     // float signedAngle = Vector3.SignedAngle(new Vector3(0,startAngle,0), transform.eulerAngles, transform.up);


      Debug.Log(t1AtStart);

      if (isFlipping)
        {
            if ( signedAngle < maxAngle)// && signedAnglestart)
            {
                flipperRigidbody.AddTorque(transform.up * (flipperStrength * -1 * (t1AtStart) ) , ForceMode.VelocityChange);
            }
            else
            {
                flipperRigidbody.angularVelocity = Vector3.zero;
                flipperRigidbody.rotation = initialRotation * Quaternion.Euler(0, startAngle - maxAngle, 0);
            }
        }
        else
        {
            if ( signedAngle  > 0f)
            {
                flipperRigidbody.AddTorque(transform.up * (flipperStrength  * (1- t1AtStart)) , ForceMode.VelocityChange);

            }
            else
            {
                flipperRigidbody.angularVelocity = Vector3.zero;
                flipperRigidbody.rotation = initialRotation;
            }
        }
    }


    private float SignedAngle(Quaternion A, Quaternion B, Vector3 axis)
    {

        // mock rotate the axis with each quaternion
        Vector3 vecA = A * axis;
        Vector3 vecB = B * axis;

       return Vector3.SignedAngle(vecA, vecB, axis);
    }


    private void rotateTest(Rigidbody rb)
    {
        
        float rotationDiff = Mathf.DeltaAngle(transform.eulerAngles.y, endAngle);
        float limitedRotationDiff = Mathf.Clamp(rotationDiff, 0, maxAngle);
        float newTargetRotation = transform.eulerAngles.y + limitedRotationDiff;
        Quaternion targetQuaternion = Quaternion.Euler(0f, newTargetRotation, 0f);
        Quaternion deltaRotation = targetQuaternion * Quaternion.Inverse(transform.rotation);

        // Apply torque to the Rigidbody to rotate towards the target rotation.
        rb.AddTorque(deltaRotation.eulerAngles);// * rb.inertiaTensor.magnitude);
    }
}
