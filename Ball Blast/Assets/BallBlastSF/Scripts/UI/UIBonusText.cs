using UnityEngine;
using UnityEngine.UI;

public class UIBonusText : MonoBehaviour
{
    [SerializeField] private Text invincibleBonusText;
    [SerializeField] private Text stopStonesBonusText;

    [SerializeField] private float showDuration;

    [HideInInspector] public bool isTextShowing;
    [HideInInspector] public float timer;

    private void Update()
    {
        if (isTextShowing == true)
        {
            timer += Time.deltaTime;

            if (timer >= showDuration)
            {
                DisableAllBonusText();
                isTextShowing = false;
                timer = 0;
            }
        }
    }

    public void ShowBonusText(Bonus.Type type)
    {
        if (type == Bonus.Type.Invincible)
        {
            invincibleBonusText.enabled = true;
            stopStonesBonusText.enabled = false;
        }

        if (type == Bonus.Type.StoneStop)
        {
            stopStonesBonusText.enabled = true;
            invincibleBonusText.enabled = false;
        }
    }

    public void DisableAllBonusText()
    {
        invincibleBonusText.enabled = false;
        stopStonesBonusText.enabled = false;
    }
}
