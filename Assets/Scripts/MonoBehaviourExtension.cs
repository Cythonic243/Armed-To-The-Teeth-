using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MonoBehaviourExtension
{
    static IEnumerator CoHelp(Action action)
    {
        action();
        yield return action;
    }

    public static Coroutine StartCoroutine(this MonoBehaviour monoBehaviour, Action action)
    {
        return monoBehaviour.StartCoroutine(CoHelp(action));
    }
}