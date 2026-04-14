using UnityEngine;

public enum GameState
{
    Playing,
    LevelUpSelection,
    GameOver
}

/// <summary>
/// 게임 전체 상태를 중아에서 관리하는 역할.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState currentState = GameState.Playing;

    [SerializeField] private LevelUpSelectionUI levelUpSelectionUI;

    [SerializeField] private GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeState(GameState.Playing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 게임 상태를 변경.
    /// </summary>
    /// <param name="newState">새로 변경할 상태</param>
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        ApplyState();
    }

    /// <summary>
    /// 현재 상태에 맞는 후속 처리를 적용.
    /// </summary>
    void ApplyState()
    {
        if(currentState == GameState.Playing)
        {
            Time.timeScale = 1.0f;
            if(gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }

            if(levelUpSelectionUI != null && levelUpSelectionUI.gameObject.activeSelf == true)
            {
                levelUpSelectionUI.gameObject.SetActive(false);
            }

            return;
        }

        if(currentState == GameState.LevelUpSelection)
        {
            Time.timeScale = 0.0f;

            if(levelUpSelectionUI != null)
            {
                //levelUpSelectionUI.gameObject.SetActive(true);
                levelUpSelectionUI.Show();
            }

            return;
        }

        if(currentState == GameState.GameOver)
        {
            Time.timeScale = 0.0f;

            if(gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }

    public GameState GetCurrentState()
    {
        return currentState;
    }

    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
    }

    public void EnterLevelUpSelection()
    {
        ChangeState(GameState.LevelUpSelection);
    }

    public void EnterGameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void ResumePlaying()
    {
        ChangeState(GameState.Playing);
    }
}
