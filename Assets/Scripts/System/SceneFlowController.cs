using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowController : MonoBehaviour
{
    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string resultSceneName = "ResultScene";

    /// <summary>
    /// 타이틀에서 게임 씬으로 이동하는 함수.
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(gameSceneName);
    }

    /// <summary>
    /// 게임 종료 후 결과 씬으로 이동하는 함수.
    /// </summary>
    public void GoToResultScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(resultSceneName);
    }

    /// <summary>
    /// 결과 씬 또는 다른 씬에서 타이틀 씬으로 이동하는 함수.
    /// </summary>
    public void GoToTitleScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(titleSceneName);
    }

    /// <summary>
    /// 현재 게임 씬을 다시 시작하는 함수.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(gameSceneName);
    }
}
