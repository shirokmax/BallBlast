using System;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float startSpeed;
    [SerializeField] private float reboundSpeed;
    [SerializeField] private float minReboundSpeed;
    [SerializeField][Range(0.0f, 1.0f)] private float reboundSpeedReductionRate;

    private Vector3 velocity;

    protected virtual void Start()
    {
        velocity.y = startSpeed;
        velocity.x = RandomSign() * horizontalSpeed;
    }

    protected virtual void Update()
    {
        velocity.y -= gravity * Time.deltaTime;

        if (Math.Abs(velocity.x) > 0.1f)
        {
            if (velocity.x < 0) velocity.x += Time.deltaTime;
            else velocity.x -= Time.deltaTime;
        }
        else velocity.x = 0;

        transform.position += velocity * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Cart>() != null)
        {
            Destroy(gameObject);
        }

        if (collision.TryGetComponent<LevelEdge>(out LevelEdge levelEdge))
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                if (reboundSpeed >= minReboundSpeed)
                {
                    reboundSpeed *= reboundSpeedReductionRate;
                    velocity.y = reboundSpeed;
                }
                else
                {
                    enabled = false;
                }
            }

            if (levelEdge.Type == EdgeType.Left && velocity.x < 0 || levelEdge.Type == EdgeType.Right && velocity.x > 0)
            {
                velocity.x *= -1;
            }
        }
    }

    private int RandomSign()
    {
        int sign = UnityEngine.Random.Range(0, 2);

        if (sign == 0) return -1;

        return 1;
    }
}
