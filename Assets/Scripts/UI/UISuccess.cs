using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISuccess : MonoBehaviour
{
    TextMeshProUGUI meshPro;
    bool isSuccess = false;
    public AudioClip successAudio;
    // Start is called before the first frame update
    void Start()
    {
        meshPro = GetComponent<TextMeshProUGUI>();
        meshPro.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSuccess) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            OnClickSuccess();
        }
    }

    public void Success()
    {
        if (successAudio != null)
        {
            GetComponent<AudioSource>().PlayOneShot(successAudio);
        }
        //if (LevelManager.instance.infectTeeth.Count + LevelManager.instance.vulnerableTeeth.Count == 0)
        // if (LevelManager.instance.enemies.Count == 0)
        {
            meshPro.enabled = true;
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void OnClickSuccess()
    {
        if (SystemInstance.systemInstance.levelIndex == 0)
        {
            // Reload
            UnityEngine.SceneManagement.SceneManager.LoadScene("PostTutorialDialogue");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PatientSelectionScreen");
        }
    }
}
