using UnityEngine;

[RequireComponent(typeof(Turret), typeof(Bag))]
public class CartUpgrades : MonoBehaviour
{
    public enum UpgradeType
    {
        Damage,
        FireRate,
        BulletsAmount
    }

    private Turret turret;
    private Bag bag;

    [Header("Стартовая стоймость апгрейдов")]
    [SerializeField] private int startDamageUpgradeCost;
    [SerializeField] private int startFireRateUpgradeCost;
    [SerializeField] private int startBulletsAmountUpgradeCost;

    [Header("Множитель увеличения стоймости апргейдов")]
    [SerializeField] private float damageUpgradeCostRate;
    [SerializeField] private float fireRateUpgradeCostRate;
    [SerializeField] private float bulletsAmountUpgradeCostRate;

    [Header("Сила апгрейда")]
    [SerializeField] private int damageUpgradeAmount;
    [SerializeField] private int fireRateUpgradeAmount;
    [SerializeField] private int bulletsAmountUpgradeAmount;

    private int damageUpgradeCost;
    private int fireRateUpgradeCost;
    private int bulletsAmountUpgradeCost;

    public int DamageUpgradeCost => damageUpgradeCost;
    public int FireRateUpgradeCost => fireRateUpgradeCost;
    public int BulletsAmountUpgradeCost => bulletsAmountUpgradeCost;

    public int DamageUpgradeAmount => damageUpgradeAmount;
    public int FireRateUpgradeAmount => fireRateUpgradeAmount;
    public int BulletsAmountUpgradeAmount => bulletsAmountUpgradeAmount;

    private void Awake()
    {
        turret = GetComponent<Turret>();
        bag = GetComponent<Bag>();

        Load();
    }

    public bool IsUpgradeAvailable(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Damage:
                {
                    if (bag.GetCoinsAmount() - damageUpgradeCost >= 0) return true;
                    return false;
                }               
            case UpgradeType.FireRate:
                {
                    if (bag.GetCoinsAmount() - fireRateUpgradeCost >= 0) return true;
                    return false;
                }            
            case UpgradeType.BulletsAmount:
                {
                    if (bag.GetCoinsAmount() - bulletsAmountUpgradeCost >= 0) return true;
                    return false;
                }
            default: return false;
        }
    }

    public void DamageUpgrade()
    {
        if (IsUpgradeAvailable(UpgradeType.Damage) == false) return;

        bag.RemoveCoins(damageUpgradeCost);

        turret.Damage += damageUpgradeAmount;

        int newUpgradeCost = (int)(damageUpgradeCost * damageUpgradeCostRate);

        if (newUpgradeCost == damageUpgradeCost) damageUpgradeCost += 1;
        else damageUpgradeCost = newUpgradeCost;

        PlayerPrefs.SetInt("CartUpgrades:DamageUpgradeCost", damageUpgradeCost);
        PlayerPrefs.SetInt("Turret:Damage", turret.Damage);
    }

    public void FireRateUpgrade()
    {
        if (IsUpgradeAvailable(UpgradeType.FireRate) == false) return;

        bag.RemoveCoins(fireRateUpgradeCost);

        turret.FireRate += fireRateUpgradeAmount;

        int newUpgradeCost = (int)(fireRateUpgradeCost * fireRateUpgradeCostRate);

        if (newUpgradeCost == fireRateUpgradeCost) fireRateUpgradeCost += 1;
        else fireRateUpgradeCost = newUpgradeCost;

        PlayerPrefs.SetInt("CartUpgrades:FireRateUpgradeCost", fireRateUpgradeCost);
        PlayerPrefs.SetInt("Turret:FireRate", turret.FireRate);
    }

    public void BulletsAmountUpgrade()
    {
        if (IsUpgradeAvailable(UpgradeType.BulletsAmount) == false) return;

        bag.RemoveCoins(bulletsAmountUpgradeCost);

        turret.ProjectileAmount += bulletsAmountUpgradeAmount;

        int newUpgradeCost = (int)(bulletsAmountUpgradeCost * bulletsAmountUpgradeCostRate);

        if (newUpgradeCost == bulletsAmountUpgradeCost) bulletsAmountUpgradeCost += 1;
        else bulletsAmountUpgradeCost = newUpgradeCost;

        PlayerPrefs.SetInt("CartUpgrades:BulletsAmountUpgradeCost", bulletsAmountUpgradeCost);
        PlayerPrefs.SetInt("Turret:ProjectileAmount", turret.ProjectileAmount);
    }

    private void Load()
    {
        damageUpgradeCost = PlayerPrefs.GetInt("CartUpgrades:DamageUpgradeCost", startDamageUpgradeCost);
        fireRateUpgradeCost = PlayerPrefs.GetInt("CartUpgrades:FireRateUpgradeCost", startFireRateUpgradeCost);
        bulletsAmountUpgradeCost = PlayerPrefs.GetInt("CartUpgrades:BulletsAmountUpgradeCost", startBulletsAmountUpgradeCost);
    }
}
