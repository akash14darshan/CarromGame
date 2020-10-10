using UnityEngine;

class Token : MonoBehaviour
{
    public static int CollisionCount;

    [SerializeField] Transform Arrow;
    Vector3 OriginalPosition;
    [HideInInspector] public Rigidbody2D RigidBody;
    [SerializeField] GameObject Focus;
    public byte Point;
    int lastFrame;
    
    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        OriginalPosition = transform.localPosition;
        Arrow.gameObject.SetActive(false);
        Focus.SetActive(false);
    }

    bool _isColliding;
    bool IsColliding
    {
        set
        {
            if(value != _isColliding)
            {
                _isColliding = value;
                if (value) CollisionCount++;
                else CollisionCount--;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        IsColliding = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IsColliding = false;
    }

    void OnDestroy()
    {
        IsColliding = false;
    }

    void OnDisable()
    {
        IsColliding = false;
    }

    void OnEnable()
    {
        IsColliding = false;
    }

    void Update()
    {
        if(Time.frameCount - lastFrame > 5)
        {
            Arrow.gameObject.SetActive(false);
            Focus.SetActive(false);
        }
    }

    public void Reset()
    {
        if(OriginalPosition != Vector3.zero)
            transform.localPosition = OriginalPosition;
        IsColliding = false;
        gameObject.SetActive(true);
    }

    public void Activate(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetDirection(Vector2 normal)
    {
        transform.localEulerAngles = Vector3.zero;
        Arrow.gameObject.SetActive(true);
        Focus.SetActive(true);
        lastFrame = Time.frameCount;
        float angle = (Mathf.Rad2Deg * Mathf.Atan(normal.y / normal.x)) - 90;
        if (normal.x > 0)
            angle += 180;
        Arrow.localEulerAngles = new Vector3(0, 0, angle);
    }
}
