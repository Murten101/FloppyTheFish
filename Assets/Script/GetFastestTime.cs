using TMPro;
using UnityEngine;

public class GetFastestTime : MonoBehaviour
{
    [SerializeField] TMP_Text _targetText;

    private void Start()
    {
        var time = PlayerPrefs.GetFloat(GameTimer.FastestRunTimeKey, -1);
        var timeString = time == -1 ? "-" : time.ToString();
        _targetText.text = $"Best: {timeString} seconds";
    }
}
