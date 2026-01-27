using UnityEngine;

public class FishCorrection : MonoBehaviour
{

    [SerializeField]
    private GameObject correctionRef;

    // [SerializeField]
    // private float adjustmentForce;

    // [SerializeField]
    // private GameObject  fishParent; 

    [SerializeField]
    private Rigidbody[] fishRbs;

    [SerializeField]
    private float maxWidth;
    
    void Start()
    {
        
    }

    void Update()
    {
       CheckCentered();
    }

    void CheckCentered()
    {
        float currentFishPos = fishRbs[1].transform.position.x;

        if (currentFishPos > correctionRef.transform.position.x+maxWidth ||
            currentFishPos < correctionRef.transform.position.x-maxWidth)
        {
            Vector3 adjustmentForce = Vector3.left * (correctionRef.transform.position.x - currentFishPos); 
            Debug.Log(adjustmentForce);

            fishRbs[0].AddRelativeForce(adjustmentForce,ForceMode.Impulse);
            fishRbs[1].AddRelativeForce(adjustmentForce,ForceMode.Impulse);
            fishRbs[2].AddRelativeForce(adjustmentForce,ForceMode.Impulse);
            fishRbs[3].AddRelativeForce(adjustmentForce,ForceMode.Impulse);
        }
    }
}
