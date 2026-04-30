using UnityEngine;
using System.Collections.Generic;

public enum UpgradeOptionType
{
    MoveSpeed,
    AttackSpeed,
    ProjectileSpeed
}

[System.Serializable]
public class OptionData
{
    public string name = "Upgrade";
    public string description = "Upgrade Description";
    public UpgradeOptionType optionType = UpgradeOptionType.MoveSpeed;
    public float upgradeValue = 0.0f;
}

[CreateAssetMenu(fileName = "UpgradeOptionData", menuName = "Game/UpgradeOption")]
public class UpgradeOptionData : ScriptableObject
{
    [SerializeField] List<OptionData> optionDatasList = new List<OptionData>();

    public OptionData GetOptionDataByIndex(int index)
    {
        if(index < 0 || index >= optionDatasList.Count)
        {
            return null;
        }

        return optionDatasList[index];
    }

    public int GetOptionDatasCount()
    {
        return optionDatasList.Count;
    }
}
