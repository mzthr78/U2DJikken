using System;
using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Categorization;

public class brickbreaker : MonoBehaviour
{
    [SerializeField] GameObject ball;
    //[SerializeField] GameObject paddle;
    [SerializeField] GameObject brickPrefab;
    [SerializeField] GameObject abyss;
    [SerializeField] TextMeshProUGUI label;

    int count = 15;
    int life = 3;

    bool isPlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abyss.GetComponent<abyss>().OnFall += HandleBallFall;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject brick = Instantiate(brickPrefab, new Vector3(-3.8f + 2 * i, 1.6f + j, 0), Quaternion.identity);

                brick.GetComponent<brick>().OnDestroy += HandleBrickDestroy;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 旧式InputSystem
        if (!isPlaying && Input.GetKeyDown(KeyCode.Space)) // GetKeyだと連続して反応しちゃう？
        {
            Info("");
            ball.GetComponent<ball>().Fire();

            isPlaying = true;
        } 
    }

    void HandleBrickDestroy()
    {
        count -= 1;

        if (count <= 0)
        {
            Info("Clear!");
            Time.timeScale = 0f;
            return;
        }
    }

    void HandleBallFall()
    {
        life -= 1;

        if (life <= 0)
        {
            Info("GAME OVER");
            Time.timeScale = 0f;
            return;
        }

        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);

        ball.transform.position = Vector3.zero;
        ball.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

        Info("Press Space to start");

        isPlaying = false;
    }

    private void Info(String message = "")
    {
        label.enabled = (message != "");
        label.text = message;
    }
}
