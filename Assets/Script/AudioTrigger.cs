using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;

    private Vector3 prevPlayerPos;

    [SerializeField]
    private AudioResource[] tauntAudio, milestoneAudio, braceAudio, holdAudio, exhaleAudio, flopAudio, squelchAudio;

    [SerializeField]
    private AudioResource sizzelAudio;

    [SerializeField]
    private AudioSource audioSource, bgAudio, effortAudio, holdSource;

    [SerializeField]
    private bool funnyFilters = false;

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
            PlayRandom(milestoneAudio, true);            
        }

        if (player.transform.position.x > (prevPlayerPos.x + tauntTriggerDistance))
        {
            prevPlayerPos = player.transform.position;
            PlayRandom(tauntAudio, true);
        }

        if ( audioSource.resource != null && !audioSource.isPlaying)
        {
            audioSource.resource = null;
            audioSource.pitch = 1;
        }

        if ( !effortAudio.isPlaying && !funnyFilters)
            audioSource.pitch = 1;
    }

    private void PlayRandom(AudioResource[] targetSoundArray)
    {
        audioSource.resource = targetSoundArray[Random.Range(0,(targetSoundArray.Length-1))];
        audioSource.Play();
    }

    private void PlayRandom(AudioResource[] targetSoundArray, bool altChannel)
    {
        effortAudio.resource = targetSoundArray[Random.Range(0,(targetSoundArray.Length-1))];
        effortAudio.Play();
    }

    public void PlaySizzle()
    {
        audioSource.resource = sizzelAudio;
        audioSource.pitch = Random.Range(0.8f,1.5f);
        audioSource.Play();
    }

    public void PlaySquelch()
    {
        PlayRandom(squelchAudio);
    }

    public void PlayBrace()
    {
        if (funnyFilters)
            effortAudio.pitch = Random.Range(0.8f,1.5f);

        PlayRandom(braceAudio, true);
    }

    public void PlayHold()
    {
        if (funnyFilters)
            holdSource.pitch = Random.Range(0.8f,1.5f);

        PlayRandom(holdAudio, true);
    }

    public void PlayExhale()
    {
        if (funnyFilters)
            effortAudio.pitch = Random.Range(0.8f,1.5f);

        PlayRandom(exhaleAudio, true);
    }

    public void PlayFlop()
    {
        PlayRandom(flopAudio);
    }

    public void SetVolume()
    {
        effortAudio.volume = bgAudio.volume = audioSource.volume = PlayerPrefs.GetFloat("SavedMasterVolume", 50);
    }

    public void TogglePause(bool isPaused)
    {       
        if (isPaused)
        {
            audioSource.Pause();
            bgAudio.Pause();
            effortAudio.Pause();
        } else if ( !audioSource.isPlaying || !bgAudio.isPlaying )
        {
            audioSource.UnPause();
            bgAudio.UnPause();
            effortAudio.UnPause();
        }

    }

    public void ToggleFunnySounds()
    {
        funnyFilters = !funnyFilters;
    }
}
