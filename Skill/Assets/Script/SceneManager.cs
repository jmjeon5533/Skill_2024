using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuning
{
    public enum TireState
    {
        normal,
        desert,
        mountain,
        road
    }
    public enum EngineState
    {
        normal,
        six,
        eight
    }
    public TireState tireState;
    public EngineState engineState;
}
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    public Tuning tuning = new Tuning();
    public int stageIndex;
    private void Start()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        tuning.engineState = Tuning.EngineState.normal;
        tuning.tireState = Tuning.TireState.normal;
    }

    public void StageStart(int stageIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(stageIndex + 1);
    }
}
