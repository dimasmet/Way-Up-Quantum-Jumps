using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public enum StateArtifact
    {
        Active,
        DeActive
    }

    public SpriteRenderer _rendererArtifact;
    public Sprite[] _skinArtifacts;

    public StateArtifact stateArtifact;
    public Animator _animBoom;
    private Stage _stage;

    public void Init(Vector2 position, Stage stage)
    {
        _stage = stage;
        transform.position = position;
        stateArtifact = StateArtifact.Active;
        gameObject.SetActive(true);

        _rendererArtifact.sprite = _skinArtifacts[Random.Range(0, _skinArtifacts.Length)];

        _animBoom.Play("idle");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stateArtifact == StateArtifact.Active)
        {
            if (collision.TryGetComponent(out BulletPlayer bulletPlayer))
            {
                bulletPlayer.ReturnToPool();
                stateArtifact = StateArtifact.DeActive;
                BoomArtifact();

                _stage.DeActiveArtifact();

                CoinBalance.Instance.AddCoinInGame(15);
            }
        }
    }

    private void BoomArtifact()
    {
        _animBoom.Play("Active");
        HanSoundsGame.OnRunSound?.Invoke(HanSoundsGame.NameSound.Boom);
        Invoke(nameof(Hide), 0.5f);
    }
}
