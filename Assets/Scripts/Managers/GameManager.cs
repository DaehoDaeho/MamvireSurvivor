using UnityEngine;

/// <summary>
/// 게임 전체 흐름을 중앙에서 조율하는 역할.
/// </summary>
public class GameManager : MonoBehaviour
{
    // 현재 씬의 다른 클래스에서 접근 가능한 GameManager의 인스턴스.
    public static GameManager instance = null;

    [SerializeField] private GameStateController gameStateController;
    [SerializeField] private SceneFlowController sceneFlowController;
    [SerializeField] private GameResultRecorder gameResultRecorder;

    // 현재 게임 종료 처리가 이미 시작되었는지 여부.
    [SerializeField] private bool isGameEnding = false;

    private void Awake()
    {
        // 인스턴스 초기화.
        instance = this;        
    }

    private void Start()
    {
        if(gameStateController == null)
        {
            gameStateController = GameObject.FindAnyObjectByType<GameStateController>();
        }
    }

    /// <summary>
    /// 플레이어 사망 시 호출되는 종료 처리 함수.
    /// </summary>
    public void HandlePlayerDeath()
    {
        if(isGameEnding == true)
        {
            return;
        }

        isGameEnding = true;

        if(gameStateController != null)
        {
            gameStateController.EnterGameOver();
        }

        if(gameResultRecorder != null)
        {
            gameResultRecorder.RecordCurrentResult();
        }

        if(sceneFlowController != null)
        {
            sceneFlowController.GoToResultScene();
        }
    }

    /// <summary>
    /// 현재 게임 종료 처리 중인지 반환.
    /// </summary>
    /// <returns>게임 종료 처리 중 여부</returns>
    public bool IsGameEnding()
    {
        return isGameEnding;
    }

    public GameStateController GetGameStateController()
    {
        return gameStateController;
    }

    public SceneFlowController GetSceneFlowController()
    {
        return sceneFlowController;
    }

    public GameResultRecorder GetGameResultRecorder()
    {
        return gameResultRecorder;
    }
}
