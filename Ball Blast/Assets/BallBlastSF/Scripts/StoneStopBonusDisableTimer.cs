using UnityEngine;

public class StoneStopBonusDisableTimer : MonoBehaviour
{
    [HideInInspector] public float stoneStopDuration;
    [HideInInspector] public float timer;

    private void Update()
    {
        if (StoneMovement.isStoneStop == true)
        {
            timer += Time.deltaTime;

            if (timer >= stoneStopDuration)
            {
                StoneSpawner.Instance.enabled = true;
                StoneMovement.isStoneStop = false;
                timer = 0;
            }
        }
    }
}
