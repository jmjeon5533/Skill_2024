using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TitleManager : MonoBehaviour
{
    public Button[] titleButtons;

    [Header("애니메이션")]
    [SerializeField] private Transform[] titleProp;
    [SerializeField] private Transform[] titleRoad;
    [SerializeField] private float roadSpeed;
    [SerializeField] private Transform car;
    private Transform[] carWheel;
    private Vector3 carPos;
    bool isMove;
    [Header("UI")]
    [Tooltip("랭킹 표시 오브젝트")][SerializeField] private RectTransform ranking;
    [Tooltip("랭킹에서 돌아오기")][SerializeField] private Button rankingBack;
    [Tooltip("랭킹 텍스트 버튼")][SerializeField] private Text[] rankingText;
    [SerializeField] private AudioClip BGM;

    private void Start()
    {
        carWheel = new Transform[car.transform.childCount];
        for (int i = 0; i < carWheel.Length; i++)
        {
            carWheel[i] = car.transform.GetChild(i);
        }
        titleButtons[0].onClick.AddListener(() => StartCoroutine(SceneMove(0)));
        titleButtons[1].onClick.AddListener(() => StartCoroutine(SceneMove(2)));
        titleButtons[2].onClick.AddListener(() => StartCoroutine(RankingDown(true)));
        rankingBack.onClick.AddListener(() => StartCoroutine(RankingDown(false)));
        carPos = car.transform.position;
        ranking.anchoredPosition = new Vector2(0, 1200);
        SoundManager.Instance.SetAudio(BGM, SoundManager.SoundState.BGM);
        InitRanking();
    }
    public void InitRanking()
    {
        var save = SceneManager.Instance.saveData;
        save.saveList = save.saveList.OrderBy(x => x.score).ToList();
        for (int i = 0; i < 5; i++) 
            rankingText[i].text = save.saveList.Count <= i ? "" : $"{save.saveList[i].name}       {save.saveList[i].score}";
    }
    IEnumerator SceneMove(int index)
    {
        var s = SceneManager.Instance;
        isMove = true;
        float t = 0;
        Vector3 pos1 = car.transform.position;
        Vector3 pos2 = pos1 + Vector3.right * 20;
        while (t <= 1f)
        {
            car.transform.position = Vector3.Lerp(pos1, pos2, Mathf.Pow(t, 3));
            print(t);
            t += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(s.fadeOpen(false)).OnComplete(() => s.StageStart(index));
    }
    IEnumerator RankingDown(bool isActive)
    {
        var y = isActive ? 0 : 1200;
        float t = 0;
        while (t < 1)
        {
            ranking.anchoredPosition = Vector3.Lerp(ranking.anchoredPosition, new Vector3(0, y, 0), Mathf.Pow(t, 3));
            t += Time.deltaTime;
            yield return null;
        }
    }
    private void Update()
    {
        foreach (var g in titleProp)
        {
            g.transform.position += Vector3.left * roadSpeed * Time.deltaTime;
            if (g.transform.position.x <= -35) g.transform.position += Vector3.right * 105;
        }
        foreach (var r in titleRoad)
        {
            r.transform.position += Vector3.left * roadSpeed * Time.deltaTime;
            if (r.transform.position.x <= -20) r.transform.position += Vector3.right * 120;
        }
        foreach (var c in carWheel)
        {
            c.transform.eulerAngles += Vector3.back * roadSpeed * 20 * Time.deltaTime;
        }
        if (!isMove)
            car.transform.position = carPos + (Vector3)Random.insideUnitCircle * 0.03f;
    }
}
