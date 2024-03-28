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
    public readonly string[] TireName = { "" , "�縷 Ÿ�̾�" , "��� Ÿ�̾�", "���̽� Ÿ�̾�" };
    public readonly string[] TireExplain = { "", "�縷 ���ǵ�" , "��� ���ǵ�", "���� ���ǵ�" };
    public readonly string[] EngineName = { "" , "6���� ����" , "8���� ����"};
    public TireState tireState;
    public EngineState engineState;
}
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    public Image fadeImage;
    public Transform fadeCanvas;
    public Tuning tuning = new Tuning();
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
    public IEnumerator fadeAlpha(int targetAlpha)
    {
        while(Mathf.Abs(fadeImage.color.a - targetAlpha) >= 0.01f)
        {
            var a = Mathf.MoveTowards(fadeImage.color.a,targetAlpha,Time.deltaTime);
            fadeImage.color = new Color(0,0,0,a);
            yield return null;
        }
    }
}
