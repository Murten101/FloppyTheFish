using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> _onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnter.Invoke(other);
    }
}
