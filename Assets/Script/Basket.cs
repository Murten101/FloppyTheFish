using UnityEngine;
using UnityEngine.UIElements;

public class Basket : MonoBehaviour
{
    [SerializeField]
    private bool isFrying = false; 

    [SerializeField]
    private float maxLow, sinkSpeed, sinkDelay = 1;

    private int timeSinceLast = 0;
    private Vector3 neutralPos;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        neutralPos = rb.position;
    }

    void FixedUpdate()
    {
        if ( isFrying )
        {
            rb.position = Vector3.MoveTowards(transform.position, (neutralPos-Vector3.up*maxLow), sinkSpeed);
            if ( timeSinceLast != 0 && (int)Time.realtimeSinceStartup > timeSinceLast+sinkDelay ) 
                isFrying = false;

        }else if ( rb.position != neutralPos  )
        {
            rb.position = Vector3.MoveTowards(rb.position, neutralPos, sinkSpeed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isFrying = true;
    }

    void OnCollisionExit(Collision collision)
    {
        timeSinceLast = (int)Time.realtimeSinceStartup;
    }
}
