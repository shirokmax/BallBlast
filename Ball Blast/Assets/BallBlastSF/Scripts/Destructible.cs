using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [SerializeField] [Min(1)] private int maxHitPoints;

    [HideInInspector] public UnityEvent ChangeHitPoints;
    [HideInInspector] public UnityEvent TakeDamage;

    private int hitPoints;

    public UnityEvent OnDestroyed;
    private bool isDestroyed;

    public int MaxHitPoints
    {
        set
        {
            if (value > 0) maxHitPoints = value;
            else maxHitPoints = 1;
        }
        get
        {
            return maxHitPoints;
        }
    }

    private void Start()
    {
        hitPoints = maxHitPoints;
        ChangeHitPoints.Invoke();
    }

    public void ApplyDamage(int damage)
    {
        hitPoints -= damage;
        ChangeHitPoints.Invoke();
        TakeDamage.Invoke();

        if (hitPoints <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (isDestroyed == true) return;

        isDestroyed = true;

        hitPoints = 0;
        ChangeHitPoints.Invoke();
        TakeDamage.Invoke();

        OnDestroyed.Invoke();
    }

    public void Heal(int healAmount)
    {
        if (hitPoints < maxHitPoints)
        {
            if (hitPoints + healAmount > maxHitPoints)
            {
                hitPoints = maxHitPoints;
            }
            else
            {
                hitPoints += healAmount;
            }

            ChangeHitPoints.Invoke();
        }
    }

    public int GetHitPoints()
    {
        return hitPoints;
    }
}