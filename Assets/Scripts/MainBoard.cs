using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MainBoard : MonoBehaviour
{
    public static readonly List<Token> Bodies = new List<Token>();
    [SerializeField] Text Score;
    [SerializeField] BoardStriker Striker;
    short Score_value;
    public static MainBoard Instance;

    void Awake()
    {
        Instance = this;
        byte count = 0;
        foreach(var body in GetComponentsInChildren<Token>(true))
        {
            body.ID = count++;
            Bodies.Add(body);
        }
    }

    void OnEnable()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        foreach (var body in Bodies)
        {
            body.Reset();
            Score_value = 0;
            Score.text = "0\nPoints";
        }
        Striker.ResetMover();
    }

    public void AddScore(short value)
    {
        Score_value += value;
        Score.text = Score_value.ToString()+ "\nPoints";
    }

    public static bool IsGameOver
    {
        get
        {
            foreach(var body in Bodies)
            {
                if (body.gameObject.activeInHierarchy) return false;
            }
            return true;
        }
    }

    public static bool HasStopped
    {
        get
        {
            foreach(var body in Bodies)
            {
                if(body.gameObject.activeInHierarchy && (!body.RigidBody.IsSleeping() || body.RigidBody.velocity.magnitude != 0))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
