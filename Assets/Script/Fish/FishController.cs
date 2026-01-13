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
    private float _maxRotationSpeed = 10f;
    [SerializeField]
    private float _rotationTorque = 10f;
    [SerializeField]
    private float _bendStrength = 100f;

    private KeyCode? lastBendKey = null;

    private void Start()
    {
        SetSpringStrength(_bendStrength);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) lastBendKey = KeyCode.A;
        if (Input.GetKeyDown(KeyCode.D)) lastBendKey = KeyCode.D;
        float bendTarget = 0f;

        if (lastBendKey != null && !Input.GetKey(lastBendKey.Value))
        {
            lastBendKey =
                Input.GetKey(KeyCode.A) ? KeyCode.A :
                Input.GetKey(KeyCode.D) ? KeyCode.D :
                null;
        }

        if (lastBendKey == KeyCode.A && Input.GetKey(KeyCode.A))
            bendTarget = -_targetBendValue;
        else if (lastBendKey == KeyCode.D && Input.GetKey(KeyCode.D))
            bendTarget = _targetBendValue;

        SetTarget(bendTarget);

        float torque =
            Input.GetKey(KeyCode.LeftArrow) ? _rotationTorque :
            Input.GetKey(KeyCode.RightArrow) ? -_rotationTorque :
            0f;

        if (torque != 0f &&
            GetAverageRotationSpeed(_rigidBodies) < _maxRotationSpeed)
        {
            RotateChainAroundCG(_rigidBodies, Vector3.forward, torque);
        }

#if UNITY_EDITOR
        SetSpringStrength(_bendStrength);
#endif
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

    void SetSpringStrength(float value)
    {
        foreach (HingeJoint hinge in _hingeJoints)
        {
            if (hinge == null) continue;

            JointSpring spring = hinge.spring;
            spring.spring = value;
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

    float GetAverageRotationSpeed(List<Rigidbody> bodies)
    {
        if (bodies == null || bodies.Count == 0)
            return 0f;

        float total = 0f;
        int count = 0;

        foreach (var rb in bodies)
        {
            if (!rb) continue;

            total += rb.angularVelocity.magnitude;
            count++;
        }

        return count > 0 ? total / count : 0f;
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
