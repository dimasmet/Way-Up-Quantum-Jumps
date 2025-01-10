using UnityEngine;

public class PlanetFinish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spritePlanet;
    [SerializeField] private Sprite[] _planetsSprite;

    public void SetPlanetInit(Vector2 position)
    {
        transform.position = position;
        _spritePlanet.sprite = _planetsSprite[Random.Range(0, _planetsSprite.Length)];
    }
}
