using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField]
    private List<HingeJoint> _hingeJointsHead = new();
    [SerializeField]
    private List<HingeJoint> _hingeJointsTail = new();
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

    [SerializeField]
    private KeyCode _tailUp = KeyCode.Q, _tailDown = KeyCode.W, _headUp = KeyCode.O, _headDown = KeyCode.P, _spinLeft = KeyCode.E, _spinRight = KeyCode.I;

    private bool playedBrace;

    [SerializeField]
    private AudioTrigger audioTrigger; 

    private KeyCode? _lastTailBendKey = null;
    private KeyCode? _lastHeadBendKey = null;
    
    public bool isMenuToggled = false;

    private void Start()
    {
        SetSpringStrength(_bendStrength);
    }

    void Update()
    {
        if (isMenuToggled) return;

        if (!playedBrace && 
            (Input.GetKeyDown(_spinLeft) || 
            Input.GetKeyDown(_spinRight)))
        {
                audioTrigger.PlayBrace();
                playedBrace = true;

        } else if ( playedBrace && 
                    !(Input.GetKeyDown(_spinLeft) ||
                    Input.GetKeyDown(_spinRight)))
        {
                playedBrace = false;
                audioTrigger.PlayExhale();
        }

        CheckBendHead();
        CheckBendTail();
        float torque =
            Input.GetKey(_spinLeft) ? _rotationTorque :
            Input.GetKey(_spinRight) ? -_rotationTorque :
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

    private void CheckBendTail()
    {
        if (Input.GetKeyDown(_tailDown)) _lastTailBendKey = _tailDown;
        if (Input.GetKeyDown(_tailUp)) _lastTailBendKey = _tailUp;
        float bendTarget = 0f;

        if (_lastTailBendKey != null && !Input.GetKey(_lastTailBendKey.Value))
        {
            _lastTailBendKey =
                Input.GetKey(_tailDown) ? _tailDown :
                Input.GetKey(_tailUp) ? _tailUp :
                null;
        }

        if (_lastTailBendKey == _tailDown && Input.GetKey(_tailDown))
            bendTarget = -_targetBendValue;
        else if (_lastTailBendKey == _tailUp && Input.GetKey(_tailUp))
            bendTarget = _targetBendValue;

        BendTail(bendTarget);
    }

    private void BendTail(float bendValue)
    {
        foreach (HingeJoint hinge in _hingeJointsTail)
        {
            if (hinge == null) continue;

            JointSpring spring = hinge.spring;
            spring.targetPosition = bendValue;
            hinge.spring = spring;

            hinge.useSpring = true;
        }
    }

    private void CheckBendHead()
    {
        if (Input.GetKeyDown(_headDown)) _lastHeadBendKey = _headDown;
        if (Input.GetKeyDown(_headUp)) _lastHeadBendKey = _headUp;
        float bendTarget = 0f;

        if (_lastHeadBendKey != null && !Input.GetKey(_lastHeadBendKey.Value))
        {
            _lastHeadBendKey =
                Input.GetKey(_headDown) ? _headDown :
                Input.GetKey(_headUp) ? _headUp :
                null;
        }

        if (_lastHeadBendKey == _headDown && Input.GetKey(_headDown))
            bendTarget = -_targetBendValue;
        else if (_lastHeadBendKey == _headUp && Input.GetKey(_headUp))
            bendTarget = _targetBendValue;

        BendHead(bendTarget);
    }

    private void BendHead(float bendValue)
    {
        foreach (HingeJoint hinge in _hingeJointsHead)
        {
            if (hinge == null) continue;

            JointSpring spring = hinge.spring;
            spring.targetPosition = bendValue;
            hinge.spring = spring;

            hinge.useSpring = true;
        }
    }

    private void SetSpringStrength(float value)
    {
        foreach (HingeJoint hinge in _hingeJointsHead)
        {
            if (hinge == null) continue;

            JointSpring spring = hinge.spring;
            spring.spring = value;
            hinge.spring = spring;
            hinge.useSpring = true;
        }
        foreach (HingeJoint hinge in _hingeJointsTail)
        {
            if (hinge == null) continue;

            JointSpring spring = hinge.spring;
            spring.spring = value;
            hinge.spring = spring;
            hinge.useSpring = true;
        }
    }

    private Vector3 ComputeCenterOfMass(List<Rigidbody> bodies)
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

    private float GetAverageRotationSpeed(List<Rigidbody> bodies)
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


    private void RotateChainAroundCG(List<Rigidbody> bodies, Vector3 rotationAxis, float torqueStrength)
    {
        Vector3 cg = ComputeCenterOfMass(bodies);

        foreach (var rb in bodies)
        {
            Vector3 r = rb.worldCenterOfMass - cg;

            if (r.sqrMagnitude < 0.0001f)
                continue;

            Vector3 torque = rotationAxis.normalized * torqueStrength;
            rb.AddTorque(torque * Time.deltaTime, ForceMode.Force);
        }
    }
}
