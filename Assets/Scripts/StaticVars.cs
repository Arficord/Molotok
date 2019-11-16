using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public enum LANGUGE
{
    ENGLISH,
    RUSSIAN,
    POLISH,
}
public static class StaticVars
{
    public static bool isMenuActive = true;
    public static bool isLastGameVsComputer = false;
    public static bool isBattleUiActive = false;
    public static float computerHitRange = 0.30f;

    public const float ADVERT_WIN_CHANCE = 0.3f;

    public enum COMPUTER_DIFFICULT
    {
        VERYEASY,
        EASY,
        NORMAL,
        HARD,
        INSANE,
    }
    public const float COMPUTER_INSANE = 0.1f;
    public const float COMPUTER_HARD = 0.13f;
    public const float COMPUTER_NORMAL = 0.15f;
    public const float COMPUTER_EASY = 0.2f;
    public const float COMPUTER_VERYEASY = 0.30f;

    static StaticVars()
    {
        load();
    }

    private static LANGUGE _languge = LANGUGE.ENGLISH;
    public static LANGUGE languge
    {
        get
        {
            return _languge;
        }
        set
        {
            _languge = value;
            PlayerPrefs.SetInt("languge", (int)languge);
        }
    }
    public static COMPUTER_DIFFICULT currentDificult = COMPUTER_DIFFICULT.EASY;
    public static string firstPlayerName = "First";
    public static string secondPlayerName = "Second";
    public static string singlePlayerName = "You";
    public static string computerName = "Android";
    public static List<bool> boughtNails;

    private static int _nailID = 0;
    public static int nailID
    {
        get
        {
           return _nailID;
        }
        set
        {
            _nailID = value;
            Debug.Log("New ID:" + value);
            EventController.eventController.changedNailSprite();
        }
    }
    private static int _winnsVsEasy = 0;
    public static int winnsVsEasy 
    {
        get
        {
            return _winnsVsEasy;
        }
        set
        {
            _winnsVsEasy = value;
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_computer);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_computer_beginner);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_computer_advanced);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_computer_master);
        }
    }
    private static int _winnsVsNormal = 0;
    public static int winnsVsNormal
    {
        get
        {
            return _winnsVsNormal;
        }
        set
        {
            _winnsVsNormal = value;
            winnsVsEasy++;
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_normal_computer);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_normal_computer_beginner);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_normal_computer_advanced);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_normal_computer_master);
        }
    }
    private static int _winnsVsHard = 0;
    public static int winnsVsHard
    {
        get
        {
            return _winnsVsHard;
        }
        set
        {
            _winnsVsHard = value;
            winnsVsNormal++;
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_difficult_computer);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_difficult_computer_beginner);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_difficult_computer_advanced);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_difficult_computer_master);
        }
    }
    private static int _winnsVsInsane = 0;
    public static int winnsVsInsane
    {
        get
        {
            return _winnsVsEasy;
        }
        set
        {
            _winnsVsInsane = value;
            winnsVsHard++;
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_insane_computer);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_insane_computer_beginner);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_insane_computer_advanced);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_insane_computer_master);
        }
    }
    private static int _winnsVsFriend = 0;
    public static int winnsVsFriend
    {
        get
        {
            return _winnsVsFriend;
        }
        set
        {
            _winnsVsFriend = value;
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_friend);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_friend_beginner);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_friend_advanced);
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_defeat_friend_master);
        }
    }
    public static int advertSaves = 2;
    private static int _money = 300;
    public static int money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            if (onMoneyChange!=null)
            {
                onMoneyChange();
            }
        }
    }
    public static event Action onMoneyChange;
    public static void clearOnMoneyChange()
    {
        onMoneyChange = null;
    }
    public static void changeDificult(StaticVars.COMPUTER_DIFFICULT dificult)
    {
        StaticVars.currentDificult = dificult;
        switch (dificult)
        {
            case StaticVars.COMPUTER_DIFFICULT.VERYEASY:
                StaticVars.computerHitRange = StaticVars.COMPUTER_VERYEASY;
                break;
            case StaticVars.COMPUTER_DIFFICULT.EASY:
                StaticVars.computerHitRange = StaticVars.COMPUTER_EASY;
                break;
            case StaticVars.COMPUTER_DIFFICULT.NORMAL:
                StaticVars.computerHitRange = StaticVars.COMPUTER_NORMAL;
                break;
            case StaticVars.COMPUTER_DIFFICULT.HARD:
                StaticVars.computerHitRange = StaticVars.COMPUTER_HARD;
                break;
            case StaticVars.COMPUTER_DIFFICULT.INSANE:
                StaticVars.computerHitRange = StaticVars.COMPUTER_INSANE;
                break;
        }
    }

    public static void save()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetString("firstPlayerName", firstPlayerName);
        PlayerPrefs.SetString("secondPlayerName", secondPlayerName);
        PlayerPrefs.SetString("singlePlayerName", singlePlayerName);
        PlayerPrefs.SetInt("winnsVsEasy", winnsVsEasy);
        PlayerPrefs.SetInt("winnsVsNormal", winnsVsNormal);
        PlayerPrefs.SetInt("winnsVsHard", winnsVsHard);
        PlayerPrefs.SetInt("winnsVsInsane", winnsVsInsane);
        PlayerPrefs.SetInt("winnsVsFriend", winnsVsFriend);
        PlayerPrefs.SetInt("languge",(int)languge);
        PlayerPrefs.SetInt("nailID", nailID);
        PlayerPrefs.SetString("boughtNailsCode", convertBoolString(boughtNails));

        Debug.Log("SAVING:");
        Debug.Log("money = " + money);
        Debug.Log("nail = " + nailID);
        Debug.Log("Bought size " + boughtNails.Count);
    }
    public static void load()
    {
        //PlayerPrefs.DeleteAll(); //TODO УДАЛЯЕТ ВСЕ
        money = PlayerPrefs.GetInt("money", money); ;
        firstPlayerName = PlayerPrefs.GetString("firstPlayerName", firstPlayerName);
        secondPlayerName = PlayerPrefs.GetString("secondPlayerName", secondPlayerName);
        singlePlayerName = PlayerPrefs.GetString("singlePlayerName", singlePlayerName);
        winnsVsEasy = PlayerPrefs.GetInt("winnsVsEasy", winnsVsEasy);
        winnsVsNormal = PlayerPrefs.GetInt("winnsVsNormal", winnsVsNormal);
        winnsVsHard = PlayerPrefs.GetInt("winnsVsHard", winnsVsHard);
        winnsVsInsane = PlayerPrefs.GetInt("winnsVsInsane", winnsVsInsane);
        winnsVsFriend = PlayerPrefs.GetInt("winnsVsFriend", winnsVsFriend);
        languge = (LANGUGE)PlayerPrefs.GetInt("languge", (int)languge);
        nailID = PlayerPrefs.GetInt("nailID", nailID);


        const int NAILS_COUNT = 7;

        boughtNails = convertStringBool(PlayerPrefs.GetString("boughtNailsCode"));
        if (boughtNails.Count==0)
        {
            boughtNails.Add(true);
        }
        while (boughtNails.Count<NAILS_COUNT)
        {
            boughtNails.Add(false);
        }


        Debug.Log("LOADING:");
        Debug.Log("money = " + money);
        Debug.Log("nail = " + nailID);
        Debug.Log("Bought size " + boughtNails.Count);
    }

    public static string convertBoolString(List<bool> arr)
    {
        string res = "";
        foreach(bool i in arr)
        {
            if(i)
            {
                res += '1';
            }
            else
            {
                res += '0';
            }
        }
        return res;
    }
    public static List<bool> convertStringBool(string str)
    {
        List<bool> res = new List<bool>();
        foreach(char i in str)
        {
            res.Add(i=='1');
        }
        return res;
    }
}
