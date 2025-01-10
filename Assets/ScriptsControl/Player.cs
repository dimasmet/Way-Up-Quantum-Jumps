using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbPlayer;
    [SerializeField] private float _forceJump;
    [SerializeField] private float _speedMax;

    private Vector2 _startPos;

    private Vector3 _directionMoveCurr;
    private float _speedMoveCurr;

    private bool isMovement = false;

    [SerializeField] private GameObject _saveObj;
    public bool isSave = false;
    private Stage _saveStage;
    private Stage _tempStage;

    [SerializeField] private Animator _anim;

    private List<Stage> stagesTempUsed;

    private DataSkin _dataSkin;

    [Header("Sprite Player")]
    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        _startPos = transform.position;

        EventsBusMini.OnStartLevel += StartMove;
        EventsBusMini.OnStopGame += StopGame;
        EventsBusMini.OnResultGame += CheckResult;
        EventsBusMini.OnRestartSave += MoveToSaveSpawn;
        EventsBusMini.OnReloadBonusEcho += ReloadBonus;
    }

    private void OnDisable()
    {
        EventsBusMini.OnStartLevel -= StartMove;
        EventsBusMini.OnStopGame -= StopGame;
        EventsBusMini.OnResultGame -= CheckResult;
        EventsBusMini.OnRestartSave -= MoveToSaveSpawn;
        EventsBusMini.OnReloadBonusEcho -= ReloadBonus;
    }

    public void SetSkinData(DataSkin dataSkin)
    {
        _dataSkin = dataSkin;
        _renderer.sprite = _dataSkin.idleSprite;
    }

    private void StartMove(int i)
    {
        transform.position = _startPos;

        isSave = false;
        _saveObj.gameObject.SetActive(false);

        _saveObj.GetComponent<SpriteRenderer>().sprite = _dataSkin.idleSprite;

        _rbPlayer.isKinematic = false;

        stagesTempUsed = new List<Stage>();
    }

    private void StopGame()
    {
        _rbPlayer.isKinematic = true;
        _rbPlayer.velocity = Vector2.zero;
    }

    public void SaveSpawn()
    {
        _saveObj.transform.position = transform.position;
        _saveObj.gameObject.SetActive(true);

        _saveStage = _tempStage;
        isSave = true;
    }

    public void MoveToSaveSpawn()
    {
        isSave = false;
        transform.position = _saveObj.transform.position;
        _saveObj.gameObject.SetActive(false);
        _anim.Play("Show");

        _rbPlayer.isKinematic = false;

        for (int i = 0; i < stagesTempUsed.Count; i++)
        {
            stagesTempUsed[i].RestartSavePlayer();
        }
    }

    private void ReloadBonus()
    {
        isSave = false;
        _saveObj.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out BatutPlatfrom batutPlatfrom))
        {
            _rbPlayer.velocity = Vector2.zero;
            _rbPlayer.AddForce(Vector2.up * _forceJump);

            HanSoundsGame.OnRunSound?.Invoke(HanSoundsGame.NameSound.Jump);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Stage stage))
        {
            if (stage.IsUsed == false)
            {
                stage.IsUsed = true;

                stagesTempUsed.Add(stage);
                _tempStage = stage;
                EventsBusMini.OnUsedPlatfrom?.Invoke();
                EventsBusMini.OnSetTargetCamera?.Invoke(stage.transform);
            }
        }

        if (collision.TryGetComponent(out PlanetFinish planet))
        {
            EventsBusMini.OnResultGame?.Invoke(EventsBusMini.TypeResult.Win);
            _renderer.sprite = _dataSkin.idleSprite;
        }

        if (collision.TryGetComponent(out CheckLoseGameTrigger clearScene))
        {
            EventsBusMini.OnResultGame?.Invoke(EventsBusMini.TypeResult.Lose);
        }
    }

    public void StartMove()
    {
        isMovement = true;
    }

    public void StopMove()
    {
        isMovement = false;

        SetStatePlayer(StateAnimPlayer.idle);
    }

    public void CheckResult(EventsBusMini.TypeResult typeResult)
    {
        switch (typeResult)
        {
            case EventsBusMini.TypeResult.Win:
                Main.Instance.OpenResult(typeResult);
                break;
            case EventsBusMini.TypeResult.Lose:
                if (isSave == false)
                {
                    _rbPlayer.velocity = Vector2.zero;
                    _rbPlayer.isKinematic = true;
                    SetStatePlayer(StateAnimPlayer.End);
                    Main.Instance.OpenResult(typeResult);
                }
                else
                {
                    _rbPlayer.velocity = Vector2.zero;
                    _rbPlayer.isKinematic = true;
                    SetStatePlayer(StateAnimPlayer.End);
                    _anim.Play("Hide");
                    EventsBusMini.OnSetTargetCamera?.Invoke(_saveStage.transform);
                    EventsBusMini.OnMoveToSavePos?.Invoke(_saveObj.transform.position);
                }
                break;
        }
    }

    public void SetParamsToMove(Vector3 directionMove, float speedCoeff)
    {
        _directionMoveCurr = directionMove;
        _directionMoveCurr.y = 0;
        _speedMoveCurr = _speedMax * speedCoeff;

        if (_directionMoveCurr.x > 0.2f)
        {
            _renderer.transform.rotation = Quaternion.Euler(0, 180, 0);
            SetStatePlayer(StateAnimPlayer.Move);
        }
        else
        {
            if (_directionMoveCurr.x < -0.2f)
            {
                _renderer.transform.rotation = Quaternion.Euler(0, 0, 0);
                SetStatePlayer(StateAnimPlayer.Move);
            }
            else
            {
                SetStatePlayer(StateAnimPlayer.idle);
            }
        }
    }

    public enum StateAnimPlayer
    {
        idle,
        Move,
        End
    }

    public void SetStatePlayer(StateAnimPlayer stateAnim)
    {
        switch (stateAnim)
        {
            case StateAnimPlayer.idle:
                _renderer.sprite = _dataSkin.idleSprite;
                break;
            case StateAnimPlayer.Move:
                _renderer.sprite = _dataSkin.moveSprite;
                break;
            case StateAnimPlayer.End:
                _renderer.sprite = _dataSkin.loseSprite;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (isMovement)
        {
            //Vector2 pos = ;
            //pos.y = transform.position.y;
            //pos.x = Mathf.Clamp(pos.x, -1.85f, 1.85f);

            //_rbPlayer.MovePosition(_rbPlayer.position + _directionMoveCurr * _speedMoveCurr * Time.fixedDeltaTime);
            transform.position = transform.position + _directionMoveCurr * _speedMoveCurr * Time.fixedDeltaTime;

            float oX = transform.position.x;
            //float oY = transform.position.y;

            oX = Mathf.Clamp(oX, -1.85f, 1.85f);
            //oY = Mathf.Clamp(oY, -4f, 4f);

            transform.position = (new Vector2(oX, transform.position.y));
        }
    }

    
}
