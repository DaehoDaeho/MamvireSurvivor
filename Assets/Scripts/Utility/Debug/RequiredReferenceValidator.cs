using UnityEngine;

/// <summary>
/// Inspector에서 연결해야 하는 필수 참조들이 비어 있는지 검사하는 역할.
/// </summary>
public class RequiredReferenceValidator : MonoBehaviour
{
    [Header("검사 대상 설정")]
    // 검사할 필수 참조 목록.
    [SerializeField] private Object[] requredReferences;

    // 각 참조의 이름 목록.
    [SerializeField] private string[] requredReferenceNames;

    [Header("검사 옵션")]
    // 게임 시작 시 자동 검사를 할지 여부.
    [SerializeField] private bool validateOnStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(validateOnStart == true)
        {
            ValidateReferences();
        }
    }

    /// <summary>
    /// 필수 참조 목록을 검사하고 비어 있는 참조를 Console에 경고로 출력.
    /// </summary>
    public void ValidateReferences()
    {
        for(int i=0; i<requredReferences.Length; ++i)
        {
            Object currentReference = requredReferences[i];

            if(currentReference != null)
            {
                continue;
            }

            // 연결이 빠진 참조의 정보를 출력.
            string referenceName = GetReferenceName(i);

            Debug.LogWarning(gameObject.name + " | RequiredReferenceValidator | 필수 참조 누락 | " +
                "index: " + i + ", " + "name: " + referenceName);
        }
    }

    /// <summary>
    /// 지정된 인덱스의 참조 이름을 반환.
    /// </summary>
    /// <param name="index">인덱스</param>
    /// <returns>참조 이름</returns>
    string GetReferenceName(int index)
    {
        if(requredReferenceNames == null)
        {
            return "Unnamed Reference";
        }

        if(index < 0 || index >= requredReferenceNames.Length)
        {
            return "Unnamed Reference";
        }

        string referenceName = requredReferenceNames[index];
        if(string.IsNullOrEmpty(referenceName) == true)
        {
            return "Unnamed Reference";
        }

        return referenceName;
    }
}
