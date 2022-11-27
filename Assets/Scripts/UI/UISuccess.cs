using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISuccess : MonoBehaviour
{
    TextMeshProUGUI meshPro;
    // Start is called before the first frame update
    void Start()
    {
        meshPro = GetComponent<TextMeshProUGUI>();
        meshPro.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < 10) return;
        //if (LevelManager.instance.infectTeeth.Count + LevelManager.instance.vulnerableTeeth.Count == 0)
        if (LevelManager.instance.enemies.Count == 0)
        {
            meshPro.enabled = true;
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                // Reload
                UnityEngine.SceneManagement.SceneManager.LoadScene("ExampleScene");
            }
        }
    }
}
