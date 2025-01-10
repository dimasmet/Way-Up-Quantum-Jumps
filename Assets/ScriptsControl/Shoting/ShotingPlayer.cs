using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingPlayer : MonoBehaviour
{
    [SerializeField] private List<BulletPlayer> _tempBullets;
    [SerializeField] private BulletPlayer _prefabBullet;

    [SerializeField] protected float _deltaTimeShot;
    [SerializeField] protected Transform _posSpawnBullet;
    [SerializeField] protected float _speedBullet;

    private List<BulletPlayer> _tempListBullet = new List<BulletPlayer>();

    private Transform _containerToBullet;

    private Vector2 _directionMove = Vector2.up;

    public BulletPlayer GetBullet()
    {
        BulletPlayer bullet;

        if (_tempBullets.Count > 0)
        {
            bullet = _tempBullets[0];
            _tempBullets.RemoveAt(0);
        }
        else
        {
            if (_containerToBullet == null)
            {
                GameObject cont = new GameObject("Bullets");
                _containerToBullet = cont.transform;
            }
            bullet = Instantiate(_prefabBullet, _containerToBullet);
            bullet.InitBullet(this);

            _tempListBullet.Add(bullet);
        }

        bullet.gameObject.SetActive(true);

        return bullet;
    }

    public void ReturnBullet(BulletPlayer bullet)
    {
        if (_tempBullets.Contains(bullet) == false)
        {
            _tempBullets.Add(bullet);
        }
    }

    public void HideAllBullet()
    {
        for (int i = 0; i < _tempListBullet.Count; i++)
        {
            _tempListBullet[i].ReturnToPool();
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _directionMove = direction;
    }

    public void StartShoting()
    {
        StartCoroutine(WaitToShot());
    }

    public void StopShoting()
    {
        StopAllCoroutines();
    }

    private IEnumerator WaitToShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            BulletPlayer bullet = GetBullet();
            bullet.RunShot(_posSpawnBullet.position, _directionMove, _speedBullet);
            HanSoundsGame.OnRunSound?.Invoke(HanSoundsGame.NameSound.Shot);
            yield return new WaitForSeconds(_deltaTimeShot);
        }
    }
}
