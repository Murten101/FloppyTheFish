using TMPro;
using UnityEngine;

public class GetLastTime : MonoBehaviour
{
    [SerializeField] TMP_Text _targetText;

    private void Start()
    {
        var time = PlayerPrefs.GetFloat(GameTimer.LastRunTimeKey, -1);
        var timeString = time == -1 ? "-" : time.ToString();
        _targetText.text = $"This run: {timeString} seconds";
    }
}
