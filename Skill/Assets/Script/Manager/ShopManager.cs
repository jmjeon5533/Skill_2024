using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopButtons
{
    public Button button;
    public GameObject item;
    [HideInInspector] public bool isBuy;
    public int price;
}
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    public Transform carStand;
    public float standSpeed;
    [Header("카테고리 선택 버튼")]
    public Button[] Panelbtn;
    public GameObject[] Panels;
    [Header("아이템 선택 버튼")]
    public ShopButtons[] tire;
    public ShopButtons[] engine;
    public ShopButtons[] decoration;
    public Material[] material;
    public Transform rotatePos;
    [Space(10)]
    [SerializeField] TMP_Text moneyText;
    
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
            tire[i].button.onClick.AddListener(() =>
            {
                DisableItem();
                tire[num].item.SetActive(true);
                if(SelectObj == tire[num] && (SceneManager.Instance.money >= tire[num].price || tire[num].isBuy))
                {
                    BuyItem(tire[num]);
                    SceneManager.Instance.tuning.tireState = (Tuning.TireState)num + 1;
                }
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
                if(SelectObj == engine[num] && (SceneManager.Instance.money >= engine[num].price || engine[num].isBuy))
                {
                    BuyItem(engine[num]);
                    SceneManager.Instance.tuning.engineState = (Tuning.EngineState)num + 1;
                }
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
                if(SelectObj == decoration[num] && (SceneManager.Instance.money >= decoration[num].price || decoration[num].isBuy))
                {
                    BuyItem(decoration[num]);
                    SceneManager.Instance.material = material[num];
                }
                SelectObj = tire[num];
            });
        }
    }
    public void BuyItem(ShopButtons button)
    {
        SceneManager.Instance.money -= button.price;
        button.isBuy = true;
        button.button.image.color = Color.gray;
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
        moneyText.text = $"{SceneManager.Instance.money * 1000:#,##0}$";
    }
}
