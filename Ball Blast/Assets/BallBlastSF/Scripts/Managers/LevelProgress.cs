using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private LevelState levelState;

    private int currentLevel;

    public int CurrentLevel => currentLevel;

    private void Awake()
    {
        levelState.Passed.AddListener(OnLevelPassed);

        Load();
    }

    private void OnDestroy()
    {
        levelState.Passed.RemoveListener(OnLevelPassed);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Reset();
        }
    }
#endif

    private void OnLevelPassed()
    {
        currentLevel++;

        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("LevelProgress:Level", currentLevel);
    }

    private void Load()
    {
        currentLevel = PlayerPrefs.GetInt("LevelProgress:Level", 1);
    }

#if UNITY_EDITOR
    private void Reset()
    {
        PlayerPrefs.DeleteAll();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endif
}
