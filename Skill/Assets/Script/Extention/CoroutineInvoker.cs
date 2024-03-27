using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineInvoker : MonoBehaviour
{
    private static CoroutineInvoker instance;
    public static CoroutineInvoker Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("Invoker").AddComponent<CoroutineInvoker>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    
}
