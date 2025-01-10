using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance;
    private float _speedMove;

    [SerializeField] private float _speedMoveStart;

    private Vector3 _target;

    private bool _move = false;

    private Vector3 sourcePos;
    private Transform _targetTransfrom;

    [SerializeField] private GameObject _clearTrigger;
    private bool isSavePlayerMove = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        sourcePos = transform.position;
        //sourcePos.z = -10;

        EventsBusMini.OnStartLevel += ResetCamera;
        EventsBusMini.OnSetTargetCamera += SetTarget;
        EventsBusMini.OnMoveToSavePos += SetToMovePlayerToSave;
    }

    private void OnDisable()
    {
        EventsBusMini.OnStartLevel -= ResetCamera;
        EventsBusMini.OnSetTargetCamera -= SetTarget;
        EventsBusMini.OnMoveToSavePos -= SetToMovePlayerToSave;
    }

    private void SetToMovePlayerToSave(Vector2 posSave)
    {
        isSavePlayerMove = true;
        _move = true;
    }

    private void SetTarget(Transform target)
    {
        _speedMove = _speedMoveStart;
        _targetTransfrom = target;
        _target = _targetTransfrom.position;

        _target.x = transform.position.x;
        _target.z = transform.position.z;

        _move = true;
    }

    private void ResetCamera(int count)
    {
        _speedMove = _speedMoveStart;
        transform.position = sourcePos;
        isSavePlayerMove = false;
        _move = false;
    }

    private void FixedUpdate()
    {
        if (_move)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, Time.fixedDeltaTime * _speedMove);
            if (transform.position == _target)
            {
                if (isSavePlayerMove)
                {
                    EventsBusMini.OnRestartSave?.Invoke();
                    isSavePlayerMove = false;
                }
            }
        }
    }
}
