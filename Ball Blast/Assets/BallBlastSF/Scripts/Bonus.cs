using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bonus : PickupObject
{
    public enum Type
    {
        Invincible,
        StoneStop
    }

    [Header("Неуязвимость")]
    [SerializeField] private float invincibleDuration;

    [Header("Остановка камней")]
    [SerializeField] private float stoneStopDuration;

    private StoneStopBonusDisableTimer stoneStopDisableScript;
    private UIBonusText bonusTextScript;
    private SpriteRenderer sprite;

    private Type type;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        bonusTextScript = FindObjectOfType<UIBonusText>();
        stoneStopDisableScript = FindObjectOfType<StoneStopBonusDisableTimer>();

        stoneStopDisableScript.stoneStopDuration = stoneStopDuration;

        RandomizeBonus();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.transform.root.TryGetComponent<Cart>(out Cart cart))
        {
            bonusTextScript.ShowBonusText(type);
            bonusTextScript.timer = 0;
            bonusTextScript.isTextShowing = true;

            switch (type)
            {
                case Type.Invincible:
                    {
                        cart.InvincibleOn(invincibleDuration);

                        break;
                    }
                case Type.StoneStop:
                    {
                        StoneSpawner.Instance.enabled = false;
                        StoneMovement.isStoneStop = true;
                        stoneStopDisableScript.timer = 0;

                        break;
                    }
                default: return;
            }
        }
    }

    private void RandomizeBonus()
    {
        type = (Type)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Type)).Length);

        switch (type)
        {
            case Type.Invincible:
                {
                    sprite.color = new Color(0, 255, 0);
                    break;
                }     
            case Type.StoneStop:
                {
                    sprite.color = new Color(255, 255, 255);
                    break;
                }
            default:
                {
                    sprite.color = new Color(255, 255, 255);
                    break;
                }
        }
    }
}
