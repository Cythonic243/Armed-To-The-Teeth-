using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class dialogueManagerScript : MonoBehaviour
{
    [SerializeField] private string dialogueAssigner;

    //List that holds all the dialogue ui text objects
    [SerializeField] public List<TMP_Text> Dialogue = new List<TMP_Text>();
    [SerializeField] private TMP_Text dialogueText;
    
    [SerializeField] private GameObject FadeToBlack;



    [SerializeField] private GameObject DentistObject;

    [SerializeField] private Sprite normDentist;
    [SerializeField] private Sprite clipboardDentist;

    [SerializeField] private GameObject ExampleImage;
    [SerializeField] private Sprite enemy;
    [SerializeField] private Sprite plaqueEnemy;
    [SerializeField] private Sprite tooth1;
    [SerializeField] private Sprite tooth2;
    [SerializeField] private Sprite tooth3;


    [SerializeField] private bool fadingIn;
    [SerializeField] private float fadeInCounter = 0f;
    [SerializeField] private bool fadingOut;
    [SerializeField] private float fadeOutCounter = 1f;


    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject nextSceneButton;

    [SerializeField] private bool lastText = false;

    [SerializeField] private int nextDialogueIndex = 0;

    //[SerializeField] private int NextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "...";
        ExampleImage.SetActive(true);
        fadingIn = true;
        

        if (dialogueAssigner != "intro")
        {
            ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //For fading in or out
        //Fade in comes from Start(), Fade out comes from "TutorialButton"
        if (fadingOut)
        {
            if (fadeOutCounter <= 1)
            {
                fadeOutCounter += 0.005f;
                FadeToBlack.GetComponent<Image>().color = new Color(0, 0, 0, fadeOutCounter);
            }
        }
        if (fadingIn)
        {
            if (fadeInCounter >= 0)
            {
                fadeInCounter -= 0.005f;
                FadeToBlack.GetComponent<Image>().color = new Color(0, 0, 0, fadeInCounter);
            }
            else
            {
                fadingIn = false;
            }
        }
    }

    public void ContinueButton()
    {
        Debug.Log("continueButton Pressed");
        
        dialogueText.text = Dialogue[nextDialogueIndex].text;
        nextDialogueIndex++;
        Debug.Log("index is: " + nextDialogueIndex);
        if (nextDialogueIndex >= Dialogue.Count)
        {
            continueButton.SetActive(false);
            nextSceneButton.SetActive(true);
            Debug.Log("End of dialogue at index: " + nextDialogueIndex);
        }

        //Checks which dialogue sequence this scene is, and uses the appropriate button function
        //Intro Scene
        if(dialogueAssigner == "intro")
        {
            if (nextDialogueIndex == 2)
            {
                DentistObject.GetComponent<Image>().sprite = clipboardDentist;
                ExampleImage.SetActive(true);
                ExampleImage.GetComponent<Image>().sprite = enemy;
            }

            if (nextDialogueIndex == 3)
            {
                ExampleImage.GetComponent<Image>().sprite = plaqueEnemy;
            }

            if (nextDialogueIndex == 5)
            {
                ExampleImage.GetComponent<Image>().sprite = tooth1;
            }

            if (nextDialogueIndex == 6)
            {
                ExampleImage.GetComponent<Image>().sprite = tooth2;
            }

            if (nextDialogueIndex == 7)
            {
                ExampleImage.GetComponent<Image>().sprite = tooth3;
            }

            if (nextDialogueIndex == 8)
            {
                ExampleImage.SetActive(false);
                DentistObject.GetComponent<Image>().sprite = normDentist;
            }
        }
        
        if (dialogueAssigner == "post-tutorial")
        {
            if (nextDialogueIndex == 1)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }

            if (nextDialogueIndex == 2)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            
            if (nextDialogueIndex == 3)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            if (nextDialogueIndex == 4)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                dialogueText.fontStyle = Dialogue[nextDialogueIndex - 1].fontStyle;
            }

        }
        if (dialogueAssigner == "patient1")
        {
            if (nextDialogueIndex == 1)
            {
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }

            if (nextDialogueIndex == 2)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            if (nextDialogueIndex == 3)
            {
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            if (nextDialogueIndex == 4)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        if (dialogueAssigner == "patient2")
        {
            if (nextDialogueIndex == 1)
            {
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }

            if (nextDialogueIndex == 2)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            if (nextDialogueIndex == 3)
            {
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        if (dialogueAssigner == "patient3")
        {
            if (nextDialogueIndex == 1)
            {
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }

            if (nextDialogueIndex == 3)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            if (nextDialogueIndex == 4)
            {
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            if (nextDialogueIndex == 5)
            {
                DentistObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                ExampleImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }

    }

    //Doesn't necessarily start the tutorial, just moves to the next scene
    public void StartTutorial()
    {
        Invoke(nameof(NextScene), 1.2f);
        fadingOut = true;
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
