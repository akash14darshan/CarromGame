using System.Collections;
using UnityEngine;

class Pocket : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Token token = other.gameObject.GetComponent<Token>();
        if(token!= null)
        {
            MainBoard.Instance.AddScore(token.Point);
            Rigidbody2D body = other.gameObject.GetComponent<Rigidbody2D>();
            body.velocity = Vector3.zero;
            body.angularVelocity = 0;
            other.gameObject.SetActive(false);
            return;
        }
        BoardStriker striker = other.gameObject.GetComponent<BoardStriker>();
        if(striker != null)
        {
            MainBoard.Instance.AddScore(-1);
            striker.SetRenderer(false);
            StartCoroutine(BeginStop(striker.GetComponent<Rigidbody2D>()));
        }
    }

    IEnumerator BeginStop(Rigidbody2D body)
    {
        yield return new WaitForSeconds(0.3f);
        body.velocity = Vector3.zero;
        body.angularVelocity = 0;
    }
}