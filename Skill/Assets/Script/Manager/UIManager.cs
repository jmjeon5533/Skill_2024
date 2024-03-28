using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TMP_Text countText;
    [SerializeField] Image showImages;
    [SerializeField] RectTransform showParent;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        var s = SceneManager.Instance;
        if(s != null) StartCoroutine(s.fadeAlpha(0));
        showParent.anchoredPosition = new Vector2(-100, 1200);
    }
    public void Count(int count)
    {
        if(count <= 3 && count >= 1)
        {
            countText.text = count.ToString();
        }
        else
        {
            countText.text = "";
        }
    }
    public void AddShowImage(string UpgradeExplain, Color color)
    {
        print("t");
        var img = Instantiate(showImages,showParent);
        var text = img.transform.GetChild(0).GetComponent<Text>();
        img.color = color;
        text.text = UpgradeExplain;
    }
    public IEnumerator ShowUpgrade()
    {
        print("show");
        float t = 0;
        while(t < 1)
        {
            print(t);
            t += Time.deltaTime;
            showParent.anchoredPosition = Vector3.Lerp(showParent.anchoredPosition,new Vector2(-100,0),Mathf.Pow(t,3));
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        t = 0;
        while(t < 1)
        {
            print(t);
            t += Time.deltaTime;
            showParent.anchoredPosition = Vector3.Lerp(showParent.anchoredPosition,new Vector2(-100,-1200),Mathf.Pow(t,3));
            yield return null;

        }
    }
}
