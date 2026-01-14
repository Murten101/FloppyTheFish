using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject TargetUi;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") )   
            return;

        TargetUi.SetActive(true);
    }
}
