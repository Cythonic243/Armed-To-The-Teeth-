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
    [SerializeField] private int SceneIndex;
    // Start is called before the first frame update

    public void startButton()
    {
        //Load scene of index 1, may need to change depending on how many scenes there are
        SceneManager.LoadScene(SceneIndex);
        Debug.Log("scene loaded: " + SceneIndex);
    }
    public void quitButton()
    {
        Application.Quit();
        Debug.Log("application quit");
    }
}
