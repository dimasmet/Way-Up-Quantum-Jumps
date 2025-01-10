using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLoseGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Stage stage))
        {
            if (stage.IsStartPlatfrom != true)
            {
                //PoolBatutObj.Instance.ReturnPlatfromToPool(stage);
                if (stage.isActiveArtifact)
                {
                    EventsBusMini.OnResultGame?.Invoke(EventsBusMini.TypeResult.Lose);
                }
            }
        }
    }
}
