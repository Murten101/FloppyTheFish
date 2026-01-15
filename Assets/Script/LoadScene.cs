using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{

    [SerializeField] private int scenetoload = 0;

    public void LoadTheScene()
    {
        SceneManager.LoadScene(scenetoload);
    }

    public void Quitgame()
    {
        Application.Quit();
    }

}
 