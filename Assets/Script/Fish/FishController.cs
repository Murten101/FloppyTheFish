using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField]
    private List<HingeJoint> _hingeJoints = new();
    [SerializeField]
    private List<Rigidbody> _rigidBodies = new();

    [SerializeField]
    private float _targetBendValue = 45f;
    [SerializeField]
    private float _rotationTorque = 10f;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SetTarget(-_targetBendValue);
        }else 
        if (Input.GetKey(KeyCode.D))
        {
            SetTarget(_targetBendValue);
        }else
        {
            SetTarget(0);
        }

        var torque = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            torque = _rotationTorque;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            torque = -_rotationTorque;
        }

        RotateChainAroundCG(_rigidBodies, Vector3.forward, torque);
    }

    void SetTarget(float value)
    {
        foreach (HingeJoint hinge in _hingeJoints)
        {
            if (hinge == null) continue;

            JointSpring spring = hinge.spring;
            spring.targetPosition = value;
            hinge.spring = spring;

            hinge.useSpring = true;
        }
    }
    Vector3 ComputeCenterOfMass(List<Rigidbody> bodies)
    {
        Vector3 com = Vector3.zero;
        float totalMass = 0f;

        foreach (var rb in bodies)
        {
            com += rb.worldCenterOfMass * rb.mass;
            totalMass += rb.mass;
        }

        return com / totalMass;
    }

    void RotateChainAroundCG(List<Rigidbody> bodies, Vector3 rotationAxis, float torqueStrength)
    {
        Vector3 cg = ComputeCenterOfMass(bodies);

        foreach (var rb in bodies)
        {
            Vector3 r = rb.worldCenterOfMass - cg;

            if (r.sqrMagnitude < 0.0001f)
                continue;

            Vector3 torque = rotationAxis.normalized * torqueStrength;
            rb.AddTorque(torque, ForceMode.Force);
        }
    }
}
