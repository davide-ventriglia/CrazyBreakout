using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance;
    AllLeaderboardData leaderboardData;
    public LeaderboardData ClassicLeaderboardData {get; private set;}
    public LeaderboardData CrazyLeaderboardData {get; private set;}
    static string data;


    void Awake() {
        // singleton pattern
        if(Instance!=null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Using coroutine to gather data almost from beginning of game
    void Start(){
        //getting leaderboard data
        StartCoroutine(GetLeaderboardData());
        RefreshLeaderboards();
    }
    IEnumerator GetLeaderboardData(){
        FirebaseDatabaseJSBridge.GetLeaderboardData("Data",gameObject.name,"LocallyCopyLeaderboardData", "DisplayError");
        yield return new WaitForSeconds(3.0f);
    }
    void LocallyCopyLeaderboardData(string jsonData){
        data = jsonData;
    }
    public void DisplayError(string error){
        Debug.Log(error);
    }


    public void RefreshLeaderboards()
    {
        FirebaseDatabaseJSBridge.GetLeaderboardData("Data",gameObject.name,"LocallyCopyLeaderboardData", "DisplayError");

        leaderboardData = JsonUtility.FromJson<AllLeaderboardData>(data);
        ClassicLeaderboardData = OrderData(leaderboardData.ClassicLeaderboardData);
        CrazyLeaderboardData = OrderData(leaderboardData.CrazyLeaderboardData);
    }

    LeaderboardData OrderData(LeaderboardData data){
        data.userDetailsList = data.userDetailsList.OrderByDescending(x => x.score).ToList();
        return data;
    }

    public void SaveNewPlayer(LeaderboardData leaderboard, string leaderboardName, User user){
        int leaderboardIndex = leaderboard.userDetailsList.Count;
        string newRecord = JsonUtility.ToJson(user);
        string path = "Data/"+leaderboardName+"/userDetailsList/"+leaderboardIndex.ToString();
        FirebaseDatabaseJSBridge.SaveNewPlayer(path,newRecord,gameObject.name,"DisplayError");
        RefreshLeaderboards();
    }

}