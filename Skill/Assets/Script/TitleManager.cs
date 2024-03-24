using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button[] titleButtons;

    [SerializeField] private Transform[] titleProp;
    [SerializeField] private Transform[] titleRoad;
    [SerializeField] private float roadSpeed;
    [SerializeField] private Transform car;
    private Transform[] carWheel;
    private Vector3 carPos;

    private void Start()
    {
        carWheel = new Transform[car.transform.childCount];
        for(int i = 0; i < carWheel.Length; i++)
        {
            carWheel[i] = car.transform.GetChild(i);
        }
        titleButtons[0].onClick.AddListener(() => StartCoroutine(StageStart()));
        carPos = car.transform.position;
    }
    IEnumerator StageStart()
    {
        float t = 0;
        Vector3 pos1 = car.transform.position;
        Vector3 pos2 = pos1 + Vector3.right * 20;
        while(t <= 2f)
        {
            car.transform.position = Vector3.Lerp(pos1,pos2,Mathf.Pow(t,3));
            t += Time.deltaTime;
            yield return null;
        }
        SceneManager.Instance.StageStart(0);
    }
    private void Update()
    {
        foreach(var g in titleProp)
        {
            g.transform.position += Vector3.left * roadSpeed * Time.deltaTime;
            if(g.transform.position.x <= -35) g.transform.position += Vector3.right * 105;
        }
        foreach(var r in titleRoad)
        {
            r.transform.position += Vector3.left * roadSpeed * Time.deltaTime;
            if(r.transform.position.x <= -20) r.transform.position += Vector3.right * 120;
        }
        foreach(var c in carWheel)
        {
            c.transform.eulerAngles += Vector3.back * roadSpeed * 20 * Time.deltaTime;
        }
        car.transform.position = carPos + (Vector3)Random.insideUnitCircle * 0.03f;
    }
}
