using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적이 플레이어와 Trigger로 접촉했을 때 플레이어에게 데미지를 요청하는 역할을 한다.
/// </summary>
public class EnemyContactDamage : MonoBehaviour
{
    [Header("접촉 데미지 설정")]
    [SerializeField] private int contactDamage = 1; // 데미지 양.

    [Header("대상 판별 설정")]
    [SerializeField] private string playerTag = "Player";   // 접촉 대상이 플레이어인지 확인할 때 사용할 태그 이름.

    /// <summary>
    /// Trigger 안에 다른 Collider2D가 머물러 있는 동안 호출되는 Unity 이벤트 함수.
    /// </summary>
    /// <param name="collision">현재 Trigger 안에 들어와 있는 다른 Collider2D</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag) == false)
        {
            return;
        }

        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
        }
        else
        {
            Debug.LogWarning("Player 태그는 있지만, PlayerHealth 컴포넌트가 없습니다.");
        }
    }
}
