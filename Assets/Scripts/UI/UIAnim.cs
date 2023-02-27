using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIAnim : MonoBehaviour
{

    public Transform ScorePosition;
    public Color originalColorRound = new Color(185, 25, 25, 255);


    public TextMeshProUGUI roundTxt;


    [Header("Hit Marker")]
    public Transform hitMarkerSpawn;
    public Image hitMarker;


    public void AddScore(string score)
    {
        GameObject scoreEarned = new GameObject("ScoreEarned");
        scoreEarned.transform.SetParent(ScorePosition.transform, false);
        scoreEarned.transform.position = ScorePosition.transform.position;
        TextMeshProUGUI scoreText = scoreEarned.AddComponent<TextMeshProUGUI>();
        scoreText.text = "+" + score;
        scoreText.fontSize = 10;
        scoreText.color = Color.yellow;
        scoreText.alignment = TextAlignmentOptions.Center;
        scoreText.alignment = TextAlignmentOptions.CenterGeoAligned;
        scoreEarned.transform.DOLocalMove(new Vector2(-80, Random.Range(-20, 20)), 1).SetEase(Ease.OutCubic);
        scoreText.DOFade(0, 1).SetEase(Ease.InQuint);
        Destroy(scoreEarned, 1);
    }

    public void DiscountScore(string score)
    {
        GameObject scoreEarned = new GameObject("ScoreEarned");
        scoreEarned.transform.SetParent(ScorePosition.transform, false);
        scoreEarned.transform.position = ScorePosition.transform.position;
        TextMeshProUGUI scoreText = scoreEarned.AddComponent<TextMeshProUGUI>();
        scoreText.text = "-" + score;
        scoreText.fontSize = 15;
        scoreText.color = Color.red;
        scoreText.alignment = TextAlignmentOptions.Center;
        scoreText.alignment = TextAlignmentOptions.CenterGeoAligned;
        scoreEarned.transform.DOLocalMove(new Vector2(-80, Random.Range(-20, 20)), 1).SetEase(Ease.OutCubic);
        scoreText.DOFade(0, 1).SetEase(Ease.InQuint);
        Destroy(scoreEarned, 1);
    }

    public async void ChangeRoundAnimation()
    {

        await roundTxt.DOColor(Color.white, 2f).SetEase(Ease.InBounce).AsyncWaitForCompletion();
        roundTxt.DOColor(originalColorRound, 4f).SetEase(Ease.OutBounce);

    }

    public void PerkAnimation(Transform perkPivot)
    {
        perkPivot.DOShakeScale(0.5f,0.3f,10,90f,true);
    }

    






}
