using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class UIHPNum : MonoBehaviour
{
    [SerializeField] public PanettoneGames.GenericEvents.IntEvent hpCountEvent;
    ExtendGenEventListener<int> hpCountEventListener = new ExtendGenEventListener<int>();
    TextMeshProUGUI meshPro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        meshPro = GetComponent<TextMeshProUGUI>();
        meshPro.text = "HP:-----";
        hpCountEventListener.action = (i) =>
        {
            meshPro.text = "HP:" + (i);
        };
        hpCountEvent.RegisterListener(hpCountEventListener);
    }

    private void OnDisable()
    {
        hpCountEvent.UnregisterListener(hpCountEventListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
