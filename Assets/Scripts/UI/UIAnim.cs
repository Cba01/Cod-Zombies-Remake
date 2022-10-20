using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class UIAnim : MonoBehaviour
{
    public void AddScore(string score)
    {
        GameObject scoreEarned = new GameObject("ScoreEarned");
        scoreEarned.transform.SetParent(this.transform);
        scoreEarned.transform.position = this.transform.position;
        TextMeshProUGUI scoreText = scoreEarned.AddComponent<TextMeshProUGUI>();
        scoreText.text = "+" + score;
        scoreText.fontSize = 20;
        scoreText.color = Color.yellow;
        scoreText.alignment = TextAlignmentOptions.Center;
        scoreText.alignment = TextAlignmentOptions.CenterGeoAligned;
        scoreEarned.transform.DOLocalMove(new Vector2(-100, Random.Range(-20, 20)), 1).SetEase(Ease.OutCubic);
        scoreText.DOFade(0 , 1).SetEase(Ease.InQuint);
        Destroy(scoreEarned, 1);
    }
}
