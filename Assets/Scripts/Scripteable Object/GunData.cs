using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    /*     [Header("Info")]
     */
    public new string name;

    /*     [Header("Model Prefab")]
     */
    public GameObject model;

    /*     [Header("Animator Controller")]
     */
    public AnimatorOverrideController controller;

    /*     [Header("Shooting")]
     */
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float timeBetweenShots;
    public int bulletsPerTap;
    public bool allowButtonHold;

    /*     [Header("Reloading")]
     */
    public int initialAmmo;
    public int ammo;
    public int ammoNeeded;
    public int bulletsLeft;
    public int bulletsShot;
    public int magazineSize;
    public int magazineAmount;
    public float reloadTime;
    public float starterReloadTime;

    /*     [Header("Effects")]
     */
    public float camShakeMagnitude;
    public float camShakeRoughness;
    public float camShakeFadeInTime;
    public float camShakeFadeOutTime;
    public GameObject muzzleFlash;


    /*     [Header("Flags (NO MODIFY)")]
     */
    public bool reloading;
    public bool shooting;
    public bool readyToShoot;
}
