using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class startMenuScript : MonoBehaviour
{
    //[SerializeField] Button StartButton;
    //[SerializeField] Button QuitButton;
    [SerializeField] private int NextSceneIndex;

    public void startButton()
    {
        //Load scene of index 1, may need to change depending on how many scenes there are
        SceneManager.LoadScene(NextSceneIndex);
        Debug.Log("scene loaded: " + NextSceneIndex);
    }
    public void quitButton()
    {
        Application.Quit();
        Debug.Log("application quit");
    }
}
