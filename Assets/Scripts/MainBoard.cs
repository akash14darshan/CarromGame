using System.Collections.Generic;
using UnityEngine;

class MainBoard : MonoBehaviour
{
    public static readonly List<Rigidbody2D> Bodies = new List<Rigidbody2D>();
    void Awake()
    {
        foreach(var body in GetComponentsInChildren<Rigidbody2D>(true))
        {
            Bodies.Add(body);
        }
    }

    public static bool HasStopped
    {
        get
        {
            bool hasStopped = true;
            foreach (var body in Bodies)
            {
                if (body.IsSleeping() && body.velocity.magnitude == 0)
                    continue;
                else
                {
                    hasStopped = false;
                    break;
                }
            }
            return hasStopped;
        }
    }
}
