using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DeathScreen : MonoBehaviour
{
    WaveManager waveManager;

    [Header("Death Screen")]
    public GameObject arms;
    public GameObject playerUi;
    public GameObject deathScreen;
    public Image deathScreenBackground;
    public TextMeshProUGUI roundsTxt;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }


    public void DeathScreenGO()
    {
        arms.SetActive(false);
        playerUi.SetActive(false);
        deathScreen.SetActive(true);
        roundsTxt.SetText(waveManager.currentRound + " Rounds");


        StartCoroutine(FadeBackground(1f, 5f));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator FadeBackground(float targetAlpha, float duration)
    {
        Color currentColor = deathScreenBackground.color;
        Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(currentColor.a, targetColor.a, timer / duration);
            deathScreenBackground.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            yield return null;
        }

        deathScreenBackground.color = targetColor;

    }
}
