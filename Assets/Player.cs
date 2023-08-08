using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    private string path = "Assets/Data/hiscore.txt";

    private bool isDead = false;
    private int col;
    private SpriteRenderer sprite;

    private GameObject scoreText;
    private GameObject death_effect;

    private int score = 0;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        scoreText = GameObject.Find("ScoreText");
        death_effect = (GameObject)Resources.Load("prefabs/Death_effect", typeof(GameObject));

        ChangeCol(UnityEngine.Random.Range(0, 4));
    }

    public void Kill()
    {
        GetComponent<AudioSource>().Play();

        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        isDead = true;

        if (ReadFromFile() < score)
        {
            WriteToFile(score);
        }

        var main = death_effect.GetComponent<ParticleSystem>().main;
        main.startColor = GetComponent<SpriteRenderer>().color;
        GameObject g = Instantiate(death_effect, transform);
        Destroy(g, 2f);

        score = 0;
    }

    public void Restart()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;

        isDead = false;
    }

    public bool GetDead()
    {
        return isDead;
    }

    public int GetColour()
    {
        return col;
    }

    public void ChangeCol(int new_col)
    {
        col = new_col;

        switch (col)
        {
            case 0:
                sprite.color = Color.red;
                break;
            case 1:
                sprite.color = Color.green;
                break;
            case 2:
                sprite.color = Color.blue;
                break;
            case 3:
                sprite.color = Color.yellow;
                break;
        }

        scoreText.GetComponent<TextMeshProUGUI>().color = sprite.color;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.GetComponent<TextMeshProUGUI>().text = "" + score;
    }

    private void WriteToFile(int sc)
    {
        StreamWriter w = new StreamWriter(path);
        w.WriteLine(sc);
        w.Flush();
        w.Close();
    }

    public int ReadFromFile()
    {
        int hiscore;
        StreamReader r = new StreamReader(path);
        hiscore = int.Parse(r.ReadLine());
        r.Close();
        return hiscore;
    }
}
