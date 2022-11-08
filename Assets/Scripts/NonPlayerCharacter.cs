using UnityEngine;

/// <summary>
/// This class handle Non player character. It store their lines of dialogues and the portrait to display.
/// The player controller will call the Advance function when the player press the interact button in front of the NPC
/// The advance function will return false as long as there is new dialogue line, but return true once finished.
/// (Used by Player Controller to block movement until the dialogue is finished)
/// </summary>
public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float timerDisplay;
    public string dialog_key = "jambi_say";


    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
        
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        var textMeshProGUI = transform.Find("DialogCanvas/Image/TextMeshPro Text").GetComponent<TMPro.TextMeshProUGUI>();
        textMeshProGUI.SetText(SystemInstance.systemInstance.gameObject.GetComponent<CSVReader>().keyValuePairs[dialog_key]);
        textMeshProGUI.richText = true;
        dialogBox.SetActive(true);
    }
}
