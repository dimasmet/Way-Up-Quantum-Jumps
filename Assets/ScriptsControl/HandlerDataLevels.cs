using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WrapperLevels
{
    public List<LevelData> levelDatas;
}

[System.Serializable]
public class LevelData
{
    public int number;
    public int countLenght;
    public StateLevel stateLevel;
}

public enum StateLevel
{
    Close,
    Open,
    Success
}

public class HandlerDataLevels : MonoBehaviour
{
    public static HandlerDataLevels Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [SerializeField] private WrapperLevels wrapperLevels;
    [SerializeField] private LevelsView levelsView;

    private void Start()
    {
        string json = PlayerPrefs.GetString("LevelsData");
        if (json != "")
        {
            wrapperLevels = JsonUtility.FromJson<WrapperLevels>(json);
        }

        levelsView.InitLevelsButtons(wrapperLevels);
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("LevelsData", JsonUtility.ToJson(wrapperLevels));
    }

    public LevelData GetDataLevel(int number)
    {
        return (wrapperLevels.levelDatas[number]);
    }

    public void SuccessLevel(int number)
    {
        if (wrapperLevels.levelDatas[number].stateLevel != StateLevel.Success)
        {
            wrapperLevels.levelDatas[number].stateLevel = StateLevel.Success;
            if (number < wrapperLevels.levelDatas.Count - 1)
            {
                wrapperLevels.levelDatas[number + 1].stateLevel = StateLevel.Open;
                SaveData();
            }
        }
    }
}
