using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : Interactable
{
    [SerializeField]
    private GameObject mysteryBox;
    [SerializeField]
    private bool mysteryBoxOpen = false;

    private WeaponHolderSlot weaponHolderSlot;


    private Animation weaponAnimation;
    private Animator mysteryBoxAnimator;

    public GameObject[] guns;
    public GameObject[] gunsPrefab;
    public Transform cubePosition;

    [SerializeField]
    private float timer;
    int counter, counterCompare;

    [SerializeField]
    private int lastSelectedGun;
    public int selectedGun;

    [Header("Audio")]
    public AudioClip music;
    private AudioSource audioSource;


    public bool weaponSelected = false;


    // Start is called before the first frame update
    void Start()
    {
        weaponAnimation = GetComponentInChildren<Animation>();
        mysteryBoxAnimator = GetComponent<Animator>();
        weaponHolderSlot = FindObjectOfType<WeaponHolderSlot>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (mysteryBoxOpen == true)
        {

            if (weaponAnimation.IsPlaying("WeaponShow_MysteryBox"))
            {
                timer += Time.deltaTime;

                if (timer < 4.0f && counter < counterCompare)
                {
                    counter++;
                }
                else if (counter == counterCompare)
                {
                    counter = 0;
                    RandomizeWeapon();
                    counterCompare++;
                }
                else if (timer > 4.0f && mysteryBoxOpen)
                {
                    weaponSelected = true;
                }
                guns[selectedGun].transform.position = cubePosition.transform.position;
            }
            else
            {
                weaponSelected = false;
                counter = 0;
                counterCompare = 0;
                timer = 0;
                mysteryBoxOpen = false;
                mysteryBoxAnimator.SetBool("isOpen", mysteryBoxOpen);

            }
        }


    }

    protected override void Interact()
    {


        if (weaponSelected)
        {
            lastSelectedGun = selectedGun;
            mysteryBoxOpen = false;
            weaponHolderSlot.LoadWeaponModel(gunsPrefab[selectedGun]);
            weaponSelected = false;
            mysteryBoxAnimator.SetBool("isOpen", mysteryBoxOpen);
            DisableGuns();
            counter = 0;
            counterCompare = 0;
            timer = 0;
            weaponAnimation.Stop();

        }
        else
        {
            mysteryBoxOpen = true;
            mysteryBoxAnimator.SetBool("isOpen", mysteryBoxOpen);
            weaponAnimation.Play();
            audioSource.PlayOneShot(music);

        }


    }

    void RandomizeWeapon()
    {
        int gunCount = guns.Length;
        int rand = Random.Range(0, gunCount);
        while (rand == selectedGun)
        {
            rand = Random.Range(0, gunCount);
        }
        selectedGun = rand;

        DisableGuns();

        guns[selectedGun].SetActive(true);
        guns[selectedGun].transform.position = cubePosition.transform.position;

    }

    void DisableGuns()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
    }
}
