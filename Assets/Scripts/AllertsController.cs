using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllertsController : MonoBehaviour
{
    public GameObject moneyAllert;

    public void showMoneyAllert(int money)
    {
        GameObject allert = Instantiate(moneyAllert, transform);
        allert.transform.GetChild(0).GetComponent<Text>().text = "+" + money;
    }
}
