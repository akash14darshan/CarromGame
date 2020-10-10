using System;
using UnityEngine;

class StrikerDrag : MonoBehaviour
{
    Action<Vector2> Callback;
    [SerializeField] Transform Line;
    [SerializeField] Transform Arrow;
    [SerializeField] float MaxMagnitude;
    [SerializeField] float MinMagnitude;
    Transform Pivot;
    Camera Camera;
    Vector2 Dir;
    int TokenLayer;

    private float MaxHeight;
    private float MinHeight;
    void Awake()
    {
        TokenLayer = LayerMask.GetMask("Token");
        Camera = Camera.main;
        Pivot = transform.parent;
        BoxCollider2D box = Line.GetComponent<BoxCollider2D>();
        MinHeight = Arrow.localPosition.y;
        MaxHeight = MinHeight + box.size.y * Line.localScale.y;
        Destroy(box);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.SetActive(false);
            Callback?.Invoke(Dir);
        }
        else
        {
            Vector3 Direction = -Pivot.InverseTransformPoint(Camera.ScreenToWorldPoint(Input.mousePosition));
            Dir = new Vector2(Direction.x, Direction.y);
            if(Dir.magnitude > MaxMagnitude)
            {
                Dir = (Dir / Dir.magnitude) * MaxMagnitude;
            }
            Arrow.localPosition = new Vector2(0, Mathf.Clamp(MinHeight + Dir.magnitude, MinHeight, MaxHeight));
            float percentageMagnitude = (Dir.magnitude - MinMagnitude) / (MaxMagnitude - MinMagnitude);
            float height = ((MaxHeight - MinHeight) * percentageMagnitude) + MinHeight;
            Arrow.localPosition = new Vector2(0, Mathf.Clamp(height, MinHeight, MaxHeight));
            float angle = (Mathf.Rad2Deg * Mathf.Atan(Direction.y / Direction.x)) - 90;
            if (Direction.x < 0) angle -= 180;
            transform.localEulerAngles = new Vector3(0,0,angle);


            //var hit = Physics2D.Raycast(Vector3.zero,Direction,float.PositiveInfinity,TokenLayer);
            var hit = Physics2D.Raycast(transform.position, transform.up, float.PositiveInfinity, TokenLayer);
            if(hit.collider != null)
            {
                Token token = hit.collider.gameObject.GetComponent<Token>();
                if(token != null)
                {
                    token.SetDirection(hit.normal);
                }
            }
        }
    }

    public void Begin(Action<Vector2> callback)
    {
        Callback = callback;
        gameObject.SetActive(true);
    }
}
