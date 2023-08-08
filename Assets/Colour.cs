using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Colour : MonoBehaviour
{
    private int col;

    private void Start()
    {
        if (name == "Red")
        {
            col = 0;
        }
        else if (name == "Green")
        {
            col = 1;
        }
        else if (name == "Blue")
        {
            col = 2;
        }
        else if (name == "Yellow")
        {
            col = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && col != collision.gameObject.GetComponent<Player>().GetColour())
        {
            collision.GetComponent<Player>().Kill();
        }
        else if (collision.gameObject.name == "Boundary")
        {
            GameObject.Find("Player").GetComponent<Player>().Kill();
        }
    }
}
