using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSceneObject : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent(out BulletPlayer bulletPlayer))
        {
            bulletPlayer.ReturnToPool();
        }
    }
}
