using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{
    public enum StoneSize
    {
        Small,
        Medium,
        Big,
        Huge
    }

    [Header("Parameters")]
    [SerializeField] private Color[] colors;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private StoneSize size;
    [SerializeField] private float spawnUpForce;

    [Header("Coin")]
    [SerializeField] private Coin coinPrefab;
    [SerializeField][Range(0.0f, 1.0f)] private float coinDropChance;

    [Header("Bonus")]
    [SerializeField] private Bonus bonusPrefab;
    [SerializeField][Range(0.0f, 1.0f)] private float bonusDropChance;

    [HideInInspector] public int coinValue;

    private StoneMovement movement;

    public StoneSize Size => size;

    private void Awake()
    {
        movement = GetComponent<StoneMovement>();

        SetSize(size);
        sprite.color = colors[Random.Range(0, colors.Length)];

        OnDestroyed.AddListener(OnStoneDestroyed);
    }

    private void OnDestroy()
    {
        OnDestroyed.RemoveListener(OnStoneDestroyed);
    }

    public void SetSize(StoneSize size)
    {
        if (size < 0) return;

        switch (size)
        {
            case StoneSize.Small:
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case StoneSize.Medium:
                transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                break;
            case StoneSize.Big:
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                break;
            case StoneSize.Huge:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                transform.localScale = Vector3.one;
                break;
        }

        this.size = size;
    }

    private void OnStoneDestroyed()
    {
        if (size != StoneSize.Small) SpawnStones();

        if (Random.Range(0.0f, 1.0f) <= coinDropChance)
        {
            Coin coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.Value = coinValue;
        }

        if (Random.Range(0.0f, 1.0f) <= bonusDropChance)
        {
            Instantiate(bonusPrefab, transform.position, Quaternion.identity);
        }

        if (size == StoneSize.Small)
        {
            StoneSpawner.Instance.SmallStoneDestroyed.Invoke();
        }

        Destroy(gameObject);
    }

    private void SpawnStones()
    {
        for (int i = 0; i < 2; i++)
        {          
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, StoneSpawner.Instance.offsetPosZ);

            Stone stone = Instantiate(this, spawnPosition, Quaternion.identity);
            stone.SetSize(size - 1);
            stone.MaxHitPoints = Mathf.Clamp(MaxHitPoints / 2, 1, MaxHitPoints);
            stone.movement.AddVerticalVelocity(spawnUpForce);
            stone.movement.SetHorizontalDirection(i * 2 - 1);

            stone.coinValue = StoneSpawner.Instance.GenerateCoinValue();

            StoneSpawner.Instance.offsetPosZ += 0.01f;
        }
    }
}
