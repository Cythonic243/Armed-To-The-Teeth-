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
    [SerializeField] public PanettoneGames.GenericEvents.IntEvent toothCountEvent;
    ExtendGenEventListener<int> enemyCountEventListener = new ExtendGenEventListener<int>();
    ExtendGenEventListener<int> toothCountEventListener = new ExtendGenEventListener<int>();
    public UISuccess uISuccess;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        meshPro = GetComponent<TextMeshProUGUI>();
        meshPro.text = "Teeth:--";
        toothCountEventListener.action = (i) =>
        {
            meshPro.text = "Teeth:" + (i);
            if (i == 0)
            {
                uISuccess.Success();
            }
        };
        toothCountEvent.RegisterListener(toothCountEventListener);
    }

    private void OnDisable()
    {
        toothCountEvent.UnregisterListener(toothCountEventListener);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
