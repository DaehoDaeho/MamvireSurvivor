using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WeaponData
{
    public string weaponName;

    public float attackInterval = 1.0f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 8.0f;
    public int projectileDamage = 1;

    public float projectileLifetime = 2.0f;
    public float hitFeedbackStrength = 0.0f;

    public WeaponPatternType patternType = WeaponPatternType.Straight;
    public int projectileCount = 1;
    public float spreadAngleStep = 15.0f;

    public AudioClip fireSound = null;
    public GameObject fireEffectPrefab = null;
}

[CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Game/PlayerWeapon")]
public class PlayerWeaponData : ScriptableObject
{
    [SerializeField] List<WeaponData> playerWeaponDatas = new List<WeaponData>();

    public WeaponData CopyWeaponData(int index)
    {
        WeaponData newData = new WeaponData();
        newData.weaponName = playerWeaponDatas[index].weaponName;
        newData.attackInterval = playerWeaponDatas[index].attackInterval;
        newData.projectilePrefab = playerWeaponDatas[index].projectilePrefab;
        newData.projectileSpeed = playerWeaponDatas[index].projectileSpeed;
        newData.projectileDamage = playerWeaponDatas[index].projectileDamage;

        newData.projectileLifetime = playerWeaponDatas[index].projectileLifetime;
        newData.hitFeedbackStrength = playerWeaponDatas[index].hitFeedbackStrength;

        newData.patternType = playerWeaponDatas[index].patternType;
        newData.projectileCount = playerWeaponDatas[index].projectileCount;
        newData.spreadAngleStep = playerWeaponDatas[index].spreadAngleStep;

        newData.fireSound = playerWeaponDatas[index].fireSound;
        newData.fireEffectPrefab = playerWeaponDatas[index].fireEffectPrefab;

        return newData;
    }

    public WeaponData GetWeaponData(int index)
    {
        return playerWeaponDatas[index];
    }

    public int GetWeaponDataCount()
    {
        return playerWeaponDatas.Count;
    }

    public WeaponData[] GetAllWeaponDatas()
    {
        return playerWeaponDatas.ToArray();
    }
}
