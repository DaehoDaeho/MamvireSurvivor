using UnityEngine;

/// <summary>
/// WeaponController가 들고 있는 무기 목록을 순회하고, 특정 조건의 무기를 찾는 예제를 보여주기 위한 디버그용 스크립트.
/// </summary>
public class WeaponCollectionDebugger : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrintAllWeapons();
        PrintSlowestWeapon();
    }

    /// <summary>
    /// 현재 등록된 모든 무기 데이터를 순회하여 로그로 출력하는 함수.
    /// </summary>
    void PrintAllWeapons()
    {
        WeaponData[] allWeapons = weaponController.GetAllWeapons();

        for(int i=0; i<allWeapons.Length; ++i)
        {
            Debug.Log("무기 Index = " + i + ", 이름 = " + allWeapons[i].weaponName +
                ", attackInterval = " + allWeapons[i].attackInterval);
        }
    }

    /// <summary>
    /// 등록된 무기 중 공격 주기가 긴 무기를 찾아 출력하는 함수.
    /// </summary>
    void PrintSlowestWeapon()
    {
        WeaponData[] allWeapons = weaponController.GetAllWeapons();

        WeaponData slowestWeapon = null;

        for (int i = 0; i < allWeapons.Length; ++i)
        {
            WeaponData currentData = allWeapons[i];

            if(slowestWeapon == null)
            {
                slowestWeapon = currentData;
                continue;
            }

            if(currentData.attackInterval > slowestWeapon.attackInterval)
            {
                slowestWeapon = currentData;
            }
        }

        if(slowestWeapon != null)
        {
            Debug.Log("가장 느린 무기 이름 = " + slowestWeapon.weaponName +
                ", attackInterval = " + slowestWeapon.attackInterval);
        }
    }
}
