using UnityEngine;

class Mover : MonoBehaviour
{
    private float Height;
    private float MaxDisp;
    [SerializeField] Transform Striker;
    [SerializeField] BoardStriker BoardStriker;

    private float LastPosition;
    bool IsMouseDown;
    Camera Camera;

    void SetStriker(float disp)
    {
        float pos = Mathf.Clamp(disp, -MaxDisp, MaxDisp);
        Striker.localPosition = new Vector2(pos, Height);
        BoardStriker.SetStrikerPosition(pos / MaxDisp);
    }

    void Awake()
    {
        Camera = Camera.main;
        MaxDisp = GetComponent<BoxCollider2D>().size.x/2;
        Height = Striker.localPosition.y;
        SetStriker(0);
    }

    void Update()
    {
        if(IsMouseDown)
        {
            Vector3 Pos = transform.InverseTransformPoint(Camera.ScreenToWorldPoint(Input.mousePosition));
            SetStriker(Pos.x);
        }
    }

    void OnMouseDown()
    {
        if(!Popup.IsActive)
        {
            Token.CollisionCount = 0;
            LastPosition = Striker.localPosition.x;
            BoardStriker.ActivateDrag(true);
            IsMouseDown = true;
        }
    }

    void OnMouseUp()
    {
        if(IsMouseDown)
        {
            IsMouseDown = false;
            BoardStriker.ActivateDrag(false);
            if (!BoardStriker.CheckPosition())
            {
                Popup.ShowMessage("Error", "Current set position is overlapping with a token");
                SetStriker(LastPosition);
            }
            else
            {
                LastPosition = Striker.localPosition.x;
            }
        }
    }
}
