using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public Text gemCountText;
    public Image selectionImage;
    public Text HUD_GemCount;
    public int gems;
    public Image[] lifeUnits;



    private void Awake()
    {
        _instance = this;
    }

    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }

            return _instance;
        }
    }

    public void UpdateShopSelection(int ypos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, ypos);
    }


    public void OpenShop()
    {
        gemCountText.text = "" + gems + " Gems";
    }

    public void UpdateGemCount()
    {
        HUD_GemCount.text = "" + gems;
        gemCountText.text = "" + gems;
    }

    public void UpdateLife(int Health)
    {
        for(int i = 0; i <= Health; i++)
        {
            if(i == Health)
            {
                lifeUnits[i].enabled = false;
            }
        }
    }
}