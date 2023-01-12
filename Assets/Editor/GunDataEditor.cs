using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GunData))]
public class GunDataEditor : Editor
{

    SerializedObject soTarget;

    private SerializedProperty gunName;

    private SerializedProperty model;

    private SerializedProperty controller;

    private SerializedProperty damage;
    private SerializedProperty timeBetweenShooting;
    private SerializedProperty spread;
    private SerializedProperty range;
    private SerializedProperty timeBetweenShots;
    private SerializedProperty bulletsPerTap;
    private SerializedProperty allowButtonHold;

    private SerializedProperty initialAmmo;
    private SerializedProperty ammo;
    private SerializedProperty ammoNeeded;
    private SerializedProperty bulletsLeft;
    private SerializedProperty bulletsShot;
    private SerializedProperty magazineSize;
    private SerializedProperty magazineAmount;
    private SerializedProperty reloadTime;
    private SerializedProperty starterReloadTime;

    private SerializedProperty camShakeMagnitude;
    private SerializedProperty camShakeRoughness;
    private SerializedProperty camShakeFadeInTime;
    private SerializedProperty camShakeFadeOutTime;
    private SerializedProperty muzzleFlash;


    private SerializedProperty reloading;
    private SerializedProperty shooting;
    private SerializedProperty readyToShoot;


    private string[] tabs = { "Basic", "Shooting", "Reloading", "Effects" };
    private int tabIndex;

    private void OnEnable()
    {
        GunData gunData = (GunData)target;
        soTarget = new SerializedObject(target);

        gunName = soTarget.FindProperty("name");
        model = soTarget.FindProperty("model");
        controller = soTarget.FindProperty("controller");
        damage = soTarget.FindProperty("damage");
        timeBetweenShooting = soTarget.FindProperty("timeBetweenShooting");
        spread = soTarget.FindProperty("spread");
        range = soTarget.FindProperty("range");
        timeBetweenShots = soTarget.FindProperty("timeBetweenShots");
        bulletsPerTap = soTarget.FindProperty("bulletsPerTap");
        allowButtonHold = soTarget.FindProperty("allowButtonHold");

        initialAmmo = soTarget.FindProperty("initialAmmo");
        ammo = soTarget.FindProperty("ammo");
        ammoNeeded = soTarget.FindProperty("ammoNeeded");
        bulletsLeft = soTarget.FindProperty("bulletsLeft");
        bulletsShot = soTarget.FindProperty("bulletsShot");
        magazineSize = soTarget.FindProperty("magazineSize");
        magazineAmount = soTarget.FindProperty("magazineAmount");
        reloadTime = soTarget.FindProperty("reloadTime");
        starterReloadTime = soTarget.FindProperty("starterReloadTime");

        camShakeMagnitude = soTarget.FindProperty("camShakeMagnitude");
        camShakeRoughness = soTarget.FindProperty("camShakeRoughness");
        camShakeFadeInTime = soTarget.FindProperty("camShakeFadeInTime");
        camShakeFadeOutTime = soTarget.FindProperty("camShakeFadeOutTime");
        muzzleFlash = soTarget.FindProperty("muzzleFlash");

        reloading = soTarget.FindProperty("reloading");
        shooting = soTarget.FindProperty("shooting");
        readyToShoot = soTarget.FindProperty("readyToShoot");

    }

    public override void OnInspectorGUI()
    {
        soTarget.Update();
        EditorGUILayout.BeginVertical();
        tabIndex = GUILayout.Toolbar(tabIndex, tabs);
        EditorGUILayout.EndVertical();

        switch (tabIndex)
        {
            case 0:
                BasicTab();
                break;
            case 1:
                ShootingTab();
                break;
            case 2:
                ReloadingTab();
                break;
            case 3:
                EffectsTab();
                break;
            default:

                break;
        }

        if (GUI.changed)
        {
            Debug.Log("Text field has changed.");
        }
        soTarget.ApplyModifiedProperties();
    }

    private void BasicTab()
    {
        EditorGUILayout.Space(3);

        EditorGUILayout.PropertyField(gunName);
        EditorGUILayout.PropertyField(model);
        EditorGUILayout.PropertyField(controller);
        EditorGUILayout.PropertyField(reloading);
        EditorGUILayout.PropertyField(shooting);
        EditorGUILayout.PropertyField(readyToShoot);
        /* GUILayout.Label("Info", EditorStyles.boldLabel);
        GunData gunData = (GunData)target;
        gunData.name = EditorGUILayout.TextField(gunData.name, gunData.name);
        gunData.model = (GameObject)EditorGUILayout.ObjectField("Model Prefab", gunData.model, typeof(GameObject), false);
        gunData.controller = (AnimatorOverrideController)EditorGUILayout.ObjectField("Animator Override", gunData.controller, typeof(AnimatorOverrideController), false);
        gunData.reloading = EditorGUILayout.Toggle("Reloading", gunData.reloading);
        gunData.shooting = EditorGUILayout.Toggle("Shooting", gunData.shooting);
        gunData.readyToShoot = EditorGUILayout.Toggle("Ready To Shoot", gunData.readyToShoot); */
    }
    private void ShootingTab()
    {
        EditorGUILayout.Space(3);

        EditorGUILayout.PropertyField(damage);
        EditorGUILayout.PropertyField(timeBetweenShooting);
        EditorGUILayout.PropertyField(timeBetweenShots);
        EditorGUILayout.PropertyField(spread);
        EditorGUILayout.PropertyField(range);
        EditorGUILayout.PropertyField(bulletsPerTap);
        EditorGUILayout.PropertyField(allowButtonHold);


        /* 
                GunData gunData = (GunData)target;
                gunData.damage = EditorGUILayout.IntField("Damage", gunData.damage);
                gunData.timeBetweenShooting = EditorGUILayout.FloatField("Time Between Shooting", gunData.timeBetweenShooting);
                gunData.timeBetweenShots = EditorGUILayout.FloatField("Time Between Shots", gunData.timeBetweenShots);
                gunData.spread = EditorGUILayout.Slider("Spread", gunData.spread, 0, 10);
                gunData.range = EditorGUILayout.Slider("Range", gunData.range, 0, 200);
                gunData.bulletsPerTap = EditorGUILayout.IntField("Bullets Per Tap", gunData.bulletsPerTap);
                gunData.allowButtonHold = EditorGUILayout.Toggle("Allow Button Hold", gunData.allowButtonHold); */



    }
    private void ReloadingTab()
    {
        EditorGUILayout.Space(3);

        GUILayout.Label("Ammo", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(initialAmmo);
        EditorGUILayout.PropertyField(ammo);
        EditorGUILayout.PropertyField(ammoNeeded);

        EditorGUILayout.Space(3);

        GUILayout.Label("Bullets", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(bulletsLeft);
        EditorGUILayout.PropertyField(bulletsShot);
        EditorGUILayout.PropertyField(magazineSize);
        EditorGUILayout.PropertyField(magazineAmount);

        EditorGUILayout.Space(3);

        GUILayout.Label("Reload", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(reloadTime);
        EditorGUILayout.Space(1);
        EditorGUILayout.PropertyField(starterReloadTime);


        /*  GunData gunData = (GunData)target;
         gunData.bulletsLeft = EditorGUILayout.IntField("Bullets Left", gunData.bulletsLeft);
         gunData.bulletsShot = EditorGUILayout.IntField("Bullets Shot", gunData.bulletsShot);
         gunData.magazineSize = EditorGUILayout.IntField("Magazine Size", gunData.magazineSize);
         gunData.magazineAmount = EditorGUILayout.IntField("Magazine Amount", gunData.magazineAmount);
         gunData.reloadTime = EditorGUILayout.FloatField("Reload Time", gunData.reloadTime); */
    }
    private void EffectsTab()
    {
        EditorGUILayout.Space(3);

        GUILayout.Label("Camera Effect", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(camShakeMagnitude);
        EditorGUILayout.PropertyField(camShakeRoughness);
        EditorGUILayout.PropertyField(camShakeFadeInTime);
        EditorGUILayout.PropertyField(camShakeFadeOutTime);

        EditorGUILayout.Space(3);

        GUILayout.Label("MuzzleFlash Effect", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(muzzleFlash);


        /* GUILayout.Label("Camera Shaker", EditorStyles.boldLabel);
        GunData gunData = (GunData)target;
        gunData.camShakeMagnitude = EditorGUILayout.FloatField("Camera Shake Magnitude", gunData.camShakeMagnitude);
        gunData.camShakeRoughness = EditorGUILayout.FloatField("Camera Shake Roughness", gunData.camShakeRoughness);
        gunData.camShakeFadeInTime = EditorGUILayout.FloatField("Camera Shake Fade In Time", gunData.camShakeFadeInTime);
        gunData.camShakeFadeOutTime = EditorGUILayout.FloatField("Camera Shake Fade Out Time", gunData.camShakeFadeOutTime);

        EditorGUILayout.Space();
        GUILayout.Label("Muzzle Flash Prefab", EditorStyles.boldLabel);
        gunData.muzzleFlash = (GameObject)EditorGUILayout.ObjectField("Muzzle Flash Effect", gunData.muzzleFlash, typeof(GameObject), false);

        EditorGUILayout.Space();
        GUILayout.Label("Bullet Hole Prefab", EditorStyles.boldLabel); */


    }


}
