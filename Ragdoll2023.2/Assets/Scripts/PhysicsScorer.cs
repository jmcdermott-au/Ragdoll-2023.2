using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhysicsScorer : MonoBehaviour
{

    public struct JointData
    {
        Joint singleJoints;
        float previousForce;
    }

    JointData[] jointData;

    public Transform ragdoll;
    

    public TMP_Text forceScore;

    

    public float score;

    private void Start()
    {
        Joint[] joints = ragdoll.GetComponentsInChildren<Joint>();
        jointData = new JointData[joints.Length];

        for (int index = 0 ; index < joints.Length; index++ )
        {
            Joint joint = joints[index];
            jointData[index] = new JointData();
            jointData[index].singlejoints = joint;
            jointData[index].previousForce = 0f;
        }

        
    }

    private void FixedUpdate()
    {

        float tempscore = Mathf.Abs(previousForce - singleJoint.currentForce.magnitude);


        if (tempscore >= 100)

        {
            score += tempscore;
        }
        previousForce = singleJoint.currentForce.magnitude;

        forceScore.text = score.ToString();
    }


}
