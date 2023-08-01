using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [HideInInspector] public UnityEvent ChangeCoinsAmount;

    private int coinsAmount;

    private void Start()
    {
        coinsAmount = PlayerPrefs.GetInt("Bag:CoinsAmount", 0);

        ChangeCoinsAmount.Invoke();
    }

    public void AddCoins(int coins)
    {
        coinsAmount += coins;
      
        ChangeCoinsAmount.Invoke();
        PlayerPrefs.SetInt("Bag:CoinsAmount", coinsAmount);
    }

    public bool RemoveCoins(int coins)
    {
        if (coinsAmount - coins < 0) return false;

        coinsAmount -= coins;

        ChangeCoinsAmount.Invoke();
        PlayerPrefs.SetInt("Bag:CoinsAmount", coinsAmount);

        return true;
    }

    public int GetCoinsAmount() 
    { 
        return coinsAmount;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddCoins(10000);     
        }
    }
#endif
}
