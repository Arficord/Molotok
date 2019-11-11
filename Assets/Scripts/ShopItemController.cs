using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public static ShopController shop;
    public Image nailImage;
    public Text name;
    public Text price;
    public GameObject useText;
    public Button buyButton;
    public GameObject lockImage;
    
    private int id;

    private static Color AWAILABLE_TO_BUY = new Color(0, 255, 0, 1);
    private static Color NOT_AWAILABLE_TO_BUY = new Color(255, 0, 0, 1);
    private static Color BOUGHT = new Color(255, 255, 0, 1);
    private void Start()
    {
        StaticVars.onMoneyChange += redrawAwailebles;
    }

    public void set(int id)
    {
        this.id = id;
        nailImage.sprite = shop.getSprite(id);
        name.text = shop.items[id].name;
        price.text = shop.items[id].price.ToString();


        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(buttonClick);
        redrawBought();
        redrawAwailebles();
    }
    public void buttonClick()
    {
        if(StaticVars.boughtNails[id])
        {
            StaticVars.nailID = id;
        }
        else
        {
            shop.buyItem(id);
            redrawBought();
        }
        StaticVars.save();
    }
    private void redrawBought()
    {
        lockImage.SetActive(!StaticVars.boughtNails[id]);
        if(StaticVars.boughtNails[id])
        {
            price.gameObject.SetActive(false);
            useText.SetActive(true);
            buyButton.GetComponent<Image>().color = BOUGHT;
        }
    }
    private void redrawAwailebles()
    {
        if (StaticVars.boughtNails[id])
        {
            return;
        }
        if(StaticVars.money>=shop.items[id].price)
        {
            buyButton.GetComponent<Image>().color = AWAILABLE_TO_BUY;
        }
        else
        {
            buyButton.GetComponent<Image>().color = NOT_AWAILABLE_TO_BUY;
        }
    }
}
