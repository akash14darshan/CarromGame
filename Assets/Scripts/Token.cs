using UnityEngine;

class Token : MonoBehaviour
{
    public static int CollisionCount;

    [SerializeField] Transform Arrow;
    int lastFrame;
    
    void Awake()
    {
        Arrow.gameObject.SetActive(false);
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

    void Update()
    {
        if(Time.frameCount - lastFrame > 5)
        {
            Arrow.gameObject.SetActive(false);
        }
    }

    public void SetDirection(Vector2 normal)
    {
        transform.localEulerAngles = Vector3.zero;
        Arrow.gameObject.SetActive(true);
        lastFrame = Time.frameCount;
        Debug.Log(normal);
        float angle = (Mathf.Rad2Deg * Mathf.Atan(normal.y / normal.x)) - 90;
        if (normal.x > 0)
            angle += 180;
        Arrow.localEulerAngles = new Vector3(0, 0, angle);
    }
}
