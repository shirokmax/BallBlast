using UnityEngine;
using UnityEngine.UI;

public class UICoinsText : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Bag bag;

    private void Awake()
    {     
        bag.ChangeCoinsAmount.AddListener(OnChangeCoinsAmount);
    }

    private void OnDestroy()
    {
        bag.ChangeCoinsAmount.RemoveListener(OnChangeCoinsAmount);
    }

    private void OnChangeCoinsAmount()
    {
        if (bag.GetCoinsAmount() >= 1000)
            coinsText.text = (bag.GetCoinsAmount() / 1000f).ToString("F1") + "k";
        else
            coinsText.text = bag.GetCoinsAmount().ToString();
    }
}
