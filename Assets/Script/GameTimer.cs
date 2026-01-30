using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    void Update()
    {
        _timerText.text = $"Time: {Mathf.Floor(Time.timeSinceLevelLoad)}";
    }
}
