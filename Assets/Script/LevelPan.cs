using UnityEngine;

public class LevelPan : MonoBehaviour
{

    [SerializeField]
    private Camera playerCamera, panCamera;

    [SerializeField]
    private GameObject startPoint, finishPoint;

    [SerializeField]
    private float panSpeed = 0.5f;

    [SerializeField]
    private bool triggerPan = false;

    private bool playerCam = false;
    private bool panCam = true;

    void Start()
    {
        playerCamera.gameObject.SetActive(playerCam);        
        panCamera.transform.position = startPoint.transform.position;
    }

    void Update()
    {
        if ( !triggerPan )
            return;

        if (    !(panCamera.transform.position.x < finishPoint.transform.position.x + 2.5 &&
                panCamera.transform.position.x > finishPoint.transform.position.x - 2.5)
            )
            panCamera.transform.position = Vector3.MoveTowards(panCamera.transform.position, finishPoint.transform.position, panSpeed);

        else if ( playerCam == false )
            playerCamera.gameObject.SetActive(playerCam = true);

        if ( panCam && playerCam )
            panCamera.gameObject.SetActive( triggerPan = panCam = false);
    }
}
