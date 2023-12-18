using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class DBManager : MonoBehaviour
{
    // DatabaseReference databaseReference;
    AllLeaderboardData leaderboardData;
    public static DBManager Instance;
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
        FirebaseDatabaseJSBridge.GetData("Data",gameObject.name,"GetData", "DisplayError");
        yield return new WaitForSeconds(3.0f);
    }
    void GetData(string jsonData){
        data = jsonData;
    }
    public void DisplayError(string error){
        Debug.Log(error);
    }


    public void RefreshLeaderboards()
    {
        FirebaseDatabaseJSBridge.GetData("Data",gameObject.name,"GetData", "DisplayError");

        leaderboardData = JsonUtility.FromJson<AllLeaderboardData>(data);
        ClassicLeaderboardData = OrderData(leaderboardData.ClassicLeaderboardData);
        CrazyLeaderboardData = OrderData(leaderboardData.CrazyLeaderboardData);
    }

    LeaderboardData OrderData(LeaderboardData data){
        data.userDetailsList = data.userDetailsList.OrderByDescending(x => x.score).ToList();
        return data;
    }

    public void SaveRecord(LeaderboardData leaderboard, string leaderboardName, User user){
        int leaderboardIndex = leaderboard.userDetailsList.Count;
        string newRecord = JsonUtility.ToJson(user);
        string path = "Data/"+leaderboardName+"/userDetailsList/"+leaderboardIndex.ToString();
        FirebaseDatabaseJSBridge.SaveRecord(path,newRecord,gameObject.name,"DisplayError");
        RefreshLeaderboards();
    }

}


// public class DBManager : MonoBehaviour
// {
//     DatabaseReference databaseReference;
//     AllLeaderboardData leaderboardData;
//     public static DBManager Instance;
//     public LeaderboardData ClassicLeaderboardData {get; private set;}
//     public LeaderboardData CrazyLeaderboardData {get; private set;}


//     void Awake() {
//         // singleton pattern
//         if(Instance!=null){
//             Destroy(gameObject);
//             return;
//         }

//         // getting leaderboard data
//         databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
//         RefreshLeaderboards();

//         Instance = this;
//         DontDestroyOnLoad(gameObject);
//     }

//     void RefreshLeaderboards(){
//         StartCoroutine(GetLeaderboardData((string data) => {
//             leaderboardData = JsonUtility.FromJson<AllLeaderboardData>(data);
//             ClassicLeaderboardData = OrderData(leaderboardData.ClassicLeaderboardData);
//             CrazyLeaderboardData = OrderData(leaderboardData.CrazyLeaderboardData);
//         }));
//     }

//     IEnumerator GetLeaderboardData(Action<string> getLeaderboardData){
//         var data = databaseReference.GetValueAsync();
//         yield return new WaitUntil(predicate: ()=> data.IsCompleted);

//         if(data!=null){
//             DataSnapshot snapshot = data.Result;
//             getLeaderboardData.Invoke(snapshot.GetRawJsonValue());
//         }

//     }

//     public void DisplayData(string data)
//     {
//         testText.text = data;
//         Debug.Log(data);
//     }

//     LeaderboardData OrderData(LeaderboardData data){
//         data.userDetailsList = data.userDetailsList.OrderByDescending(x => x.score).ToList();
//         return data;
//     }

//     public void SaveRecord(LeaderboardData leaderboard, string leaderboardName, User user){
//         int leaderboardIndex = leaderboard.userDetailsList.Count;
//         string newRecord = JsonUtility.ToJson(user);
//         databaseReference.Child(leaderboardName).Child("userDetailsList").Child(leaderboardIndex.ToString()).SetRawJsonValueAsync(newRecord);
//         RefreshLeaderboards();
//     }

// }