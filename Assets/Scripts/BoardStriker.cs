using UnityEngine;

class BoardStriker : MonoBehaviour
{
    [SerializeField] BoxCollider2D Lower;
    [SerializeField] StrikerDrag DragController;
    [SerializeField] float Sensivitity;
    [SerializeField] GameObject Mover;
    bool IsMoving = false;
    CircleCollider2D Collider;
    Rigidbody2D StrikerBody;
    float MaxDisplacement;
    float Height;

    void Awake()
    {
        Vector2 rect = Lower.size;
        MaxDisplacement = Mathf.Max(rect.x, rect.y)/2;
        Height = transform.localPosition.y;
        Collider = GetComponent<CircleCollider2D>();
        StrikerBody = GetComponent<Rigidbody2D>();
        Collider.isTrigger = false;
        Mover.SetActive(true);
    }

    public void ActivateDrag(bool value)
    {
        Collider.isTrigger = value;
    }

    public void SetStrikerPosition(float normalizedPos)
    {
        transform.localPosition = new Vector2(Mathf.Clamp(normalizedPos * MaxDisplacement, -MaxDisplacement, MaxDisplacement),Height);
        transform.localEulerAngles = Vector3.zero;
    }

    public bool CheckPosition()
    {
        return Token.CollisionCount == 0;
    }

    void Update()
    {
        if(IsMoving && MainBoard.HasStopped)
        {
            SetStrikerPosition(0);
            IsMoving = false;
            Mover.SetActive(true);
        }
    }

    void OnMouseDown()
    {
        if(!IsMoving && !Popup.IsActive)
        {
            DragController.Begin(delegate(Vector2 ev) 
            {
                float finalMagnitude = ev.magnitude * Sensivitity;
                StrikerBody.AddForce(new Vector2(ev.x * finalMagnitude, ev.y * finalMagnitude));
                IsMoving = true;
                Mover.SetActive(false);
            });
        }
    }
}
