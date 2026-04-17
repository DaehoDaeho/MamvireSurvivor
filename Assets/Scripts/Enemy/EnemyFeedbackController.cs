using System.Collections;
using UnityEngine;

/// <summary>
/// 적의 피격 및 사망 시각적 표현을 담당하는 역할. 
/// </summary>
public class EnemyFeedbackController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D[] colliders;

    [SerializeField] private Color hitFlashColor = Color.white;
    [SerializeField] private float baseHitFlashDuration = 0.06f;    // 가장 약한 피격 시 기본 플래시 시간.

    [SerializeField] private float extraHitFlashDuration = 0.08f;   // 피격 강도에 따라 추가될 최대 플래시 시간.

    [SerializeField] private Color deathColor = new Color(0.4f, 0.4f, 0.4f, 0.7f);

    [SerializeField] private float deathRemoveDelay = 0.15f;    // 사망 연출 후 제거까지 대기하는 시간.

    [SerializeField] private bool isHitFlashPlaying = false;    // 현재 피격 플래시 코루틴이 실행 중인지 여부.
    [SerializeField] private bool isDeathSequenceStarted = false;   // 사망 연출이 시작되었는지 여부.

    [SerializeField] private Color originalColor = Color.white;

    [SerializeField] private AudioSource feedbackAudioSource;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private AudioClip hitSound;

    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private AudioClip deathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalColor = spriteRenderer.color;
        
        if(feedbackAudioSource == null)
        {
            feedbackAudioSource = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// 적 피격 시 시각 피드백을 재생.
    /// </summary>
    /// <param name="hitStrength">피격 강도</param>
    public void PlayHitFeedback(float hitStrength)
    {
        if(isDeathSequenceStarted == true)
        {
            return;
        }

        // Mathf.Max : 두 인자 값 중 더 큰 수를 반환하는 함수.
        float clampedStrength = Mathf.Max(0.0f, hitStrength);
        float flashDuration = baseHitFlashDuration + (extraHitFlashDuration * clampedStrength);

        // 코루틴 함수 호출.
        StopCoroutine(nameof(HitFlashRoutine));
        StartCoroutine(HitFlashRoutine(flashDuration));

        PlayHitSound();
        SpawnHitEffect();
    }

    /// <summary>
    /// 적 사망 표현을 재생하고 지연 후 제거.
    /// </summary>
    public void PlayDeathFeedbackAndDestroy()
    {
        if (isDeathSequenceStarted == true)
        {
            return;
        }

        StartCoroutine(DeathRoutine());

        PlayDeathSound();
        SpawnDeathEffect();
    }

    /// <summary>
    /// 피격 플래시를 재생하는 코루틴 함수.
    /// </summary>
    /// <param name="flashDuration">플래시 유지 시간</param>
    /// <returns></returns>
    IEnumerator HitFlashRoutine(float flashDuration)
    {
        isHitFlashPlaying = true;

        spriteRenderer.color = hitFlashColor;

        yield return new WaitForSeconds(flashDuration);

        if(isDeathSequenceStarted == false)
        {
            spriteRenderer.color = originalColor;
        }

        isHitFlashPlaying = false;
    }

    /// <summary>
    /// 사망 연출을 재생하고 적 오브젝트를 제거하는 코루틴 함수.
    /// </summary>
    /// <returns></returns>
    IEnumerator DeathRoutine()
    {
        isDeathSequenceStarted = true;

        // 콜라이더 비활성화.
        DisableAllColliders();

        spriteRenderer.color = deathColor;

        yield return new WaitForSeconds(deathRemoveDelay);

        Destroy(gameObject);
    }

    void DisableAllColliders()
    {
        for(int i=0; i<colliders.Length; ++i)
        {
            colliders[i].enabled = false;
        }
    }

    void PlayHitSound()
    {
        if(feedbackAudioSource == null || hitSound == null)
        {
            return;
        }

        feedbackAudioSource.PlayOneShot(hitSound);
    }

    void SpawnHitEffect()
    {
        if(hitEffectPrefab == null)
        {
            return;
        }

        GameObject hitEffectObject = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        Destroy(hitEffectObject, 0.5f);
    }

    void PlayDeathSound()
    {
        if(feedbackAudioSource == null || deathSound == null)
        {
            return;
        }

        feedbackAudioSource.PlayOneShot(deathSound);
    }

    void SpawnDeathEffect()
    {
        if (deathEffectPrefab == null)
        {
            return;
        }

        GameObject deathEffectObject = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        Destroy(deathEffectObject, 0.5f);
    }
}
