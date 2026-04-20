using TMPro;
using UnityEngine;

/// <summary>
/// 현재 장착 중인 무기 정보를 HUD에 표시하는 역할.
/// </summary>
public class WeaponHUDPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text currentWeaponText;
    [SerializeField] private WeaponController weaponController;

    // Update is called once per frame
    void Update()
    {
        RefreshWeaponText();
    }

    void RefreshWeaponText()
    {
        if(weaponController == null || currentWeaponText == null)
        {
            return;
        }

        WeaponData currentWeapon = weaponController.GetCurrentWeapon();

        if(currentWeapon != null)
        {
            currentWeaponText.text = "Weapon : " + currentWeapon.weaponName;
        }
        else
        {
            currentWeaponText.text = "Weapon : None";
        }
    }
}
