using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerUI : MonoBehaviour
{
    PlayerStats playerStats;

    [SerializeField]
    private TextMeshProUGUI prompText;
    public TextMeshProUGUI bulletText;

    [Header("Player Balance")]
    [SerializeField]
    private TextMeshProUGUI balanceText;


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
    }

    public void UpdateBullets(int bulletsLeft, int ammo)
    {

        bulletText.SetText(bulletsLeft + "|" + ammo);
    }

    public void UpdateText(string prompMessage)
    {
        prompText.text = prompMessage;
    }

    public void UpdateBalance()
    {
        balanceText.SetText("$"+playerStats.balance);
    }
}
