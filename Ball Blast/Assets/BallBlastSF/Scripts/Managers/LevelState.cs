using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private Cart cart;
    [SerializeField] private StoneSpawner stoneSpawner;

    [Space(5)]
    public UnityEvent Passed;
    public UnityEvent Defeat;

    private float timer;
    private bool checkPassed;
    private bool checkDefeat;

    private void Awake()
    {
        cart.CollisionStone.AddListener(OnCartCollisionStone);
        stoneSpawner.Completed.AddListener(OnStoneSpawnCompleted);
    }

    private void OnDestroy()
    {
        cart.CollisionStone.RemoveListener(OnCartCollisionStone);
        stoneSpawner.Completed.RemoveListener(OnStoneSpawnCompleted);
    }

    private void OnCartCollisionStone()
    {
        if (checkDefeat == false)
        {
            Defeat.Invoke();

            checkDefeat = true;
        }  
    }

    private void OnStoneSpawnCompleted()
    {
        checkPassed = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            if (checkPassed == true && FindObjectsOfType<Stone>().Length == 0)
            {
                Passed.Invoke();

                checkPassed = false;
            }

            timer = 0;
        }
    }
}
