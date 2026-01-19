using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField]
    private bool isFrying = false; 

    [SerializeField]
    private float maxLow, sinkSpeed, sinkDelay = 1;

    private int timeSinceLast = 0;
    private Vector3 neutralPos;

    void Start()
    {
        neutralPos = transform.position;
    }

    void Update()
    {
        if ( isFrying )
        {
            transform.position = Vector3.MoveTowards(transform.position, (neutralPos-Vector3.up*maxLow), sinkSpeed);
            if ( timeSinceLast != 0 && (int)Time.realtimeSinceStartup > timeSinceLast+sinkDelay ) 
                isFrying = false;

        }else if ( transform.position != neutralPos  )
        {
            transform.position = Vector3.MoveTowards(transform.position, neutralPos, sinkSpeed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isFrying = true;
        // collision.gameObject.transform.SetParent(transform);
    }

    void OnCollisionExit(Collision collision)
    {
        timeSinceLast = (int)Time.realtimeSinceStartup;
        // collision.gameObject.transform.SetParent(transform.parent);
    }
}
