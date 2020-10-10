using System.Collections;
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
    SpriteRenderer Renderer;
    
    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
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
        if(!value)
        {
            Token.CollisionCount = 0;
        }
    }

    public void SetStrikerPosition(float normalizedPos)
    {
        transform.localPosition = new Vector2(Mathf.Clamp(normalizedPos * MaxDisplacement, -MaxDisplacement, MaxDisplacement),Height);
        transform.localEulerAngles = Vector3.zero;
        SetRenderer(true);
    }

    public bool CheckPosition()
    {
        return Token.CollisionCount == 0;
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
                StartCoroutine(CheckingBoardMovement());
            });
        }
    }

    public void SetRenderer(bool value)
    {
        if(Renderer != null)
            Renderer.enabled = value;
        if(Collider != null)
            Collider.enabled = value;
    }

    IEnumerator CheckingBoardMovement()
    {
        yield return new WaitForSeconds(0.1f);
        if (IsMoving)
        {
            while (!StrikerBody.IsSleeping() || StrikerBody.velocity.magnitude != 0)
                yield return null;
            while(!MainBoard.HasStopped)
            {
                yield return null;
            }
            if(MainBoard.IsGameOver)
            {
                Popup.ShowConfirm("Game over", "You have finished the game. Do you want to replay?", () => 
                {
                    MainBoard.Instance.ResetGame();
                    SetStrikerPosition(0);
                    IsMoving = false;
                    Mover.SetActive(true);
                },
                    () => MatchUI.Instance.Leave(false));
            }
            else
            {
                SetStrikerPosition(0);
                IsMoving = false;
                Mover.SetActive(true);
            }
        }
    }
}
