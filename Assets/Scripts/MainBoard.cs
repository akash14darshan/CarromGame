using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MainBoard : MonoBehaviour
{
    public static readonly List<Token> Bodies = new List<Token>();
    [SerializeField] BoardStriker Striker;
    [SerializeField] Text _score_text;
    [SerializeField] Text _time_text;
    [SerializeField] Text _moves_text;
    short _score_value;
    short Score
    {
        get => _score_value;
        set
        {
            _score_value = value;
            _score_text.text = "Points\n" + value;
        }
    }

    int _time_value;
    int Time
    {
        get => _time_value;
        set
        {
            _time_value = value;
            TimeSpan t = TimeSpan.FromSeconds(value);
            _time_text.text = "Time Elapsed\n" + string.Format("{0:D2}:{1:D2}:{2:D2}",
                t.Hours,
                t.Minutes,
                t.Seconds);
        }
    }

    short _moves_value;
    short Moves
    {
        get => _moves_value;
        set
        {
            _moves_value = value;
            _moves_text.text = "Moves\n" + value;
        }
    }

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

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void ResetGame()
    {
        StopAllCoroutines();
        Bodies.ForEach(x => x.Reset());
        Score = 0;
        Moves = 0;
        Time = 0;
        Striker.ResetMover();
    }

    public void AddScore(short value)
    {
        Score += value;
    }

    public void AddMove()
    {
        if(Moves == 0)
        {
            StartCoroutine(MatchTimer());
        }
        Moves++;
    }

    IEnumerator MatchTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            Time++;
        }
    }

    public static bool IsGameOver
    {
        get
        {
            foreach(var body in Bodies)
            {
                if (body.gameObject.activeInHierarchy) return false;
            }
            Instance.StopAllCoroutines();
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
