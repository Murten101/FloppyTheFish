using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject TargetUi;
    public bool StartAnim = false;

    [SerializeField]
    private Animator anim;

    void Start()
    {
        anim.speed = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            StartAnim = true;

        if(StartAnim)
        {
            Debug.Log("animation started");
            anim.speed = 1;
            StartAnim = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") )   
            return;

        TargetUi.SetActive(true);
    }
}
