using UnityEngine;
using System.Collections.Generic;
using System;

public class UiManager : MonoBehaviour
{
    public GameObject deathUi, defaultUi, menuUi, noUi;
    private Dictionary<String, Ui> uiDic;

    [SerializeField]
    private FishController player;

    public bool isDead = false;

    public bool isPaused = false;

    void Start()
    {
        PopulateUi();

        SwitchUi(defaultUi.name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            player.isMenuToggled = isPaused = !isPaused;

            if (isPaused) SwitchUi(menuUi.name);
            if (!isPaused) SwitchUi(defaultUi.name);
        }
        
    }

    void PopulateUi()
    {
        uiDic = new Dictionary<String, Ui>();

        foreach (var childUi in GetComponentsInChildren<Ui>(true))
            uiDic[childUi.name] = childUi;
    }

    public void HideUi()
    {
        foreach (var ui in uiDic.Values)
            ui.Hide();
    }

     public void SwitchUi(String targetUi)
    {
        foreach (var ui in uiDic.Values)
            ui.Hide();

        uiDic[targetUi].Show();
    }
}