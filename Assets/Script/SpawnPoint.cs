using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position = transform.position;
            foreach (var child in GetComponentInChildren<GameObject>)
            {
                child.transform.position = transform.position;                
                Debug.Log(child.name);
            }
        }
        
    }
}
