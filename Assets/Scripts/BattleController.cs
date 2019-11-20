using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public GameObject nail;
    public const float NAIL_MAX = 0f;
    public const float NAIL_MIN = -3.5f;

    public Text curentPlayerText;
    public List<PlayerController> playingPlayers;
    public int curPlayer = 0;
    public bool isWinnerExist = false;

    public GameObject winnerText;

    public void startGame(bool isVsComputer)
    {
        StaticVars.isLastGameVsComputer = isVsComputer;
        playingPlayers = new List<PlayerController>();
        if(isVsComputer)
        {
            playingPlayers.Add(new PlayerController(StaticVars.singlePlayerName));
            playingPlayers.Add(new PlayerController(StaticVars.computerName, true));
        }
        else
        {
            playingPlayers.Add(new PlayerController(StaticVars.firstPlayerName));
            playingPlayers.Add(new PlayerController(StaticVars.secondPlayerName, false));
        }

        curentPlayerText.text = playingPlayers[curPlayer].name;
        nail.transform.localPosition += new Vector3(0, Random.Range(-1f, 0.5f), 0);
    }

    public void poseNail(float length)
    {
        if(length>0)
        {
            Debug.Log("WTF");
        }
        if(nail.transform.localPosition.y + length <= NAIL_MIN)
        {
            isWinnerExist = true;
            nail.transform.localPosition = new Vector3(0, NAIL_MIN, 0);
            winnerText.SetActive(true);
            EventController.eventController.powerLinear.transform.parent.gameObject.SetActive(false);
            if (Random.Range(0f, 1f) < StaticVars.ADVERT_WIN_CHANCE)
            {
                StaticVars.advertSaves--;
                EventController.eventController.showAdvert();
            }
            if(playingPlayers[1].isComputer)
            {
                if(curPlayer != 0)
                {
                    //defit
                    return;
                }
                switch(StaticVars.currentDificult)
                {
                    case StaticVars.COMPUTER_DIFFICULT.EASY:
                        StaticVars.winnsVsEasy++;
                        Debug.Log("WIN VS Easy " + StaticVars.winnsVsEasy);
                        break;
                    case StaticVars.COMPUTER_DIFFICULT.NORMAL:
                        StaticVars.winnsVsNormal++;
                        Debug.Log("WIN VS Normal " + StaticVars.winnsVsNormal);
                        break;
                    case StaticVars.COMPUTER_DIFFICULT.HARD:
                        StaticVars.winnsVsHard++;
                        Debug.Log("WIN VS Hard " + StaticVars.winnsVsHard);
                        break;
                    case StaticVars.COMPUTER_DIFFICULT.INSANE:
                        StaticVars.winnsVsInsane++;
                        Debug.Log("WIN VS Insane " + StaticVars.winnsVsInsane);
                        break;
                }
                EventController.eventController.getMoneyFromWinning(!playingPlayers[1].isComputer);
            }
            else
            {
                StaticVars.winnsVsFriend++;
                EventController.eventController.getMoneyFromWinning(!playingPlayers[1].isComputer);
                Debug.Log("WIN VS FRIEND " + StaticVars.winnsVsFriend);
            }
            StaticVars.save();
        }
        else
        {
            nail.transform.localPosition += new Vector3(0, length, 0);
        }   
    }
    public void endTurn()
    {
        if (playingPlayers.Count-1<=curPlayer)
        {
            curPlayer = 0;
        }
        else
        {
            curPlayer++;
        }
        updateCurentPlayerText();
    }

    private void updateCurentPlayerText()
    {
        curentPlayerText.text = playingPlayers[curPlayer].name;
    }
}
