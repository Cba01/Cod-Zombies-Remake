using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class WeaponSystem : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.WeaponActions weaponActions;
    public Vector3 parentOverride;
    private InputHandler inputHandler;
    private PlayerWeapon playerWeapon;
    private PlayerLocomotion playerLocomotion;

    [Header("References")]
    [SerializeField]
    public GunData gunData;
    [SerializeField]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AnimatorOverrideController overrideAnim;
    private HandleAnimations handleAnimations;


    [Header("Bullet Hole Graphics")]
    public GameObject bulletHoleGround;
    public GameObject bulletHoleEnemy;

    [Header("Player UI")]
    public TextMeshProUGUI text;
    private PlayerUI playerUI;



    private void Awake()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        fpsCam = camera.GetComponent<Camera>();
        playerUI = FindObjectOfType<PlayerUI>();
        gunData.bulletsLeft = gunData.magazineSize;
        gunData.readyToShoot = true;
        gunData.ammoNeeded = 0;
        gunData.ammo = gunData.initialAmmo;
        inputHandler = FindObjectOfType<InputHandler>();
        playerWeapon = FindObjectOfType<PlayerWeapon>();
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        handleAnimations = FindObjectOfType<HandleAnimations>();
        handleAnimations.SetAnimationController(overrideAnim);


    }
    private void Start()
    {

    }
    private void Update()
    {
        Debug.DrawRay(playerLocomotion.cam.transform.position, playerLocomotion.cam.transform.forward * 100, Color.green);
        ShootInput();
        playerUI.UpdateBullets(gunData.bulletsLeft, gunData.ammo);

    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

    }

    private void OnEnable()
    {
        handleAnimations.GunUpAnimation();
        handleAnimations.SetAnimationController(overrideAnim);

    }

    public void ShootInput()
    {
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 100, Color.blue);

        if (gunData.allowButtonHold)
        {
            gunData.shooting = inputHandler.shootHold_Input;
        }
        else
        {
            gunData.shooting = inputHandler.shootOnce_Input;
        }

        if (inputHandler.reload_Input && gunData.bulletsLeft < gunData.magazineSize && !gunData.reloading && gunData.ammo > 0)
        {
            Reload();
        }


        //Shoot
        if (gunData.readyToShoot && gunData.shooting && !gunData.reloading && gunData.bulletsLeft > 0)
        {
            gunData.bulletsShot = gunData.bulletsPerTap;

            Shoot();


        }
        else
        {

        }
    }

    private void Shoot()
    {

        gunData.readyToShoot = false;
        playerLocomotion.canSprint = false;


        //Spread
        float x = Random.Range(-gunData.spread, gunData.spread);
        float y = Random.Range(-gunData.spread, gunData.spread);
        float z = Random.Range(-gunData.spread, gunData.spread);



        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, z);

        //Raycast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, gunData.range))
        {
            Debug.DrawLine(fpsCam.transform.position, rayHit.point, Color.red, 1.0f);
            Debug.Log(rayHit.transform.tag);

            switch (rayHit.collider.tag)
            {

                case "ZombieLegs":
                    rayHit.collider.gameObject.GetComponentInParent<Zombie>().TakeDamage(gunData.damage * 1, false);
                    Debug.Log("ZombieLegs");
                    break;

                case "ZombieTorso":
                    rayHit.collider.gameObject.GetComponentInParent<Zombie>().TakeDamage(gunData.damage * 1.5f, false);
                    Debug.Log("ZombieTorso");

                    break;
                case "ZombieHead":
                    rayHit.collider.gameObject.GetComponentInParent<Zombie>().TakeDamage(gunData.damage * 2f, true);
                    Debug.Log("ZombieHead");

                    break;
                case "ZombieArms":
                    rayHit.collider.gameObject.GetComponentInParent<Zombie>().TakeDamage(gunData.damage * 1, false);
                    Debug.Log("ZombieArms");

                    break;
            }
        }
        else
        {
        }

        //Effects
        if (rayHit.transform.tag == "Enemy")
        {
            Instantiate(bulletHoleEnemy, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        }
        else
        {
            Instantiate(bulletHoleGround, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        }

        GameObject muzzleFlashGO = (GameObject)Instantiate(gunData.muzzleFlash, attackPoint.position, Quaternion.identity);
        muzzleFlashGO.transform.parent = attackPoint;

        CameraShaker.Instance.ShakeOnce(gunData.camShakeMagnitude, gunData.camShakeRoughness, gunData.camShakeFadeInTime, gunData.camShakeFadeOutTime);

        playerLocomotion.HandleShootAnimation();
        /* anim.CrossFade("Shoot", 1f, -1, 0f); */

        gunData.bulletsLeft--;
        gunData.bulletsShot--;
        gunData.ammoNeeded++;


        Invoke("ResetShot", gunData.timeBetweenShooting);

        if (gunData.bulletsShot > 0 && gunData.bulletsLeft > 0)
        {
            Invoke("Shoot", gunData.timeBetweenShots);
        }




    }


    private void ResetShot()
    {
        gunData.readyToShoot = true;
        playerLocomotion.canSprint = true;

    }

    private void Reload()
    {
        gunData.reloading = true;
        playerLocomotion.canSprint = false;
        handleAnimations.PlayReloadAnimation();
        Invoke("ReloadFinished", gunData.reloadTime);


    }
    private void ReloadFinished()
    {
        gunData.bulletsLeft = gunData.magazineSize;
        gunData.reloading = false;
        gunData.ammo -= gunData.ammoNeeded;
        gunData.ammoNeeded = 0;
        playerLocomotion.canSprint = true;
        ResetShot();
    }

}
