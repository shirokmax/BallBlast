using System;
using UnityEngine;
using UnityEngine.UI;

public class Coin : PickupObject
{
    [SerializeField] private int value;
    [SerializeField] private Text valueText;

    public int Value
    { 
        get { return value; }
        set
        {
            if (value >= 1) this.value = value;
            else this.value = 1;
        }
    }

    protected override void Start()
    {
        base.Start(); 
        
        valueText.text = value.ToString();

        if (value >= 1000)
            valueText.text = (value / 1000f).ToString("F1") + "k";
        else
            valueText.text = value.ToString();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.transform.root.TryGetComponent<Bag>(out Bag bag))
        {
            bag.AddCoins(value);
        }
    }
}
