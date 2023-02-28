using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveManager : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public GameObject zombiePrefab;
    private GameObject roundText;

    private UIAnim uIAnim;

    public float currentRound = 1;
    public int zombiesInRound;
    int spawnedZombies = 0;
    public int remainingZombies;

    [Header("Audio")]
    public AudioClip changeRoundSound;
    private AudioSource audioSource;



    void Start()
    {
        uIAnim = FindObjectOfType<UIAnim>();
        audioSource = GetComponent<AudioSource>();
        roundText = GameObject.FindGameObjectWithTag("RoundUI");

        ZombiesInRound();
        StartCoroutine(SpawnZombies());

        roundText.GetComponent<TextMeshProUGUI>().text = "" + currentRound;
    }

    void Update()
    {
        if (remainingZombies <= 0)
        {
            StartCoroutine(ChangeRound());
        }
    }

    IEnumerator ChangeRound()
    {
        spawnedZombies = 0;
        currentRound++;

        ZombiesInRound();

        yield return new WaitForSeconds(0.6f);
        //cambiar la UI del round
        audioSource.PlayOneShot(changeRoundSound);
        uIAnim.ChangeRoundAnimation();
        roundText.GetComponent<TextMeshProUGUI>().text = "" + currentRound;
        StartCoroutine(SpawnZombies());

    }
    IEnumerator SpawnZombies()
    {
        yield return new WaitForSeconds(5);
        if (spawnedZombies < zombiesInRound)
        {
            int chosenSpawn = Random.Range(0, spawnPoints.Length);
            GameObject zombie = Instantiate(zombiePrefab, spawnPoints[chosenSpawn].transform.position, Quaternion.identity);
            zombie.GetComponent<Zombie>().health = ZombieHealth();
            zombie.GetComponent<Zombie>().walkSpeed *= ZombieSpeed();
            zombie.GetComponent<Zombie>().attackDamage *= ZombieAttackDamage();
            spawnedZombies++;
        }
        float substractTime = (currentRound / 100) * 25;
        yield return new WaitForSeconds(Random.Range(5 - substractTime, 10));

        if (spawnedZombies < zombiesInRound)
        {
            StartCoroutine(SpawnZombies());
        }
    }
    float ZombieHealth()
    {
        float health;
        if (currentRound > 9)
        {
            health = 950 * Mathf.Pow(1.1f, (currentRound - 9));
            return health;
        }
        else
        {
            health = 100 * currentRound;
            return health;
        }
    }

    float ZombieSpeed()
    {
        float speed;
        if (currentRound > 9)
        {
            speed = 2;
            return speed;
        }
        else if (currentRound > 4)
        {
            speed = 1.5f;
            return speed;
        }
        else
        {
            speed = 1;
            return speed;
        }
    }

    float ZombieAttackDamage()
    {
        float attackDamage;
        if (currentRound > 5)
        {
            attackDamage = 2;
            return attackDamage;
        }
        else
        {
            attackDamage = 1;
            return attackDamage;
        }
        

    }

    void ZombiesInRound()
    {
        zombiesInRound = Mathf.RoundToInt(currentRound * 3f);
        remainingZombies = zombiesInRound;
    }
}
