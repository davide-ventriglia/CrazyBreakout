using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardUIController : MonoBehaviour
{
    public GameObject classicLeaderboard;
    public GameObject crazyLeaderboard;

    void Awake() {
        DBManager.Instance.RefreshLeaderboards();
        RenderLeaderboard(classicLeaderboard, DBManager.Instance.ClassicLeaderboardData);
        RenderLeaderboard(crazyLeaderboard, DBManager.Instance.CrazyLeaderboardData);
    }

    void RenderLeaderboard(GameObject leaderboard, LeaderboardData data){
        FillContainer(leaderboard,"NamesContainer","name",data);
        FillContainer(leaderboard,"ScoresContainer","score",data);
    }
    
    void FillContainer(GameObject leaderboard, string containerName, string dataLabel, LeaderboardData data){
        Transform container = leaderboard.transform.Find("Container").Find(containerName);
        for(int i=0; i<data.userDetailsList.Count && i<container.childCount; i++){
            if(data.userDetailsList[i]!=null)
                container.GetChild(i).GetComponent<TMP_Text>().text = dataLabel=="name" ? data.userDetailsList[i].name.ToUpper() : data.userDetailsList[i].score.ToString();
        }
    }
    public void BackToMain(){
        SceneManager.LoadScene("Title",LoadSceneMode.Single);
    }
}


