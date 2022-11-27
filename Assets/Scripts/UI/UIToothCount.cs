using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIToothCount : MonoBehaviour
{
    TextMeshProUGUI meshPro;
    // Start is called before the first frame update
    void Start()
    {
        meshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        meshPro.text = "Bacteria:" + (LevelManager.instance.enemies.Count);
        
    }
}
