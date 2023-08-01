using TMPro;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class StoneHitpointsText : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;

    private Destructible destructible;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();

        destructible.ChangeHitPoints.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        destructible.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        int hp = destructible.GetHitPoints();

        if (hp >= 1000)
        {
            hpText.text = (hp / 1000f).ToString("F1") + "k";
        }
        else
            hpText.text = hp.ToString();
    }
}
