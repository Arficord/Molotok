using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]
public class ShopItem
{
    public Sprite sprite;
    public int price;
    public string name;
    //  public string autor;
}
public class ShopController : MonoBehaviour
{
    public ShopItem[] items;
    public GameObject itemUI;
    private List<GameObject> itemUIs;
    public ScrollRect scrollView;
    public RectTransform content;
    public Text moneyText;



    private void Start()
    {
        ShopItemController.shop = this;
        itemUIs = new List<GameObject>();
        updateMoney();
        StaticVars.onMoneyChange += updateMoney;
        DrowShop();
    }
    public Sprite getSprite(int id)
    {
        if(id>=items.Length || id<0)
        {
            return items[0].sprite;
        }
        return items[id].sprite;
    }

    public void DrowShop()
    {
        content.sizeDelta=new Vector2(content.sizeDelta.x, (135) * (items.Length-2));
        for(int i=0;i<items.Length;i++)
        {
            itemUIs.Add(Instantiate(itemUI));
            itemUIs[i].transform.SetParent(content.transform);
            itemUIs[i].transform.localPosition = new Vector3(0, (-135 )* i, 0);
            itemUIs[i].transform.localScale = Vector3.one;
            itemUIs[i].GetComponent<ShopItemController>().set(i);
        }
    }
    public void updateMoney()
    {
        if (moneyText != null)
        {
            moneyText.text = StaticVars.money.ToString();
        }
        else
        {
            Debug.Log("BRED --- BRED --- BRED --- BRED --- BRED --- BRED --- BRED ---  BRED --- BRED --- BRED --- BRED --- BRED --- BRED --- BRED ---");
        }
    }

    public void buyItem(int id)
    {
        if(StaticVars.boughtNails[id]==true)
        {
            Debug.Log("Bought already");
            return;
        }
        if(items[id].price<=StaticVars.money)
        {
            StaticVars.money -= items[id].price;
            StaticVars.boughtNails[id] = true;
        }
        else
        {
            Debug.Log("NO MONEY");
        }
    }
}
