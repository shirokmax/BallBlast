using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;

    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            Pause();  
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            Unpause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;

        pausePanel.SetActive(true);
        pauseButton.enabled = false;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        isPaused = false;

        pausePanel.SetActive(false);
        pauseButton.enabled = true;
    }
}
