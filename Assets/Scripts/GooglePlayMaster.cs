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
        Debug.Log("Sign In");
        Social.localUser.Authenticate(success => { if (success) { Debug.Log("success"); } else { Debug.Log("unSuccess"); }  });
    }

    #region Achivements
    public static void incrementAchivement(string id)
    {
        Debug.Log("Achivement "+ id);
        PlayGamesPlatform.Instance.IncrementAchievement(id, 1, success => { });
    }
    public static void showAchivementsUI()
    {
        Debug.Log("Achivements UI");
        Social.ShowAchievementsUI();
    }
    #endregion
}
