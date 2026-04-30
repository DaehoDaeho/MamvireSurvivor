using UnityEngine;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// 레벨업 시 UI를 표시하고, 게임을 일시정지 한 뒤, 플레이어가 선택한 강화 결과를 PlayerExperience에 전달하는 역할.
/// </summary>
public class LevelUpSelectionUI : MonoBehaviour
{
    [Header("참조 설정")]
    [SerializeField] private PlayerStatController playerStatController;

    [Header("UI 상태 확인용")]
    [SerializeField] private bool isOpen = false;

    [SerializeField] private GameStateController gameStateController;

    [SerializeField] private TMP_Text[] descriptionTexts;
    [SerializeField] private UpgradeOptionData upgradeOptionData;

    List<OptionData> optionDatasList = new List<OptionData>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void Show()
    {
        isOpen = true;

        optionDatasList.Clear();

        int optionDatasCount = upgradeOptionData.GetOptionDatasCount();

        for(int i=0; i<optionDatasCount; ++i)
        {
            OptionData optionData = upgradeOptionData.GetOptionDataByIndex(i);
            descriptionTexts[i].text = optionData.description;
            optionDatasList.Add(optionData);
        }

        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Hide()
    {
        isOpen = false;
        Time.timeScale = 1.0f;

        if (gameStateController != null)
        {
            gameStateController.ResumePlaying();
        }

        gameObject.SetActive(false);
    }

    public void OnClickUpgrade(int index)
    {
        switch(optionDatasList[index].optionType)
        {
            case UpgradeOptionType.MoveSpeed:
                {
                    if (playerStatController != null)
                    {
                        playerStatController.ApplyMoveSpeedUpgrade(optionDatasList[index].upgradeValue);
                    }
                }
                break;

            case UpgradeOptionType.AttackSpeed:
                {
                    if (playerStatController != null)
                    {
                        playerStatController.ApplyAttackSpeedUpgrade(optionDatasList[index].upgradeValue);
                    }
                }
                break;

            case UpgradeOptionType.ProjectileSpeed:
                {
                    if (playerStatController != null)
                    {
                        playerStatController.ApplyProjectileSpeedUpgrade(optionDatasList[index].upgradeValue);
                    }
                }
                break;
        }

        Hide();
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
