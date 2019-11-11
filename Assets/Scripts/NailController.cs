using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailController : MonoBehaviour
{
    private void Start()
    {
        EventController.eventController.onChangedNailSprite += changeNailSprite;
        changeNailSprite();
    }
    public void changeNailSprite()
    {
        GetComponent<SpriteRenderer>().sprite = EventController.eventController.getCurrentNailSprite();
    }
}
