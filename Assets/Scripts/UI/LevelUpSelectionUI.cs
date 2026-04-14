using UnityEngine;

/// <summary>
/// 레벨업 시 UI를 표시하고, 게임을 일시정지 한 뒤, 플레이어가 선택한 강화 결과를 PlayerExperience에 전달하는 역할.
/// </summary>
public class LevelUpSelectionUI : MonoBehaviour
{
    [Header("참조 설정")]
    [SerializeField] private PlayerStatController playerStatController;

    [Header("UI 상태 확인용")]
    [SerializeField] private bool isOpen = false;

    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void Show()
    {
        isOpen = true;
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Hide()
    {
        isOpen = false;
        Time.timeScale = 1.0f;

        if (gameManager != null)
        {
            gameManager.ResumePlaying();
        }

        gameObject.SetActive(false);
    }

    public void SelectMoveSpeedUpgrade()
    {
        if(playerStatController != null)
        {
            playerStatController.ApplyMoveSpeedUpgrade();
        }

        Hide();
    }

    public void SelectAttackSpeedUpgrade()
    {
        if(playerStatController != null)
        {
            playerStatController.ApplyAttackSpeedUpgrade();
        }

        Hide();
    }

    public void SelectProjectileSpeedUpgrade()
    {
        if(playerStatController != null)
        {
            playerStatController.ApplyProjectileSpeedUpgrade();
        }

        Hide();
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
