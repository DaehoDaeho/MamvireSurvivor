using UnityEngine;

/// <summary>
/// 경험치 아이템이 플레이어와 닿았을 때 플레이어의 경험치 시스템에 획득을 요청하고 자기 자신을 제거.
/// </summary>
public class ExperiencePickup : MonoBehaviour
{
    [Header("경험치 설정")]
    [SerializeField] private int experienceAmount = 1;

    [Header("대상 판별 셜정")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 대상이 플레이어인지 체크.
        if(collision.CompareTag(playerTag) == false)
        {
            return;
        }

        PlayerExperience playerExperience = collision.GetComponent<PlayerExperience>();
        if(playerExperience != null)
        {
            playerExperience.AddExperience(experienceAmount);
        }

        Destroy(gameObject);
    }
}
