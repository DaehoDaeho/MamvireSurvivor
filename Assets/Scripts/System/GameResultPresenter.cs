using TMPro;
using UnityEngine;

/// <summary>
/// ResultScene에서 GameResultData의 값을 읽어서 결과 UI 텍스트에 표시하는 역할.
/// </summary>
public class GameResultPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text surviveText;
    [SerializeField] private TMP_Text finalLevelText;
    [SerializeField] private TMP_Text finalWeaponText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshResultUI();
    }

    /// <summary>
    /// 저장된 결과 데이터를 읽어 결과 UI를 갱신.
    /// </summary>
    void RefreshResultUI()
    {
        if(surviveText != null)
        {
            int totalSeconds = Mathf.FloorToInt(GameResultData.lastSurviveTime);
            int minutes = totalSeconds / 60;    // 분 계산.
            int seconds = totalSeconds % 60;    // 초 계산.

            // 분과 초를 각각 두자릿수로 출력.
            surviveText.text = "Survival Time : " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        if(finalLevelText != null)
        {
            finalLevelText.text = "Final Level : " + GameResultData.lastLevel;
        }

        if(finalWeaponText != null)
        {
            finalWeaponText.text = "Last Weapon : " + GameResultData.lastWeaponName;
        }
    }
}
