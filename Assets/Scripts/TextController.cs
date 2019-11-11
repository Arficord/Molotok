using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public enum TEXT_CODE
    {
        PLAYER_VS_COMPUTER__MAIN_MENU = 0,
        PLAYER_VS_PLAYER__MAIN_MENU = 1,
        HOW_TO_PLAY__MAIN_MENU = 2,
        HOW_TO_PLAY__RULE1 = 3,
        HOW_TO_PLAY__RULE2 = 4,
        HOW_TO_PLAY__RULE3 = 5,
        PLAYER_FIRST = 6,
        PLAYER_SECOUND =7,
        PLAYER_SOLO =8,
        DIFFICULT = 9,
        IS_THE_WINNER =10,
        POWER = 11,
        SHOP = 12,
        THX_ADV = 13,
        BUY = 14,
        USE =15,
    }
    public TEXT_CODE textID;

    void Start()
    {
        redraw();
        EventController.eventController.changedLanguge += redraw;
    }

    public void redraw()
    {
        switch(StaticVars.languge)
        {
            case LANGUGE.ENGLISH:
                GetComponent<Text>().text = getText_Eng(textID);
                break;
            case LANGUGE.RUSSIAN:
                GetComponent<Text>().text = getText_Rus(textID);
                break;
            case LANGUGE.POLISH:
                GetComponent<Text>().text = getText_Pol(textID);
                break;
            default:
                GetComponent<Text>().text = getText_Eng(textID);
                break;
        }
        
    }
    public string getText_Eng(TEXT_CODE code)
    {
        switch(code)
        {
            case TEXT_CODE.PLAYER_VS_COMPUTER__MAIN_MENU:
                return "PLAYER VS COMPUTER";
            case TEXT_CODE.PLAYER_VS_PLAYER__MAIN_MENU:
                return "PLAYER VS PLAYER";
            case TEXT_CODE.HOW_TO_PLAY__MAIN_MENU:
                return "HOW TO PLAY";
            case TEXT_CODE.HOW_TO_PLAY__RULE1:
                return "Play in turns";
            case TEXT_CODE.HOW_TO_PLAY__RULE2:
                return "Touch to hammer";
            case TEXT_CODE.HOW_TO_PLAY__RULE3:
                return "Hammer the nail to win!";
            case TEXT_CODE.PLAYER_FIRST:
                return "Player 1 :";
            case TEXT_CODE.PLAYER_SECOUND:
                return "Player 2 :";
            case TEXT_CODE.PLAYER_SOLO:
                return "Player :";
            case TEXT_CODE.DIFFICULT:
                return "Difficult";
            case TEXT_CODE.IS_THE_WINNER:
                return "Is the winner!";
            case TEXT_CODE.POWER:
                return "POWER";
            case TEXT_CODE.SHOP:
                return "SHOP";
            case TEXT_CODE.THX_ADV:
                return "THANKS FOR WATCHING THE ADVERT";
            case TEXT_CODE.BUY:
                return "BUY";
            case TEXT_CODE.USE:
                return "USE";
            default:
                return "NULL_ENG";
        }
    }
    public string getText_Rus(TEXT_CODE code)
    {
        switch (code)
        {
            case TEXT_CODE.PLAYER_VS_COMPUTER__MAIN_MENU:
                return "ИГРАТЬ С КОМПЬЮТЕРОМ";
            case TEXT_CODE.PLAYER_VS_PLAYER__MAIN_MENU:
                return "ИГРАТЬ С ДРУГОМ";
            case TEXT_CODE.HOW_TO_PLAY__MAIN_MENU:
                return "КАК ИГРАТЬ";
            case TEXT_CODE.HOW_TO_PLAY__RULE1:
                return "Игра идет по ходам";
            case TEXT_CODE.HOW_TO_PLAY__RULE2:
                return "Нажми что-бы ударить гвоздь";
            case TEXT_CODE.HOW_TO_PLAY__RULE3:
                return "Забей что-бы победить";
            case TEXT_CODE.PLAYER_FIRST:
                return "Игрок 1 :";
            case TEXT_CODE.PLAYER_SECOUND:
                return "Игрок 2 :";
            case TEXT_CODE.PLAYER_SOLO:
                return "Игрок :";
            case TEXT_CODE.DIFFICULT:
                return "Сложность";
            case TEXT_CODE.IS_THE_WINNER:
                return "- Победитель!";
            case TEXT_CODE.POWER:
                return "Сила";
            case TEXT_CODE.SHOP:
                return "МАГАЗИН";
            case TEXT_CODE.THX_ADV:
                return "Благодарю, за просмотр рекламы";
            case TEXT_CODE.BUY:
                return "КУПИТЬ";
            case TEXT_CODE.USE:
                return "ВЫБРАТЬ";
            default:
                return getText_Eng(code);
        }
    }
    public string getText_Pol(TEXT_CODE code)
    {
        switch (code)
        {
            case TEXT_CODE.PLAYER_VS_COMPUTER__MAIN_MENU:
                return "GRAJ Z KOMPUTEREM";
            case TEXT_CODE.PLAYER_VS_PLAYER__MAIN_MENU:
                return "GRAJ Z PRZYJACIELEM";
            case TEXT_CODE.HOW_TO_PLAY__MAIN_MENU:
                return "JAK GRAĆ";
            case TEXT_CODE.HOW_TO_PLAY__RULE1:
                return "Graj po kolei";
            case TEXT_CODE.HOW_TO_PLAY__RULE2:
                return "Dotkni żeby uderzyć";
            case TEXT_CODE.HOW_TO_PLAY__RULE3:
                return "Zabij żeby wygrać";
            case TEXT_CODE.PLAYER_FIRST:
                return "Gracz 1 :";
            case TEXT_CODE.PLAYER_SECOUND:
                return "Gracz 2 :";
            case TEXT_CODE.PLAYER_SOLO:
                return "Gracz :";
            case TEXT_CODE.DIFFICULT:
                return "Poziom";
            case TEXT_CODE.IS_THE_WINNER:
                return "- Wygrał!";
            case TEXT_CODE.POWER:
                return "SIŁA";
            case TEXT_CODE.SHOP:
                return "SKLEP";
            case TEXT_CODE.THX_ADV:
                return "Dziękuje za oglandanie reklamy";
            case TEXT_CODE.BUY:
                return "KUP";
            case TEXT_CODE.USE:
                return "UŻYJ";
            default:
                return getText_Eng(code);
        }
    }
}
