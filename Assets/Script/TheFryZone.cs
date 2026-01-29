using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TheFryZone : MonoBehaviour
{
    private const string BlendFactorPropName = "_BlendFactor";

    [SerializeField] private float _targetValue = 5f;
    [SerializeField] private float _moveSpeed = 2f;

    private int _inFryZone = 0;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider _)
    {
        _inFryZone += 1;

        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
    private void OnTriggerExit(Collider _)
    {
        _inFryZone -= 1;
        if (_inFryZone == 0 && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Renderer rend = other.GetComponent<Renderer>();
        if (rend == null) return;

        var curVal = rend.material.GetFloat(BlendFactorPropName);
        var newVal = Mathf.MoveTowards(curVal, _targetValue, _moveSpeed * Time.fixedDeltaTime);
        rend.material.SetFloat(BlendFactorPropName, newVal);
    }
}
