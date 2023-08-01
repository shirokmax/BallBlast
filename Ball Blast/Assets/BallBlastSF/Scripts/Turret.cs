using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Параметры снарядов")]   
    [SerializeField][Min(0)] private int startDamage;
    [SerializeField][Min(0)] private int startFireRate;
    [SerializeField][Min(1)] private int startProjectileAmount;
    [SerializeField][Min(0)] private float projectileInterval;

    private int damage;
    private int fireRate;
    private int projectileAmount;

    public int Damage
    {
        get { return damage; }
        set { if (value > 0) { damage = value; } }
    }
    public int FireRate
    {
        get { return fireRate; }
        set { if (value > 0) { fireRate = value; } }
    }
    public int ProjectileAmount
    {
        get { return projectileAmount; }
        set { if (value >= 1) projectileAmount = value; }
    }
    public int DPS
    {
        get { return (damage * projectileAmount * fireRate); }
    }

    private float timer;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        timer = 1f / fireRate;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void SpawnProjectile()
    {
        float startPosX = shootPoint.position.x - (projectileAmount - 1) * projectileInterval * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, new Vector3(startPosX + projectileInterval * i, shootPoint.position.y), transform.rotation);
            projectile.SetDamage(damage);
        }
    }

    public void Fire()
    {
        if (timer >= 1f / fireRate)
        {
            SpawnProjectile();

            timer = 0;
        }
    }

    private void Load()
    {
        damage = PlayerPrefs.GetInt("Turret:Damage", startDamage);
        fireRate = PlayerPrefs.GetInt("Turret:FireRate", startFireRate);
        projectileAmount = PlayerPrefs.GetInt("Turret:ProjectileAmount", startProjectileAmount);
    }
}
