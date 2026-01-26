using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;

    private Vector3 prevPlayerPos;

    [SerializeField]
    private AudioResource[] tauntAudio, milestoneAudio, braceAudio, exhaleAudio, flopAudio, squelchAudio;

    [SerializeField]
    private AudioResource sizzelAudio;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private int tauntTriggerDistance, milestoneTriggerDistance; 

    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        prevPlayerPos = player.transform.position; 
    }

    void Update()
    {
        if (player.transform.position.x < (prevPlayerPos.x - milestoneTriggerDistance))
        {
            prevPlayerPos = player.transform.position;
            PlayRandom(milestoneAudio);            
        }

        if (player.transform.position.x > (prevPlayerPos.x + tauntTriggerDistance))
        {
            prevPlayerPos = player.transform.position;
            PlayRandom(tauntAudio);
        }

        if ( audioSource.resource != null && !audioSource.isPlaying)
        {
            audioSource.resource = null;
            audioSource.pitch = 1;
        }
    }

    private void PlayRandom(AudioResource[] targetSoundArray)
    {
        // Debug.Log(prevPlayerPos +" : "+ Random.Range(0,(targetSoundArray.Length-1)));
        audioSource.resource = targetSoundArray[Random.Range(0,(targetSoundArray.Length-1))];
        audioSource.Play();
    }

    public void PlaySizzle()
    {
        audioSource.resource = sizzelAudio;
        audioSource.pitch = Random.Range(0.5f,1.5f);
        audioSource.Play();
    }

    public void PlaySquelch()
    {
        PlayRandom(squelchAudio);
    }

    public void PlayBrace()
    {
        PlayRandom(braceAudio);
    }

    public void PlayExhale()
    {
        PlayRandom(exhaleAudio);
    }

    public void PlayFlop()
    {
        PlayRandom(flopAudio);
    }
}
