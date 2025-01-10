using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    private ShotingPlayer _shotingPlayer;
    [SerializeField] private Rigidbody2D _rbBullet;

    public void InitBullet(ShotingPlayer shotingPlayer)
    {
        _shotingPlayer = shotingPlayer;
    }

    public void ReturnToPool()
    {
        _shotingPlayer.ReturnBullet(this);
        gameObject.SetActive(false);
    }

    public void RunShot(Vector2 position, Vector2 direction, float velocity)
    {
        transform.position = position;
        _rbBullet.velocity = direction * velocity;
    }
}
