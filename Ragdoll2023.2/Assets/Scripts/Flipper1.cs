using System.Collections;
using UnityEngine;

public class Flipper1 : MonoBehaviour
{
    public float flipperStrength = 1000f; // Adjust this to control the flipper's strength
    public float maxAngle = 45f; // The maximum angle the flipper can rotate
    public float returnSpeed = 300f; // The speed at which the flipper returns to its original position
    public KeyCode flipperKey = KeyCode.Space; // Change this to the desired flipper control key

    private Rigidbody flipperRigidbody;
    private Quaternion initialRotation;
    private bool isFlipping = false;

    void Start()
    {
        // Assuming the flipper object has a Rigidbody component attached to it
        flipperRigidbody = GetComponent<Rigidbody>();

        // Store the initial rotation of the flipper
        initialRotation = flipperRigidbody.rotation;
    }

    void Update()
    {
        // Check if the flipper key is pressed and the flipper is not currently flipping
        if (Input.GetKey(flipperKey) && !isFlipping)
        {
            

            // Set a flag to prevent continuous flipping by holding down the key
            isFlipping = true;
        }
        else
        {
            isFlipping = false;
        }
    }

    void FixedUpdate()
    {

        if (isFlipping)
        {
            flipperRigidbody.AddTorque(transform.up * flipperStrength, ForceMode.Impulse);
            return;
        }

        
        float angleDifference = Quaternion.Angle(initialRotation, flipperRigidbody.rotation);

        // If the flipper has rotated beyond the maxAngle, reduce the torque to stop it from rotating further
        if (angleDifference >= maxAngle)
        {
            // Calculate the torque needed to return the flipper to its original position
            float returnTorque = returnSpeed * Mathf.Deg2Rad;

            // Check if the angle difference is decreasing (flipper is returning to its original position)
            if (Quaternion.Angle(initialRotation, flipperRigidbody.rotation) < angleDifference)
            {
                // Apply torque in the opposite direction to return the flipper
                flipperRigidbody.AddTorque(-transform.up * returnTorque, ForceMode.Impulse);
            }
            else
            {
                // Stop the flipper from rotating further
                flipperRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
