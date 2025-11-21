using UnityEngine;


public enum SoundType
{
    CustomerBell,
    CashIncrease,
    OpenInventory,
    CloseInventory,
    CustomerRefuse,
    CounterOffer,
    DailyPanel,
    Click
}


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundClips;
    private static AudioManager Instance;
    private AudioSource _sfxAudioSource;
    
    private void Awake()
    {

        _sfxAudioSource = GetComponent<AudioSource>();
        

        if (Instance == null)
        {
            Instance = this;
           
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlayOneShot(SoundType _clip, float _volume = 0.75f)
    {
        Instance._sfxAudioSource.PlayOneShot(Instance._soundClips[(int)_clip],_volume);
    }
}
