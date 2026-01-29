using UnityEngine;

public class TheFryZone : MonoBehaviour
{
    private const string BlendFactorPropName = "_BlendFactor";

    [SerializeField] private float _targetValue = 5f;
    [SerializeField] private float _moveSpeed = 2f;

    private void OnTriggerStay(Collider other)
    {
        Renderer rend = other.GetComponent<Renderer>();
        if (rend == null) return;

        var curVal = rend.material.GetFloat(BlendFactorPropName);
        var newVal = Mathf.MoveTowards(curVal, _targetValue, _moveSpeed * Time.fixedDeltaTime);
        rend.material.SetFloat(BlendFactorPropName, newVal);
    }
}
