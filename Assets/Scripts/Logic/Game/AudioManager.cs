using UnityEngine;


public enum SoundType
{
    CUSTOMER_BELL,
    CASH_INCREASE,
    OPEN_INVENTORY,
    CLOSE_INVENTORY,
    CUSTOMER_REFUSE,
    CUSTOMER,COUNTER_OFFER,
    DAILIY_PANEL,
    CLICK
    
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
