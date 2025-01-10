using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform _posSpawnMin;
    [SerializeField] private Transform _posSpawnMax;
    [SerializeField] private BatutPlatfrom batutPlatfrom;
    [SerializeField] private Artifact artifact;

    public bool IsStartPlatfrom = false;

    public bool isUsed = false;
    private bool isMove = false;

    public bool isActiveArtifact = false;

    [SerializeField] private float _speedMove;
    private Vector3 _targetMove;

    public bool IsUsed
    {
        get
        {
            return isUsed;
        }
        set
        {
            isUsed = value;
        }
    }

    public void RestartSavePlayer()
    {
        isUsed = false;
    }

    public void SetPlatfrom(Vector2 position)
    {
        transform.position = position;
        batutPlatfrom.transform.position = new Vector3(Random.Range(_posSpawnMin.position.x, _posSpawnMax.position.x), _posSpawnMin.position.y, 0);
        batutPlatfrom.ShowPlatfrom();
        isUsed = false;

        if (Random.Range(0, 100) > 50)
        {
            _targetMove = _posSpawnMin.position;
            _targetMove.y = batutPlatfrom.transform.position.y;

            isMove = true;
        }
        else
        {
            isMove = false;
        }

        if (Random.Range(0,100) > 50)
        {
            Vector2 posSpawn = new Vector2(Random.Range(_posSpawnMin.position.x, _posSpawnMax.position.x), _posSpawnMin.position.y + 0.7f);
            artifact.Init(posSpawn,this);

            isActiveArtifact = true;
        }
        else
        {
            artifact.Hide();
            isActiveArtifact = false;
        }
    }

    private void Update()
    {
        if (isMove)
        {
            batutPlatfrom.transform.position = Vector2.MoveTowards(batutPlatfrom.transform.position, _targetMove, Time.deltaTime * _speedMove);

            if (batutPlatfrom.transform.position == _targetMove)
            {
                if (_targetMove == _posSpawnMin.position)
                {
                    _targetMove = _posSpawnMax.position;
                }
                else
                {
                    _targetMove = _posSpawnMin.position;
                }
            }
        }
    }

    public void DeActiveArtifact()
    {
        isActiveArtifact = false;
    }

    public void ReturnToPool()
    {
        PoolBatutObj.Instance.ReturnPlatfromToPool(this);
    }
}
