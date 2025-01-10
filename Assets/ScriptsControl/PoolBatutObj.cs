using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBatutObj : MonoBehaviour
{
    public static PoolBatutObj Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [SerializeField] private List<Stage> _listBatuts;
    public List<Stage> _allListBatuts = new List<Stage>();

    [SerializeField] private Stage _prefabPlatformStep;

    public Stage GetPlatfromStage()
    {
        if (_listBatuts.Count > 0)
        {
            Stage platfromStep = _listBatuts[0];
            _listBatuts.RemoveAt(0);

            platfromStep.gameObject.SetActive(true);
            return platfromStep;
        }
        else
        {
            Stage platfromStep = Instantiate(_prefabPlatformStep);
            _allListBatuts.Add(platfromStep);
            platfromStep.gameObject.SetActive(true);
            return platfromStep;
        }
    }

    public void ReturnPlatfromToPool(Stage Platform)
    {
        if (_listBatuts.Contains(Platform) == false)
        {
            _listBatuts.Add(Platform);
            Platform.gameObject.SetActive(false);
        }
    }

    public void ReturnAllObject()
    {
        for (int i = 0; i < _allListBatuts.Count; i++)
        {
            ReturnPlatfromToPool(_allListBatuts[i]);
        }
    }
}
