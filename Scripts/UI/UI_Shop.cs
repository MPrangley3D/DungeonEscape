using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    public GameObject shopUI;
    public int selectedItem = 0;
    public int currentItemCost = 200;

    private Player UIManagerInstance;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UIManagerInstance = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            if (UIManagerInstance != null)
            {
                UIManager.Instance.OpenShop();
            }

            shopUI.SetActive(true);
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopUI.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch(item)
        {
            case 0:  //Fire Sword
                UIManager.Instance.UpdateShopSelection(60);
                selectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:  //Boots
                UIManager.Instance.UpdateShopSelection(-50);
                selectedItem = 1;
                currentItemCost = 500;
                break;
            case 2:  //Key
                UIManager.Instance.UpdateShopSelection(-150);
                selectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    private void SpendGems(int currentItemCost)
    {
        UIManager.Instance.gems -= currentItemCost;
        UIManager.Instance.UpdateGemCount();
        Debug.Log("Bought " + selectedItem + " for " + currentItemCost + " account left: " + UIManager.Instance.gems);
    }

    public void BuyItem()
    {
        if (UIManager.Instance.gems >= currentItemCost)
        {
            switch(selectedItem)
            {
                case 0:  //buy F Sword
                    if (GameManager.Instance.hasSword == false)
                    {
                        Debug.Log("Buy Sword");
                        SpendGems(currentItemCost);
                        GameManager.Instance.hasSword = true;
                        GameManager.Instance.upgradeSword();
                    }
                    break;
                case 1:  //Buy Boots
                    if (GameManager.Instance.hasBoots == false)
                    {
                        Debug.Log("Buy Boots");
                        SpendGems(currentItemCost);
                        GameManager.Instance.hasBoots = true;
                    }
                    break;
                case 2:  //Buy Key
                    if (GameManager.Instance.hasKey == false)
                    {
                        Debug.Log("Buy Key");
                        SpendGems(currentItemCost);
                        GameManager.Instance.hasKey = true;
                    }
                    break;
            }
        }
        else
        {
            Debug.Log("Too Poor, kill mo shit!");
            shopUI.SetActive(false);
        }
    }
}
