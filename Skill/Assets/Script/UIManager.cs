using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TMP_Text countText;
    private void Awake()
    {
        instance = this;
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
}
