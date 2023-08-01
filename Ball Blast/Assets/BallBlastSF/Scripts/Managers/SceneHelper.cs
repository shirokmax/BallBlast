using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        StoneMovement.isStoneStop = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}