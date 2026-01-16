using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField]
    private UiManager uiManager;

    [SerializeField]
    private Ui deathUi;
    
    void OnTriggerEnter(Collider other)
    {
        if (!uiManager.isDead && !other.CompareTag("Player") )   
            return;

        uiManager.SwitchUi(deathUi.name);
        uiManager.isDead = true;
    }
}
