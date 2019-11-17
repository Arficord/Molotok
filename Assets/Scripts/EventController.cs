
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class EventController : MonoBehaviour
{    
    public enum MOVE
    {
        NONE,
        HIT_HARD,
        HIT_SOFT,
    }
    public enum Names
    {
        SINGLE,
        COMPUTER,
        FIRST,
        SECOND,
    }
    [HideInInspector]
    public static EventController eventController;
    public AllertsController allertMaster;
    public ShopController shopController;
    public BattleController battleController;
    public GameObject howToPlay_notice;
    public Image powerLinear;

    public float maxHitPower = 1f;
    private bool powerLinearPauseFlag = false;
    private Coroutine powerLinearScaleCoroutine;
    private IEnumerator powerLinearScaleIEnumerator()
    {
        powerLinear.fillAmount = 0;
        float powerLinearSpeed = 4;
        bool incrimentFlag = true;
        while (true)
        {
            while(powerLinearPauseFlag)
            {
                yield return null;
            }
            if(powerLinear.fillAmount<=0)
            {
                incrimentFlag = true;
            }
            if (powerLinear.fillAmount >= 1)
            {
                incrimentFlag = false;
            }

            if(incrimentFlag==true)
            {
                powerLinear.fillAmount += powerLinearSpeed * Time.deltaTime;
            }
            else
            {
                powerLinear.fillAmount -= powerLinearSpeed * Time.deltaTime;
            }


            yield return new WaitForEndOfFrame();
        }
    }

    public event System.Action changedLanguge;
    private MOVE computerMove = MOVE.HIT_SOFT;

    public InputField firstPlayerName;
    public InputField secoundPlayerName;
    public InputField singlePlayerName;

    public GameObject mainMenu;
    public GameObject mainMenuElements;
    public GameObject battleUI;
    public GameObject selectDificultFrame;
    public GameObject hamerModel;
    private Coroutine hamerHitCoroutine;

     private IEnumerator hamerHitIEnumerator()
     {
        float powerPercent = getPowerLinearFillAmount();
        if (powerPercent == 0)
        {
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_like_a_baby);
        }
        else
        if(powerPercent==1)
        {
            GooglePlayMaster.incrementAchivement(GPGSIds.achievement_one_punch);
        }
        return hamerHitIEnumerator(powerPercent);
     }
    private IEnumerator hamerHitIEnumerator(float fillAmount)
    {
        if(battleController.playingPlayers[battleController.curPlayer].isComputer ==false)
        {
            pausePowerLinearAnimation();
        }
        float hitPower = -maxHitPower * fillAmount;
        Vector3 hamerStartRotation = new Vector3(0, 0, 0);
        Vector3 hamerLastRotation = new Vector3(0, 0, 90);
        float speed = 100;
        for(float i=0; i<=20; i+=speed*Time.deltaTime)
        {
            hamerModel.transform.localRotation = Quaternion.Euler(Vector3.Lerp(hamerStartRotation, hamerLastRotation, (float)i / 20));
            float hamerYpos = battleController.nail.transform.localPosition.y + 1.9f;
            hamerModel.transform.localPosition =   new Vector3(hamerModel.transform.localPosition.x, hamerYpos, hamerModel.transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
        hamerModel.transform.localRotation = Quaternion.Euler(hamerLastRotation);

        if (battleController.playingPlayers[battleController.curPlayer].isComputer == false)
        {
            startPowerLinearAnimation();
        }

        battleController.poseNail(hitPower);
        if(!battleController.isWinnerExist)
        {
            battleController.endTurn();
        }

        for (float i = 0; i <= 20; i += speed * Time.deltaTime)
        {
            hamerModel.transform.localRotation = Quaternion.Euler(Vector3.Lerp(hamerLastRotation, hamerStartRotation, (float)i / 20));
            yield return new WaitForEndOfFrame();
        }
        hamerHitCoroutine = null;
    }

    public bool isPaused = true;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        eventController = this;
        mainMenu.SetActive(StaticVars.isMenuActive);
        battleUI.SetActive(StaticVars.isBattleUiActive);
        if(!StaticVars.isMenuActive)
        {
            startButtle(StaticVars.isLastGameVsComputer);
        }
    }
    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3318745", false);
        }
        firstPlayerName.text = StaticVars.firstPlayerName;
        secoundPlayerName.text = StaticVars.secondPlayerName;
        singlePlayerName.text = StaticVars.singlePlayerName;
        startPowerLinearAnimation();
        poseSelectedDificult((int)StaticVars.currentDificult);

        GooglePlayMaster.initialize();
    }

    void Update()
    {
        if(isPaused)
        {
            return;
        }
        if (battleController.isWinnerExist)
        {
            return;
        }
        if (hamerHitCoroutine != null)
        {
            return;
        }

        if (battleController.playingPlayers[battleController.curPlayer].isComputer==false)
        {
            if (Input.anyKeyDown)
            {
                hamerHitCoroutine = StartCoroutine(hamerHitIEnumerator());
            }
        }
        else
        {
            if(computerMove==MOVE.NONE)
            {
                if (battleController.nail.transform.localPosition.y - maxHitPower *(1 - StaticVars.computerHitRange) <= BattleController.NAIL_MIN)
                {
                    computerMove = MOVE.HIT_HARD;
                }
                else
                {
                    computerMove = MOVE.HIT_SOFT;
                }

            }
            else
            {
                switch (computerMove)
                {
                    case MOVE.HIT_HARD:
                        hamerHitCoroutine = StartCoroutine(hamerHitIEnumerator(Random.Range(1 - StaticVars.computerHitRange, 1)));
                        computerMove = MOVE.NONE;
                        break;
                    case MOVE.HIT_SOFT:
                        float neededStranght = ((battleController.nail.transform.localPosition.y - BattleController.NAIL_MIN) / maxHitPower) % 1;
                        hamerHitCoroutine = StartCoroutine(hamerHitIEnumerator(Random.Range(neededStranght, neededStranght + StaticVars.computerHitRange)));
                        computerMove = MOVE.NONE;
                        break;
                }
            }
        }
    }

    public void restartGame()
    {
        StaticVars.isMenuActive = false;
        StaticVars.clearOnMoneyChange();
        Application.LoadLevel(Application.loadedLevel);
    }
    public void backToMenu()
    {
        StaticVars.isMenuActive = true;
        StaticVars.clearOnMoneyChange();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void startButtle(bool isVsComputer)
    {
        battleUI.SetActive(true);
        mainMenu.SetActive(false);
        mainMenuElements.SetActive(false);
        battleController.startGame(isVsComputer);
        isPaused = false;
    }

    public void setActive_HowToPlay(bool flag)
    {
        howToPlay_notice.SetActive(flag);
    }

    public float getPowerLinearFillAmount()
    {
        return powerLinear.fillAmount;
    }

    public void changeSinglePlayeName(string name)
    {
       StaticVars.singlePlayerName = name;
    }
    public void changeComputerName(string name)
    {
        StaticVars.computerName = name;
    }
    public void changeFirstPlayerName(string name)
    {
        StaticVars.firstPlayerName = name;
    }
    public void changeSecondPlayerName(string name)
    {
        StaticVars.secondPlayerName = name;
    }

    public void changeLanguge()
    {
        if((int)StaticVars.languge<2)
        {
            StaticVars.languge++;
        }
        else
        {
            StaticVars.languge = 0;
        }
        if(changedLanguge!=null)
        {
            changedLanguge();
        }
        
    }
    public void poseSelectedDificult(GameObject sender)
    {
        selectDificultFrame.transform.localPosition = sender.transform.localPosition;
    }
    public void poseSelectedDificult(int id)
    {
        selectDificultFrame.transform.localPosition = new Vector3(-135 + (90 * (id-1)), -23.1f, 0);
    }
    public void changeDificult(int dificult)
    {
        StaticVars.changeDificult((StaticVars.COMPUTER_DIFFICULT)dificult);
    }

    public void showAchivements()
    {
        GooglePlayMaster.showAchivementsUI();
    }

    public void showAdvert()
    {
        if(StaticVars.advertSaves<=0)
        {
            StaticVars.advertSaves = 2;
            if( Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    }


    public event System.Action onChangedNailSprite;
    public void changedNailSprite()
    {
        if(onChangedNailSprite!=null)
        {
            onChangedNailSprite();
        }
    }
    public Sprite getCurrentNailSprite()
    {
        return shopController.getSprite(StaticVars.nailID);
    }


    public void getMoneyFromWinning(bool isVsPlayer)
    {
        int moneyToGet=0;
        if(isVsPlayer)
        {
            moneyToGet = 5;
        }
        else
        {
            switch (StaticVars.currentDificult)
            {
                case StaticVars.COMPUTER_DIFFICULT.VERYEASY:
                    {
                        moneyToGet = 1;
                        break;
                    }
                case StaticVars.COMPUTER_DIFFICULT.EASY:
                    {
                        moneyToGet = 5;
                        break;
                    }
                case StaticVars.COMPUTER_DIFFICULT.NORMAL:
                    {
                        moneyToGet = 10;
                        break;
                    }
                case StaticVars.COMPUTER_DIFFICULT.HARD:
                    {
                        moneyToGet = 15;
                        break;
                    }
                case StaticVars.COMPUTER_DIFFICULT.INSANE:
                    {
                        moneyToGet = 20;
                        break;
                    }
            }
        }

        allertMaster.showMoneyAllert(moneyToGet);
        StaticVars.money += moneyToGet;
    }
    public void startPowerLinearAnimation()
    {
        if(powerLinearScaleCoroutine==null)
        {
            powerLinearScaleCoroutine = StartCoroutine(powerLinearScaleIEnumerator());
        }
        else
        {
            powerLinearPauseFlag = false;
        }
    }
    public void pausePowerLinearAnimation()
    {
        powerLinearPauseFlag = true;
    }
}


