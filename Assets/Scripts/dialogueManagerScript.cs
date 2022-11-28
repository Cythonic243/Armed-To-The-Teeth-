using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class dialogueManagerScript : MonoBehaviour
{
    //List that holds all the dialogue ui text objects
    [SerializeField] public List<TMP_Text> Dialogue = new List<TMP_Text>();
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private GameObject DentistObject;

    [SerializeField] private Sprite normDentist;
    [SerializeField] private Sprite clipboardDentist;

    [SerializeField] private GameObject ExampleImage;
    [SerializeField] private Sprite enemy;
    [SerializeField] private Sprite plaqueEnemy;
    [SerializeField] private Sprite tooth1;
    [SerializeField] private Sprite tooth2;
    [SerializeField] private Sprite tooth3;


    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject tutorialButton;

    [SerializeField] private bool lastText = false;

    [SerializeField] private int nextDialogueIndex = 0;

    [SerializeField] private int NextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "...";
        ExampleImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueButton()
    {
        Debug.Log("continueButton Pressed");
        
        dialogueText.text = Dialogue[nextDialogueIndex].text;
        nextDialogueIndex++;
        Debug.Log("index is: " + nextDialogueIndex);
        Debug.Log("list count is: " + Dialogue.Count);
        if (nextDialogueIndex >= Dialogue.Count)
        {
            continueButton.SetActive(false);
            tutorialButton.SetActive(true);
            Debug.Log("End of dialogue at index: " + nextDialogueIndex);
        }

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

    public void StartTutorial()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }


}
