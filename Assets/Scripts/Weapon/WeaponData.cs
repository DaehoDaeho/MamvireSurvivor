using UnityEngine;

/// <summary>
/// 직렬화 클래스.
/// 클래스 멤버 변수의 목록을 Inspector 창에 노출되게 만들기 위해서 사용.
/// 
/// 하나의 무기에 필요한 발사 데이터를 묶어서 저장하는 역할.
/// </summary>
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

    public AudioClip fireSound = null;
    public GameObject fireEffectPrefab = null;
}
