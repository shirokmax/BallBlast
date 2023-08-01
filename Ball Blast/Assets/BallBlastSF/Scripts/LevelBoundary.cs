using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static LevelBoundary Instance;

    [SerializeField] private Vector2 screenResolution;
    [SerializeField] private BoxCollider2D leftEdge;
    [SerializeField] private BoxCollider2D rightEdge;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (Application.isPlaying == true && Application.isEditor == false)
        {
            screenResolution.x = Screen.width;
            screenResolution.y = Screen.height;
        }

        leftEdge.transform.position = new Vector3(LeftBorder - leftEdge.size.x * 0.5f, leftEdge.transform.position.y);
        rightEdge.transform.position = new Vector3(RightBorder + rightEdge.size.x * 0.5f, rightEdge.transform.position.y);
    }

    public float LeftBorder
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x;
        }
    }

    public float RightBorder
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(screenResolution.x, 0)).x;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(new Vector3(LeftBorder, -10), new Vector3(LeftBorder, 10));
        Gizmos.DrawLine(new Vector3(RightBorder, -10), new Vector3(RightBorder, 10));
    }
#endif
}
