using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UICartUpgrades : MonoBehaviour
{
    [SerializeField] private CartUpgrades cartUpgrades;
    [SerializeField] private Turret turret;

    [Header("UpgradePanel")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text upgradeInfoText;
    [SerializeField] private Text upgradeCostText;

    [Header("DPSInfoPanel")]
    [SerializeField] private Text damageInfoText;
    [SerializeField] private Text fireRateInfoText;
    [SerializeField] private Text bulletsAmountInfoText;
    [SerializeField] private Text DPSInfoText;

    private CartUpgrades.UpgradeType type;

    private void Start()
    {
        upgradeButton.interactable = false;

        upgradeInfoText.text = "Choose upgrade";
        upgradeCostText.text = "";

        UpdateDPSInfo();
    }

    public void OnDamageUpgradeSelected()
    {
        type = CartUpgrades.UpgradeType.Damage;

        upgradeInfoText.text = "Damage + " + cartUpgrades.DamageUpgradeAmount;

        if (cartUpgrades.DamageUpgradeCost >= 1000)
            upgradeCostText.text = "Cost: " + (cartUpgrades.DamageUpgradeCost / 1000f).ToString("F1") + "k";
        else 
            upgradeCostText.text = "Cost: " + cartUpgrades.DamageUpgradeCost;

        SwitchUpgradeButton(type);
    }

    public void OnFireRateUpgradeSelected()
    {
        type = CartUpgrades.UpgradeType.FireRate;

        upgradeInfoText.text = "Fire Rate + " + cartUpgrades.FireRateUpgradeAmount;

        if (cartUpgrades.FireRateUpgradeCost >= 1000)
            upgradeCostText.text = "Cost: " + (cartUpgrades.FireRateUpgradeCost / 1000f).ToString("F1") + "k";
        else 
            upgradeCostText.text = "Cost: " + cartUpgrades.FireRateUpgradeCost;

        SwitchUpgradeButton(type);
    }

    public void OnBulletsAmountUpgradeSelected()
    {
        type = CartUpgrades.UpgradeType.BulletsAmount;

        upgradeInfoText.text = "Bullets Amount + " + cartUpgrades.BulletsAmountUpgradeAmount;

        if (cartUpgrades.BulletsAmountUpgradeCost >= 1000)
            upgradeCostText.text = "Cost: " + (cartUpgrades.BulletsAmountUpgradeCost / 1000f).ToString("F1") + "k";
        else
            upgradeCostText.text = "Cost: " + cartUpgrades.BulletsAmountUpgradeCost;

        SwitchUpgradeButton(type);
    }

    private void SwitchUpgradeButton(CartUpgrades.UpgradeType type)
    {
        if (cartUpgrades.IsUpgradeAvailable(type) == true)
        {
            upgradeButton.interactable = true;
        }
        else upgradeButton.interactable = false;
    }

    public void OnClickUpgrade()
    {
        if (type == CartUpgrades.UpgradeType.Damage)
        {
            cartUpgrades.DamageUpgrade();
            OnDamageUpgradeSelected();
        }
        if (type == CartUpgrades.UpgradeType.FireRate)
        {
            cartUpgrades.FireRateUpgrade();
            OnFireRateUpgradeSelected();
        }
        if (type == CartUpgrades.UpgradeType.BulletsAmount)
        {
            cartUpgrades.BulletsAmountUpgrade();
            OnBulletsAmountUpgradeSelected();
        }
    }

    public void UpdateDPSInfo()
    {
        damageInfoText.text = "Damage: " + turret.Damage.ToString();
        fireRateInfoText.text = "Fire Rate: " + turret.FireRate.ToString();
        bulletsAmountInfoText.text = "Bullets: " + turret.ProjectileAmount.ToString();

        if (turret.DPS >= 1000)
            DPSInfoText.text = "DPS: " + (turret.DPS / 1000f).ToString("F1") + "k";
        else 
            DPSInfoText.text = "DPS: " + turret.DPS.ToString();
    }
}
