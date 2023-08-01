using UnityEngine;
using UnityEngine.UI;

public class UILevelProgress : MonoBehaviour
{
    [SerializeField] private LevelProgress levelProgress;

    [SerializeField] private Image progressBar;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text nextLevelText;
    [SerializeField] private Text progressText;

    private float smallStonesAmountPercent;

    private void OnDestroy()
    {
        StoneSpawner.Instance.SmallStoneDestroyed.RemoveListener(OnSmallStoneDestroyed);
    }

    private void Start()
    {
        StoneSpawner.Instance.SmallStoneDestroyed.AddListener(OnSmallStoneDestroyed);

        currentLevelText.text = levelProgress.CurrentLevel.ToString();
        nextLevelText.text = (levelProgress.CurrentLevel + 1).ToString();
        progressBar.fillAmount = 0;

        smallStonesAmountPercent = 1f / StoneSpawner.Instance.AllSmallStonesAmount();
    }

    private void OnSmallStoneDestroyed()
    {
        progressBar.fillAmount += smallStonesAmountPercent;

        progressText.text = progressBar.fillAmount.ToString("P0");
    }
}
