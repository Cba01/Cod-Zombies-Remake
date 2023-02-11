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

    public GameObject pauseMenu;

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

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
