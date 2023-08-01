using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Stone;

public class StoneSpawner : MonoBehaviour
{
    public static StoneSpawner Instance;

    [Header("Spawn")]
    [SerializeField] private Stone stonePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate;

    [Header("Stats Balance")]
    [SerializeField] private Turret turret;
    [SerializeField] [Range(0.0f, 1.0f)] private float minHitpointsPercentage;
    [SerializeField] private float maxHitpointsRate;

    [Header("Coin Balance")]
    [SerializeField] [Range(0.0f, 1.0f)] private float coinValuePercentage;
    [SerializeField] private float minCoinValueRate;
    [SerializeField] private float maxCoinValueRate;

    [Space(10)]
    [HideInInspector] public UnityEvent Completed;
    [HideInInspector] public UnityEvent SmallStoneDestroyed;

    [HideInInspector] public float offsetPosZ;

    private float timer;

    private int stoneAmount;    
    private int stoneAmountspawned;

    private int stoneMinHitpoints;
    private int stoneMaxHitpoints;

    private List<Stone.StoneSize> stoneSizes;

    public int StoneAmount => stoneAmount;
    public int StoneAmountspawned => stoneAmountspawned;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        stoneAmount = PlayerPrefs.GetInt("LevelProgress:Level", 1);
        stoneSizes = new List<StoneSize>();
        RandomizeStoneSizes();
    }

    private void Start()
    {
        timer = spawnRate;   

        stoneMaxHitpoints = (int)(turret.DPS * maxHitpointsRate);
        stoneMinHitpoints = (int)(stoneMaxHitpoints * minHitpointsPercentage);     
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            Spawn();
            
            timer = 0;
        }

        if (stoneAmountspawned == stoneAmount)
        {
            enabled = false;

            Completed.Invoke();
        }
    }

    private void Spawn()
    {
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        Vector3 position = new Vector3(spawnPoints[spawnPoint].position.x, spawnPoints[spawnPoint].position.y, offsetPosZ);

        Stone stone = Instantiate(stonePrefab, position, Quaternion.identity);
        stone.SetSize(stoneSizes[stoneAmountspawned]);
        stone.MaxHitPoints = Random.Range(stoneMinHitpoints, stoneMaxHitpoints + 1);

        stone.coinValue = GenerateCoinValue();

        stoneAmountspawned++;
        offsetPosZ += 0.01f;
    }

    private void RandomizeStoneSizes()
    {
        for (int i = 0; i < stoneAmount; i++)
        {
            stoneSizes.Add((Stone.StoneSize) Random.Range(1, 4));
        }
    }

    public int AllSmallStonesAmount()
    {
        int amount = 0;

        for (int i = 0; i < stoneSizes.Count; i++)
        {
            if (stoneSizes[i] == Stone.StoneSize.Huge) amount += 8;
            if (stoneSizes[i] == Stone.StoneSize.Big) amount += 4;
            if (stoneSizes[i] == Stone.StoneSize.Medium) amount += 2;
            if (stoneSizes[i] == Stone.StoneSize.Small) amount += 1;
        }

        return amount;
    }

    public int GenerateCoinValue()
    {
        return (int)(turret.DPS * coinValuePercentage * Random.Range(minCoinValueRate, maxCoinValueRate));
    }
}
