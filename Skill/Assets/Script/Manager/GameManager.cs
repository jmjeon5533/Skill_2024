using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= FindObjectOfType<GameManager>();

    public AudioClip BGM;
    public Transform[] curPath;
    public Player player;
    public Enemy enemy;

    public bool isgame;
    private void Start()
    {
        SoundManager.Instance?.SetAudio(BGM,SoundManager.SoundState.BGM);
        if(SceneManager.Instance != null) InitPlayer();
        isgame = false;
        StartCoroutine(GameStart());
    }
    private void InitPlayer()
    {
        var s = SceneManager.Instance;
        var tireIndex = (int)s.tuning.tireState;
        var engineIndex = (int)s.tuning.engineState;
        print($"{(int)s.tuning.tireState}, {s.stageIndex}");
        if(tireIndex == s.stageIndex)
        {
            player.acc += 3;
            player.maxSpeed += 3;
            print("Tire Upgrade!");
            print(s.tuning.TireName[tireIndex]);
            print(s.tuning.TireExplain[tireIndex]);
            print(UIManager.instance == null);
            UIManager.instance.AddShowImage($"{s.tuning.TireName[tireIndex]}\n{s.tuning.TireExplain[tireIndex]} +3"
            ,new Color(0.3f,1,1,0.65f));
        }
        if(engineIndex != 0)
        {
            player.acc += 3;
            player.maxSpeed += 3;
            print("Engine Upgrade!");
            UIManager.instance.AddShowImage($"{s.tuning.EngineName[engineIndex]}\n출력 스피드 +3",new Color(1,0.5f,0.3f,0.65f));
        }
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(UIManager.instance.ShowUpgrade());
        float timer = 4;
        int countTime = 3;
        while (true)
        {
            if (timer <= 0)
            {
                isgame = true;
                break;
            }
            timer -= Time.deltaTime;
            if(countTime >= timer) 
            {
                UIManager.instance.Count(countTime);
                countTime--;
            }
            yield return null;
        }
    }
}
