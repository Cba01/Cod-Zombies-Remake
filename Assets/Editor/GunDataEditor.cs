using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GunData))]
public class GunDataEditor : Editor
{
    private string[] tabs = { "Basic", "Shooting", "Reloading", "Effects" };
    private int tabIndex;

    public override void OnInspectorGUI()
    {
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
    }

    private void BasicTab()
    {
        EditorGUILayout.Space(3);

        GUILayout.Label("Info", EditorStyles.boldLabel);
        GunData gunData = (GunData)target;
        gunData.name = EditorGUILayout.TextField(gunData.name, gunData.name);
        gunData.model = (GameObject)EditorGUILayout.ObjectField("Model Prefab", gunData.model, typeof(GameObject), false);
        gunData.controller = (AnimatorOverrideController)EditorGUILayout.ObjectField("Animator Override", gunData.controller, typeof(AnimatorOverrideController), false);
        gunData.reloading = EditorGUILayout.Toggle("Reloading", gunData.reloading);
        gunData.shooting = EditorGUILayout.Toggle("Shooting", gunData.shooting);
        gunData.readyToShoot = EditorGUILayout.Toggle("Ready To Shoot", gunData.readyToShoot);
    }
    private void ShootingTab()
    {
        EditorGUILayout.Space(3);

        GunData gunData = (GunData)target;
        gunData.damage = EditorGUILayout.IntField("Damage", gunData.damage);
        gunData.timeBetweenShooting = EditorGUILayout.FloatField("Time Between Shooting", gunData.timeBetweenShooting);
        gunData.timeBetweenShots = EditorGUILayout.FloatField("Time Between Shots", gunData.timeBetweenShots);
        gunData.spread = EditorGUILayout.Slider("Spread", gunData.spread, 0, 10);
        gunData.range = EditorGUILayout.Slider("Range", gunData.range, 0, 200);
        gunData.bulletsPerTap = EditorGUILayout.IntField("Bullets Per Tap", gunData.bulletsPerTap);
        gunData.allowButtonHold = EditorGUILayout.Toggle("Allow Button Hold", gunData.allowButtonHold);



    }
    private void ReloadingTab()
    {
        EditorGUILayout.Space(3);

        GunData gunData = (GunData)target;
        gunData.bulletsLeft = EditorGUILayout.IntField("Bullets Left", gunData.bulletsLeft);
        gunData.bulletsShot = EditorGUILayout.IntField("Bullets Shot", gunData.bulletsShot);
        gunData.magazineSize = EditorGUILayout.IntField("Magazine Size", gunData.magazineSize);
        gunData.magazineAmount = EditorGUILayout.IntField("Magazine Amount", gunData.magazineAmount);
        gunData.reloadTime = EditorGUILayout.FloatField("Reload Time", gunData.reloadTime);
    }
    private void EffectsTab()
    {
        EditorGUILayout.Space(3);

        GUILayout.Label("Camera Shaker", EditorStyles.boldLabel);
        GunData gunData = (GunData)target;
        gunData.camShakeMagnitude = EditorGUILayout.FloatField("Camera Shake Magnitude", gunData.camShakeMagnitude);
        gunData.camShakeRoughness = EditorGUILayout.FloatField("Camera Shake Roughness", gunData.camShakeRoughness);
        gunData.camShakeFadeInTime = EditorGUILayout.FloatField("Camera Shake Fade In Time", gunData.camShakeFadeInTime);
        gunData.camShakeFadeOutTime = EditorGUILayout.FloatField("Camera Shake Fade Out Time", gunData.camShakeFadeOutTime);

        EditorGUILayout.Space();
        GUILayout.Label("Muzzle Flash Prefab", EditorStyles.boldLabel);
        gunData.muzzleFlash = (GameObject)EditorGUILayout.ObjectField("Muzzle Flash Effect", gunData.muzzleFlash, typeof(GameObject), false);

        EditorGUILayout.Space();
        GUILayout.Label("Bullet Hole Prefab", EditorStyles.boldLabel);


    }


}
