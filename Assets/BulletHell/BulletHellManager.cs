using System;
using System.Collections;
using System.Reflection.Emit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletHellManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Enemy;

    [SerializeField] TextMeshProUGUI InfoText;
    [SerializeField] Slider enemyLifeSlider;
    
    void Start()
    {
        InfoText.alpha = 0;

        Player.GetComponent<BHPlayer>().OnDestroy += OnPlayerDestroyed;
        Enemy.GetComponent<Enemy>().OnDestroy += OnEnemyDestroyed;
    }

    void Info(string info = "")
    {
        InfoText.enabled = (info != "");
        InfoText.text = info;
    }

    void OnPlayerDestroyed()
    {
        GameOver();
    }

    void OnEnemyDestroyed()
    {
        Info("CLEAR!");
        InfoText.alpha = 1;
    }

    void GameOver()
    {
        Info("GAME OVER");
        InfoText.alpha = 0;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        float timer = 0f;
        float duration = 3f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            InfoText.alpha = Mathf.Clamp01(timer / duration);
            yield return null;
        }
        InfoText.alpha = 1f;
    }
}
