using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Vector3 prevPlayerPos;

    [SerializeField]
    private AudioResource[] tauntAudio; 

    [SerializeField]
    private AudioResource[] milestoneAudio;

    private AudioSource audioSource;

    [SerializeField]
    private int tauntTriggerDistance, milestoneTriggerDistance; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        prevPlayerPos = player.transform.position; 
    }

    void Update()
    {
        if (player.transform.position.x > prevPlayerPos.x + tauntTriggerDistance)
        {
            prevPlayerPos = player.transform.position;
            audioSource.resource = milestoneAudio[Random.Range(0,(milestoneAudio.Length-1))];
            audioSource.Play();
        }

        if (player.transform.position.x < prevPlayerPos.x - tauntTriggerDistance)
        {
            prevPlayerPos = player.transform.position;
            audioSource.resource = tauntAudio[Random.Range(0,(milestoneAudio.Length-1))];
            audioSource.Play();
        }
        
    }
}
