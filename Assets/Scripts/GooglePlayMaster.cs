using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public static class GooglePlayMaster
{
    public static void initialize()
    {

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();


        signIn();
    }
    public static void signIn()
    {
        PlayGamesPlatform.Instance.Authenticate
        ( successSilent => 
        { 
            if(!successSilent)
            {
                PlayGamesPlatform.Instance.Authenticate(successNormal => {});
            }
        },true);
        return;
        if (!Social.localUser.authenticated)
        {
            
            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Debug.Log("success");
                }
                else
                {
                    Debug.Log("unSuccess");
                }
            });
        }
        else
        {
            Debug.Log("already Signed in");
        }
    }

    #region Achivements
    public static void unlockAchivement(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) => { });
    }
    public static void incrementAchivement(string id)
    {
        Debug.Log("Achivement "+ id);
        PlayGamesPlatform.Instance.IncrementAchievement(id, 1, success => { });
    }
    public static void showAchivementsUI()
    {
        Debug.Log("Achivements UI");
        signIn();
        Social.ShowAchievementsUI();
    }
    #endregion
}
