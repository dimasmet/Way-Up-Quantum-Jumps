using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _numberLevelText;
    [SerializeField] private GameObject _successObj;

    private LevelData levelData;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            Main.Instance.StartLevel(levelData.number);
        });
    }

    public void InitLevelButton(LevelData levelData)
    {
        this.levelData = levelData;

        _numberLevelText.text = (levelData.number + 1).ToString();
        UpdateStateViewLevel();
    }

    public void UpdateStateViewLevel()
    {
        switch (levelData.stateLevel)
        {
            case StateLevel.Close:
                _button.interactable = false;
                _successObj.SetActive(false);
                break;
            case StateLevel.Open:
                _button.interactable = true;
                _successObj.SetActive(false);
                break;
            case StateLevel.Success:
                _button.interactable = true;
                _successObj.SetActive(true);
                break;
        }
    }
}
