using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    HingeJoint _hingeJoint;
    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        var motor = _hingeJoint.motor;
        motor.targetVelocity = Time.time * 10;
        _hingeJoint.motor = motor;
    }
}
