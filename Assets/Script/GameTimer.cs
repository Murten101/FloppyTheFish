using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    internal const string LastRunTimeKey = "LastRunTime";
    internal const string FastestRunTimeKey = "FastestRunTime";
    [SerializeField] private TMP_Text _timerText;

    private bool _stopTimer = false;

    void Update()
    {
        if (!_stopTimer)
            _timerText.text = $"Time: {Mathf.Floor(Time.timeSinceLevelLoad)}";
    }

    public void StopTimer()
    {
        _stopTimer = true;
        var runTime = Time.timeSinceLevelLoad;
        PlayerPrefs.SetFloat(LastRunTimeKey, runTime);

        var fastestTime = PlayerPrefs.GetFloat(FastestRunTimeKey, float.MaxValue);
        
        if (fastestTime > Time.timeSinceLevelLoad)
        {
            PlayerPrefs.SetFloat(FastestRunTimeKey, runTime);
        }
        SceneManager.LoadScene(2);
    }
}