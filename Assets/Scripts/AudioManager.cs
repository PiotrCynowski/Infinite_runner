using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundsAudioSource;

    [SerializeField] private AudioClip sound_ShieldStart;
    [SerializeField] private AudioClip sound_ShieldFinished;
    [SerializeField] private AudioClip sound_SpeedUpStart;
    [SerializeField] private AudioClip sound_SpeedUpFinished;
    [SerializeField] private AudioClip sound_Collision;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void PlaySound(TypeOfAudioClip _clip)
    {
        switch (_clip)
        {
            case TypeOfAudioClip.ShieldStart:
                soundsAudioSource.clip = sound_ShieldStart;
                break;

            case TypeOfAudioClip.ShieldEnd:
                soundsAudioSource.clip = sound_ShieldFinished;
                break;

            case TypeOfAudioClip.SpeedUpStart:
                soundsAudioSource.clip = sound_SpeedUpStart;
                break;

            case TypeOfAudioClip.SpeedUpStop:
                soundsAudioSource.clip = sound_SpeedUpFinished;
                break;

            case TypeOfAudioClip.ObstacleCollision:
                soundsAudioSource.clip = sound_Collision;
                break;

            default:
                break;
        }

        soundsAudioSource.Play();
    }
}

public enum TypeOfAudioClip { ShieldStart, ShieldEnd, SpeedUpStart, SpeedUpStop, ObstacleCollision };
