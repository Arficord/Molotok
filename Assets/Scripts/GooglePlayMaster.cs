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
        Social.localUser.Authenticate(success => { });
    }

    #region Achivements
    public static void incrementAchivement(string id)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, 1, success => { });
    }
    public static void showAchivementsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion
}
