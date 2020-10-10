using UnityEngine;

class Token : MonoBehaviour
{
    public static int CollisionCount;
    [HideInInspector] public byte ID;
    [SerializeField] Transform Arrow;
    Vector3 OriginalPosition;
    [HideInInspector] public Rigidbody2D RigidBody;
    [SerializeField] GameObject Focus;
    public byte Point;
    int lastFrame;
    bool HasAwakened = false;
    
    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        OriginalPosition = transform.localPosition;
        Arrow.gameObject.SetActive(false);
        Focus.SetActive(false);
        HasAwakened = true;
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
        if(HasAwakened)
            transform.localPosition = OriginalPosition;
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
