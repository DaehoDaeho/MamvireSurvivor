using UnityEngine;

/// <summary>
/// 보스가 사망했을 때 GameManager를 통해 게임 클리어를 알리는 역할.
/// </summary>
public class BossClearNotifier : MonoBehaviour
{
    [SerializeField] private bool isNotified = false;

    /// <summary>
    /// 보스 사망 시 호출.
    /// </summary>
    public void NotifyBossDefeated()
    {
        if(isNotified == true)
        {
            return;
        }

        isNotified = true;

        if(GameManager.instance != null)
        {
            // 게임 클리어 통지.
            GameManager.instance.HandleGameClear();
        }
    }
}
