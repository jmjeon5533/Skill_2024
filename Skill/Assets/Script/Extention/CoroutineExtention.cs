using System;
using System.Collections;
using UnityEngine;

public static class CoroutineExtention
{
    public static void OnComplete(this Coroutine coroutine, Action Callback)
    {
        CoroutineInvoker.Instance.StartCoroutine(waitRoutine(coroutine,Callback));
    }
    private static IEnumerator waitRoutine(Coroutine coroutine, Action Callback)
    {
        yield return coroutine;
        Callback?.Invoke();
    }
}
