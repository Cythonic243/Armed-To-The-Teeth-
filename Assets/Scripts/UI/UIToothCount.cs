using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ExtendGenEventListener<T>: PanettoneGames.GenericEvents.IGameEventListener<T>
{
    public Action<T> action;
    internal object enemyCountEvent;

    public void OnEventRaised(T input)
    {
        if (action != null)
        {
            action(input);
        }
    }
}
public class UIToothCount : MonoBehaviour
{
    TextMeshProUGUI meshPro;
    [SerializeField] public PanettoneGames.GenericEvents.IntEvent enemyCountEvent;
    ExtendGenEventListener<int> enemyCountEventListener = new ExtendGenEventListener<int>();
    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void OnEnable()
    {
        meshPro = GetComponent<TextMeshProUGUI>();
        meshPro.text = "Bacteria:--";
        enemyCountEventListener.action = (i) =>
        {
            meshPro.text = "Bacteria:" + (i);
        };
        enemyCountEvent.RegisterListener(enemyCountEventListener);
    }

    private void OnDisable()
    {
        enemyCountEvent.UnregisterListener(enemyCountEventListener);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
