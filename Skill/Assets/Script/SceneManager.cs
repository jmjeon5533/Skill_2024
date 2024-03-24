using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public int stageIndex;
    private void Start()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StageStart(int stageIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(stageIndex + 1);
    }
}
