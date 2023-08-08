using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Environment : MonoBehaviour
{
    [SerializeField] private float force = 4f;
    private bool isAtDeathMenu = false;
    private bool isAtStartMenu = true;

    private GameObject env;
    private GameObject player;

    private GameObject startText;
    private GameObject restartText;
    private GameObject hiscoreText;

    private GameObject type1;

    private GameObject powerup;
    private GameObject point;

    private List<GameObject> list;

    void Start()
    {
        list = new List<GameObject>();

        powerup = (GameObject)Resources.Load("prefabs/powerup", typeof(GameObject));
        point = (GameObject)Resources.Load("prefabs/point", typeof(GameObject));

        type1 = (GameObject)Resources.Load("prefabs/obstacle", typeof(GameObject));

        env = GameObject.Find("Environment");
        player = GameObject.Find("Player");

        GameObject canvas = GameObject.Find("Canvas");
        startText = canvas.transform.Find("StartText").gameObject;
        restartText = canvas.transform.Find("RestartText").gameObject;
        hiscoreText = canvas.transform.Find("HiscoreText").gameObject;
        hiscoreText.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + player.GetComponent<Player>().ReadFromFile();

        Level_init();

        env.GetComponent<Rigidbody2D>().isKinematic = true;

        restartText.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    void Update()
    {
        if (isAtStartMenu && Input.GetKeyDown("space"))
        {
            startText.GetComponent<TextMeshProUGUI>().enabled = false;
            hiscoreText.GetComponent<TextMeshProUGUI>().enabled = false;

            env.GetComponent<Rigidbody2D>().isKinematic = false;

            isAtStartMenu = false;

            GetComponent<Rigidbody2D>().velocity = -Vector2.up * force;
            GetComponent<AudioSource>().Play();

            return;
        }
        else if (isAtDeathMenu && Input.GetKeyDown(KeyCode.R))
        {
            startText.GetComponent<TextMeshProUGUI>().enabled = true;
            hiscoreText.GetComponent<TextMeshProUGUI>().enabled = true;
            hiscoreText.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + player.GetComponent<Player>().ReadFromFile();

            restartText.GetComponent<TextMeshProUGUI>().enabled = false;

            env.transform.position = Vector2.zero;

            for(int i = 0; i < env.transform.childCount; i++)
            {
                GameObject g = env.transform.GetChild(i).gameObject;
                Destroy(g);
            }

            list = new List<GameObject>();
            Level_init();

            isAtDeathMenu = false;
            isAtStartMenu = true;

            player.GetComponent<Player>().Restart();

            return;

        } else if (isAtStartMenu || isAtDeathMenu)
        {
            return;
        }

        if (player.GetComponent<Player>().GetDead())
        {
            isAtDeathMenu = true;

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;

            restartText.GetComponent<TextMeshProUGUI>().enabled = true;

            player.GetComponent<Player>().Restart();

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * force;
            GetComponent<AudioSource>().Play();
        }

        foreach (GameObject g in list)
        {
            if (g.transform.position.y < -10f)
            {
                list.Remove(g);
                if (g.name.Contains("Obstacle"))
                {
                    list.Add(Instantiate(type1, new Vector2(0f, 10f), Quaternion.identity, env.transform));
                }
                else
                {
                    list.Add(Instantiate(powerup, new Vector2(0f, 10f), Quaternion.identity, env.transform));
                }
                Destroy(g);
                break;
            }
        }
    }

    private void Level_init()
    {
        for (int y = 0; y < 4; y++)
        {
            list.Add(Instantiate(type1, new Vector2(0f, (5 * y) + 5), Quaternion.identity, env.transform));
            list.Add(Instantiate(powerup, new Vector2(0f, (5 * y) + 7.5f), Quaternion.identity, env.transform));
        }
    }
}
