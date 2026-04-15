using UnityEngine;

/// <summary>
/// 플레이어가 가진 무기 목록과 현재 장착 무기를 관리하는 역할.
/// </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData[] weapons;

    [SerializeField] private int currentWeaponIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            EquipWeaponByIndex(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            EquipWeaponByIndex(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            EquipWeaponByIndex(2);
        }
    }

    /// <summary>
    /// 지정된 인덱스의 무기를 장착.
    /// </summary>
    /// <param name="weaponIndex"></param>
    public void EquipWeaponByIndex(int weaponIndex)
    {
        if(weapons == null)
        {
            return;
        }

        if(weaponIndex < 0 || weaponIndex >= weapons.Length)
        {
            return;
        }

        WeaponData targetWeapon = weapons[weaponIndex];

        if(targetWeapon == null)
        {
            return;
        }

        currentWeaponIndex = weaponIndex;

        Debug.Log("무기 장착 변경, 현재 무기 인덱스 = " + currentWeaponIndex + ", 무기 이름 = " + targetWeapon.weaponName);
    }

    /// <summary>
    /// 현재 장착 중인 무기 데이터를 반환.
    /// </summary>
    /// <returns>현재 장착 중인 무기 데이터</returns>
    public WeaponData GetCurrentWeapon()
    {
        if(weapons == null)
        {
            return null;
        }

        if(weapons.Length == 0)
        {
            return null;
        }

        if(currentWeaponIndex < 0 || currentWeaponIndex >= weapons.Length)
        {
            return null;
        }

        return weapons[currentWeaponIndex];
    }

    /// <summary>
    /// 현재 장착 중인 무기 인덱스를 반환.
    /// </summary>
    /// <returns>현재 장착 중인 무기 인덱스</returns>
    public int GetCurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }
}
