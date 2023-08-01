using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] viewSprites;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float vehicleWidth;

    [Header("Wheels")]
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float wheelRadius;

    [HideInInspector] public UnityEvent CollisionStone;

    private Vector3 movementTarget;

    private float lastPositionX;
    private float deltaMovement;

    private float timer;
    private bool isInvincible;
    private float invincibleDuration;

    private void Start()
    {
        movementTarget = transform.position;
    }

    private void Update()
    {
        CheckInvincible();
        Move();
        RotateWheel();
    }

    public void InvincibleOn(float duration)
    {
        isInvincible = true;
        invincibleDuration = duration;
        timer = 0;

        for (int i = 0; i < viewSprites.Length; i++)
            viewSprites[i].color = Color.black;
    }

    private void CheckInvincible()
    {
        if (isInvincible == true)
        {
            timer += Time.deltaTime;

            if (timer >= invincibleDuration)
            {
                isInvincible = false;

                for (int i = 0; i < viewSprites.Length; i++)
                    viewSprites[i].color = Color.white;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent<Stone>(out Stone stone) && isInvincible == false)
        {
            CollisionStone.Invoke();
        }
    }

    public void Move()
    {
        lastPositionX = transform.position.x;

        transform.position = Vector3.MoveTowards(transform.position, movementTarget, movementSpeed * Time.deltaTime);

        deltaMovement = transform.position.x - lastPositionX;
    }

    private void RotateWheel()
    {
        float angle = (180 * deltaMovement) / (Mathf.PI * wheelRadius * 2);

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Rotate(0, 0, -angle);
        }
    }

    public void SetMovementTarget(Vector3 target)
    {
        float leftBorder = LevelBoundary.Instance.LeftBorder + vehicleWidth * 0.5f;
        float rightBorder = LevelBoundary.Instance.RightBorder - vehicleWidth * 0.5f;

        Vector3 movTarget = target;
        movTarget.z = transform.position.z;
        movTarget.y = transform.position.y;

        if (movTarget.x < leftBorder) movTarget.x = leftBorder;
        if (movTarget.x > rightBorder) movTarget.x = rightBorder;

        movementTarget = movTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(new Vector3(transform.position.x - vehicleWidth * 0.5f, transform.position.y - 0.5f),
                        new Vector3(transform.position.x + vehicleWidth * 0.5f, transform.position.y - 0.5f));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(wheels[0].position, wheels[0].position + new Vector3(wheelRadius, 0, 0));
    }
#endif
}
