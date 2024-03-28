using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= FindObjectOfType<GameManager>();

    public Transform[] curPath;
    public Player player;
    public Enemy enemy;

    public bool isgame;
    private void Start()
    {
        InitPlayer();
        isgame = false;
        StartCoroutine(GameStart());
    }
    private void InitPlayer()
    {
        var s = SceneManager.Instance;
        print($"{(int)s.tuning.tireState}, {s.stageIndex}");
        if((int)s.tuning.tireState + 1 == s.stageIndex)
        {
            player.acc += 5;
            player.maxSpeed += 5;
            print("Tire Upgrade!");
        }
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);
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
