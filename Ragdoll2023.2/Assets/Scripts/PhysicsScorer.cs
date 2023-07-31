using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhysicsScorer : MonoBehaviour
{
    public Joint singleJoint;

    public TMP_Text forceScore;

    private void Update()
    {
        forceScore.text = singleJoint.currentForce.magnitude.ToString();
    }
}
