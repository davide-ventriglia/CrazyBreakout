using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    public void PlayClassicMode(){
        SceneManager.LoadScene("ClassicMode",LoadSceneMode.Single);
    }

    public void PlayCrazyMode(){
        SceneManager.LoadScene("CrazyMode",LoadSceneMode.Single);
    }

    public void OpenLeaderboard(){
        SceneManager.LoadScene("Leaderboard",LoadSceneMode.Single);
    }

    public void ReturnToTitleScreen(){
        SceneManager.LoadScene("Title",LoadSceneMode.Single);
    }
}
