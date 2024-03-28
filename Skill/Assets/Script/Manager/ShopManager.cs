using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopButtons
{
    public Button button;
    public GameObject item;
}
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    public Transform carStand;
    public float standSpeed;
    [Header("PanelBtn")]
    public Button[] Panelbtn;
    public GameObject[] Panels;
    [Header("ItemBtn")]
    public ShopButtons[] tire;
    public ShopButtons[] engine;
    public ShopButtons[] decoration;
    public Texture2D[] textures;
    public Transform rotatePos;
    [Space(10)]

    public ShopButtons SelectObj;
    private void Start()
    {
        InitItemBtn();
        InitPanelBtn();
        DisableItem();
        DisablePanel();
        Panels[0].SetActive(true);
        
        var s = SceneManager.Instance;
        StartCoroutine(s.fadeOpen(true));
    }
    public void InitPanelBtn()
    {
        for (int i = 0; i < Panelbtn.Length; i++)
        {
            var num = i;
            Panelbtn[num].onClick.AddListener(() =>
            {
                DisableItem();
                DisablePanel();
                Panels[num].SetActive(true);
            });
        }
    }
    public void InitItemBtn()
    {
        for (int i = 0; i < tire.Length; i++)
        {
            var num = i;
            tire[num].button.onClick.AddListener(() =>
            {
                DisableItem();
                tire[num].item.SetActive(true);
                SelectObj = tire[num];
            });
        }
        for (int i = 0; i < engine.Length; i++)
        {
            var num = i;
            engine[num].button.onClick.AddListener(() =>
            {
                DisableItem();
                engine[num].item.SetActive(true);
                SelectObj = engine[num];
            });
        }
        for (int i = 0; i < tire.Length; i++)
        {
            var num = i;
            decoration[num].button.onClick.AddListener(() =>
            {
                DisableItem();
                decoration[num].item.SetActive(true);
                SelectObj = tire[num];
            });
        }
    }
    public void DisablePanel()
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].gameObject.SetActive(false);
        }
    }
    public void DisableItem()
    {
        for (int i = 0; i < rotatePos.childCount; i++)
        {
            rotatePos.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        carStand.Rotate(Vector3.up * Time.deltaTime * standSpeed);
        rotatePos.Rotate(new Vector3(1,1,0) * Time.deltaTime * standSpeed);
    }
}
