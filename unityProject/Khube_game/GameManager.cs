using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    public GameObject completeLevelUI;
    public GameObject completeGameUI;
    public void completeLevel()
    {
        completeLevelUI.SetActive(true);
        Invoke("loadNextLevel" , 1f);

    }
    public void gameOver()
    {
        if(gameEnded == false)
        {
            gameEnded = true;
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            Invoke("restart" , 1f);
        }
    }
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void loadNextLevel()
    {
        if((SceneManager.GetActiveScene().buildIndex + 1) != 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }else{
            completeLevelUI.SetActive(false);
            completeGameUI.SetActive(true);
            gameEnded = true;
        }
    }
}
