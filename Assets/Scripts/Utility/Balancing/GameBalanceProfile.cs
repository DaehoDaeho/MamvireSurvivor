using UnityEngine;

/// <summary>
/// 게임 전체 밸런스 조정에 사용할 배율 데이터를 저장하는 ScriptableObject
/// 유니티에서 제공하는 데이터 세팅용 스크립트.
/// </summary>
[CreateAssetMenu(fileName = "GameBalanceProifile", menuName = "Game/BalanceProfile")]
public class GameBalanceProfile : ScriptableObject
{
    // 적 생성 간격에 곱할 배율.
    public float enemySpawnIntervalMultiplier = 1.0f;
    // 한번에 생성되는 적 수에 곱할 배율.
    public float enemySpawnCountMultiplier = 1.0f;
    // 적 체력에 곱할 배율.
    public float enemyHealthMultiplier = 1.0f;

    // 무기 데미지에 곱할 배율.
    public float weaponDamageMultiplier = 1.0f;
    // 무기 공격 주기에 곱할 배율.
    public float weaponAttackIntervalMultiplier = 1.0f;
    // 투사체 속도에 곱할 배율.
    public float projectileSpeedMultiplier = 1.0f;

    // 레벨업 필요 경험치에 곱할 배율.
    public float requiredExperienceMultiplier = 1.0f;

    // 이 밸런스 프로파일의 의도를 적어두는 메모.
    public string balanceMemo = "일반 밸런스 프로파일";
}
