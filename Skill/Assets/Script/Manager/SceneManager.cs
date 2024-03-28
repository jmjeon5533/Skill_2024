using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
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
    public readonly string[] TireName = { "" , "사막 타이어" , "산악 타이어", "레이싱 타이어" };
    public readonly string[] TireExplain = { "", "사막 스피드" , "산악 스피드", "도로 스피드" };
    public readonly string[] EngineName = { "" , "6기통 엔진" , "8기통 엔진"};
    public TireState tireState;
    public EngineState engineState;

}
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    public Image fadeImage;
    public Transform fadeCanvas;
    public Tuning tuning = new Tuning();
    public Player basePlayer;
    public int stageIndex;
    public int money;
    private void Start()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(fadeCanvas);
        money = 1000000;
    }

    public void StageStart(int Index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Index + 1);
        stageIndex = Index + 1;
    }
    public IEnumerator fadeOpen(bool isOpen)
    {
        float targetSize = isOpen ? 3000 : 0;
        float t = 0;
        while(t < 1)
        {
            var size = Mathf.Lerp(fadeImage.rectTransform.sizeDelta.x, targetSize, t);
            fadeImage.rectTransform.sizeDelta = new Vector2(size,size);
            t += Time.deltaTime * 0.5f;
            yield return null;
        }
    }
}
