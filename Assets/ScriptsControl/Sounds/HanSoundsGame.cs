using System;
using UnityEngine;

public class HanSoundsGame : MonoBehaviour
{
    public static Action<NameSound> OnRunSound;

    public AudioSource audioSourceBck;
    public AudioSource audioSourceSounds;

    public enum NameSound
    {
        Jump,
        Boom,
        CoinUp,
        Win,
        Lose,
        Shot
    }

    public AudioClip Jump;
    public AudioClip Boom;
    public AudioClip CoinUp;
    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip Shot;

    private bool isGlobal = true;

    private void Start()
    {
        OnRunSound += RunSound;
    }

    private void OnDisable()
    {
        OnRunSound -= RunSound;
    }

    public bool ChangeSoundsGlobal()
    {
        isGlobal = !isGlobal;

        if (isGlobal) audioSourceBck.mute = false;
        else
        audioSourceBck.mute = true;

        return isGlobal;
    }

    public void RunSound(NameSound nameSound)
    {
        if (isGlobal)
        {
            switch (nameSound)
            {
                case NameSound.Jump:
                    audioSourceSounds.PlayOneShot(Jump);
                    break;
                case NameSound.Boom:
                    audioSourceSounds.PlayOneShot(Boom);
                    break;
                case NameSound.CoinUp:
                    audioSourceSounds.PlayOneShot(CoinUp);
                    break;
                case NameSound.Win:
                    audioSourceSounds.PlayOneShot(Win);
                    break;
                case NameSound.Lose:
                    audioSourceSounds.PlayOneShot(Lose);
                    break;
                case NameSound.Shot:
                    audioSourceSounds.PlayOneShot(Shot);
                    break;
            }
        }
    }
}
