using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startSpawnPos;
    private Vector2 _spawnPosVector;

    [SerializeField] private float _deltaOy;

    private int countStepWay;
    private int countMaxStep;

    [SerializeField] private PlanetFinish _planetFinish;
    [SerializeField] private Stage _firstStage;

    private void Start()
    {
        EventsBusMini.OnUsedPlatfrom += SpawnPlatfromStageNext;
        EventsBusMini.OnStartLevel += StartSpawn;
        EventsBusMini.OnStopGame += StopGame;
    }

    private void OnDisable()
    {
        EventsBusMini.OnUsedPlatfrom -= SpawnPlatfromStageNext;
        EventsBusMini.OnStartLevel -= StartSpawn;
        EventsBusMini.OnStopGame -= StopGame;
    }

    public void StopGame()
    {
        PoolBatutObj.Instance.ReturnAllObject();
    }

    private void StartSpawn(int countLenght)
    {
        SetCountLenght(countLenght);
        _planetFinish.gameObject.SetActive(false);
        _spawnPosVector = _startSpawnPos.position;
        _firstStage.IsUsed = false;
    }

    private void SetCountLenght(int count)
    {
        countStepWay = 0;
        countMaxStep = count;
    }

    private void SpawnPlatfromStageNext()
    {
        if (countStepWay < countMaxStep)
        {
            Stage platformStage = PoolBatutObj.Instance.GetPlatfromStage();

            platformStage.SetPlatfrom(_spawnPosVector);

            _spawnPosVector.y += _deltaOy;

            countStepWay++;
        }
        else
        {
            SpawnFinish();
        }
    }

    private void SpawnFinish()
    {
        _spawnPosVector.y += _deltaOy;
        _planetFinish.gameObject.SetActive(true);
        _planetFinish.SetPlanetInit(_spawnPosVector);
    }
}
