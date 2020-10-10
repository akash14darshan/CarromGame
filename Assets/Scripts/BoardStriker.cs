using System.Collections;
using System.Collections.Generic;
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

    readonly List<byte> CollidingTokenIDs = new List<byte>();

    void OnTriggerEnter2D(Collider2D other)
    {
        Token token = other.gameObject.GetComponent<Token>();
        if(token != null)
        {
            Debug.Log("Enter Token found:" + token.ID);
            CollidingTokenIDs.Add(token.ID);
            return;
        }
        Debug.Log("Entered collision token not found:"+other.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Token token = other.gameObject.GetComponent<Token>();
        if (token != null)
        {
            Debug.Log("Exit Token found:" + token.ID);
            CollidingTokenIDs.Remove(token.ID);
            return;
        }
        Debug.Log("Exit collision token not found:" + other.gameObject.name);
    }

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

    public void SetStrikerPosition(float normalizedPos)
    {
        transform.localPosition = new Vector2(Mathf.Clamp(normalizedPos * MaxDisplacement, -MaxDisplacement, MaxDisplacement),Height);
        transform.localEulerAngles = Vector3.zero;
        SetRenderer(true);
    }

    public bool CheckPosition()
    {
        return CollidingTokenIDs.Count == 0;
    }

    public void ResetMover()
    {
        if(Collider)
            Collider.isTrigger = true;
        SetStrikerPosition(0);
        IsMoving = false;
        Mover.SetActive(true);
    }

    void OnMouseDown()
    {
        if(!IsMoving && !Popup.IsActive)
        {
            if(!CheckPosition())
            {
                Popup.ShowMessage("Notice", "Striker is overlapping a token. Please move striker to an empty place");
                return;
            }
            DragController.Begin(delegate(Vector2 ev) 
            {
                Collider.isTrigger = false;
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
                    ResetMover();
                },
                    () => MatchUI.Instance.Leave(false));
            }
            else
            {
                ResetMover();
            }
        }
    }
}
