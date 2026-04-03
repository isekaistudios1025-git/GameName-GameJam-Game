//----- AudioManager.cs START-----


using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private AudioMixerGroup ambianceGroup;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private AudioSource ambianceSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetupSources();
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    private void SetupSources()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        ambianceSource = gameObject.AddComponent<AudioSource>();

        musicSource.outputAudioMixerGroup = musicGroup;
        sfxSource.outputAudioMixerGroup = sfxGroup;
        ambianceSource.outputAudioMixerGroup = ambianceGroup;

        musicSource.loop = true;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }
}


//----- AudioManager.cs END-----