/// <summary>
/// 게임 종료 시 결과 데이터를 임시로 저장하는 static 저장소 역할.
/// </summary>
public static class GameResultData
{
    public static float lastSurviveTime = 0.0f; // 마지막 플레이의 생존 시간.
    public static int lastLevel = 1;    // 마지막 플레이의 최종 레벨.
    public static string lastWeaponName = "None";   // 마지막 플레이의 최종 무기 이름.
}
