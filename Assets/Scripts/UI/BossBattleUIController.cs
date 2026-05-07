using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 보스전 UI를 관리하는 역할.
/// 보스 이름, 보스 체력바, 보스 등장 문구를 표시.
/// </summary>
public class BossBattleUIController : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private TMP_Text bossNameText;
    [SerializeField] private Image bossHealthBar;
    [SerializeField] private TMP_Text announcementText;

    [SerializeField] private float announcementDuration = 2.0f;

    [SerializeField] private EnemyHealth currentBossHealth;

    private Coroutine announcementCoroutine = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(rootPanel != null)
        {
            rootPanel.SetActive(false);
        }

        if(announcementText != null)
        {
            announcementText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RefreshBossHealth();
    }

    /// <summary>
    /// 보스 UI를 표시하고 체력 정보를 연결.
    /// </summary>
    /// <param name="bossHealth">보스의 EnemyHealth</param>
    /// <param name="bossName">보스 이름</param>
    public void ShowBoss(EnemyHealth bossHealth, string bossName)
    {
        currentBossHealth = bossHealth;

        if(rootPanel != null)
        {
            rootPanel.SetActive(true);
        }

        if(bossNameText != null)
        {
            bossNameText.text = bossName;
        }

        // 보스 체력바 갱신.
        RefreshBossHealth();

        // 보스 등장 문구 출력.
        ShowAnnouncement("BOSS APPEARED!!!");
    }

    /// <summary>
    /// 보스의 현재 체력을 UI에 반영.
    /// </summary>
    void RefreshBossHealth()
    {
        if(bossHealthBar == null)
        {
            return;
        }

        if(currentBossHealth == null)
        {
            return;
        }

        int currentHealth = currentBossHealth.GetCurrentHealth();
        int maximumHealth = currentBossHealth.GetMaximumHealth();

        float percent = (float)currentHealth / (float)maximumHealth;

        bossHealthBar.fillAmount = percent;
    }

    /// <summary>
    /// 화면에 짧은 안내 문구를 표시.
    /// </summary>
    /// <param name="message">표시할 문구</param>
    void ShowAnnouncement(string message)
    {
        if(announcementText == null)
        {
            return;
        }

        if(announcementCoroutine != null)
        {
            StopCoroutine(announcementCoroutine);
        }

        announcementCoroutine = StartCoroutine(ShowAnnouncementRoutine(message));
    }

    IEnumerator ShowAnnouncementRoutine(string message)
    {
        announcementText.text = message;
        announcementText.gameObject.SetActive(true);

        yield return new WaitForSeconds(announcementDuration);

        announcementText.gameObject.SetActive(false);

        announcementCoroutine = null;
    }
}
