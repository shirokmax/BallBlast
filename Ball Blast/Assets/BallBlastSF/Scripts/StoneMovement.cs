using System;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{  
    [SerializeField] private float horizontalSpeed;    
    [SerializeField] private float reboundSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float gravityOffset;

    public static bool isStoneStop;

    private Vector3 velocity;
    private bool useGravity;

    private void Awake()
    {
        velocity.x = -Math.Sign(transform.position.x) * horizontalSpeed;
    }

    private void Update()
    {
        if (isStoneStop == false)
        {
            TryEnableGravity();
            Move();
        }
    }

    private void TryEnableGravity()
    {
        if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.LeftBorder) - gravityOffset)
        {
            useGravity = true;
        }
    }

    private void Move()
    {
        if (useGravity == true)
        {
            velocity.y -= gravity * Time.deltaTime;
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        velocity.x = Math.Sign(velocity.x) * horizontalSpeed;

        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<LevelEdge>(out LevelEdge edge))
        {
            if (edge.Type == EdgeType.Bottom)
            {
                velocity.y = reboundSpeed;
            }

            if (edge.Type == EdgeType.Left && velocity.x < 0 || edge.Type == EdgeType.Right && velocity.x > 0)
            {
                velocity.x *= -1;
            }
        }
    }

    public void AddVerticalVelocity(float velocity)
    {
        this.velocity.y += velocity;
    }

    public void SetHorizontalDirection(float direction)
    {
        velocity.x = Math.Sign(direction) * horizontalSpeed;
    }
}
