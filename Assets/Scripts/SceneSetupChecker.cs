using UnityEngine;

public class SceneSetupChecker : MonoBehaviour
{
    [SerializeField]
    private string worldRootName = "World";

    [SerializeField]
    private string systemsRootName = "Systems";

    [SerializeField]
    private string playerStartName = "PlayerStart";

    [SerializeField]
    private string uiRootName = "UI";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int totalCheckCount = 5;    // 검사할 항목의 총 개수.
        int successCount = 0;   // 검사를 통과한 항목의 총 개수.

        if(Camera.main != null)
        {
            Debug.Log("[정상] Main Camera가 존재합니다.");
            ++successCount;
        }
        else
        {
            Debug.LogWarning("[누락] Main Camera가 없습니다. 카메라를 확인하세요.");
        }

        // 씬에 배치된 오브젝트 중 인자로 전달된 이름의 오브젝트를 찾아서 반환.
        GameObject worldRootObject = GameObject.Find(worldRootName);
        if(worldRootObject != null)
        {
            Debug.Log("[정상] World 루트가 존재합니다. 이름: " + worldRootObject.name);
            ++successCount;
        }
        else
        {
            Debug.LogWarning("[누락] World 루트 오브젝트가 없습니다. 권장 이름: " + worldRootName);
        }

        GameObject systemsRootObject = GameObject.Find(systemsRootName);
        if (systemsRootObject != null)
        {
            Debug.Log("[정상] Systems 루트가 존재합니다. 이름: " + systemsRootObject.name);
            ++successCount;
        }
        else
        {
            Debug.LogWarning("[누락] Systems 루트 오브젝트가 없습니다. 권장 이름: " + systemsRootName);
        }

        GameObject playerStarttObject = GameObject.Find(playerStartName);
        if (playerStarttObject != null)
        {
            Debug.Log("[정상] PlayerStart 오브젝트가 존재합니다. 이름: " + playerStarttObject.name);
            ++successCount;
        }
        else
        {
            Debug.LogWarning("[누락] PlayerStart 오브젝트가 없습니다. 권장 이름: " + playerStartName);
        }

        GameObject uiRootObject = GameObject.Find(uiRootName);
        if (uiRootObject != null)
        {
            Debug.Log("[정상] UI 루트가 존재합니다. 이름: " + uiRootObject.name);
            ++successCount;
        }
        else
        {
            Debug.LogWarning("[누락] UI 루트 오브젝트가 없습니다. 권장 이름: " + uiRootName);
        }

        if(successCount == totalCheckCount)
        {
            Debug.Log("결과: 기본 씬 구조가 매우 잘 준비되었습니다.");
        }
        else
        {
            Debug.Log("결과: 항목이 누락되었습니다. Console 로그를 참조해서 씬 구조를 보완하세요!!!");
        }
    }
}
