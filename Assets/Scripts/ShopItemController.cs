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
    private static Color SELECTED = new Color(255, 110, 4, 0.5f);
    private static ShopItemController selectedItem = null;
    private void Start()
    {
        StaticVars.onMoneyChange += redrawAwailables;
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
        redrawAwailables();
        if(StaticVars.nailID == id)
        {
            selectItem();
        }
    }
    public void buttonClick()
    {
        if(StaticVars.boughtNails[id])
        {
            StaticVars.nailID = id;
            selectItem();            
        }
        else
        {
            shop.buyItem(id);
            redrawBought();
        }
        StaticVars.save();
    }

    private void selectItem()
    {
        redrawItemToAwailable(selectedItem);
        selectedItem = this;
        redrawItemToSelected(this);
    }

    private void redrawBought()
    {
        lockImage.SetActive(!StaticVars.boughtNails[id]);
        if(StaticVars.boughtNails[id])
        {
            redrawItemToAwailable(this);
        }
    }
    private void redrawAwailables()
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

    private static void redrawItemToAwailable(ShopItemController item)
    {
        if(item==null)
        {
            return;
        }
        item.price.gameObject.SetActive(false);
        item.useText.SetActive(true);
        item.buyButton.GetComponent<Image>().color = BOUGHT;
    }
    private static void redrawItemToSelected(ShopItemController item)
    {
        if (item == null)
        {
            return;
        }
        item.buyButton.GetComponent<Image>().color = SELECTED;
    }
}
