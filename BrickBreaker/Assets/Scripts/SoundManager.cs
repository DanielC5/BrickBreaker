using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    public AudioClip bounceSFX;
    public AudioClip brickBounceSFX;
    public AudioClip breakSFX;
    public AudioClip buttonSFX;

    public AudioClip deathSFX;
    private AudioSource audioPlayer;
    public AudioClip menuMusic;
    public  AudioClip music;



    void Awake()
    {
        //makes sure that there's only one sound manager
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBounce()
    {
        audioPlayer.PlayOneShot(bounceSFX);

    }

    public void PlayBrickBounce()
    {
        audioPlayer.PlayOneShot(brickBounceSFX);

    }

    public void PlayBreak()
    {
        audioPlayer.PlayOneShot(breakSFX);

    }

    public void PlayButton()
    {
        audioPlayer.PlayOneShot(buttonSFX);

    }

    public void PlayDeath()
    {
        audioPlayer.PlayOneShot(deathSFX);

    }

    public void PlayMusic()
    {
        audioPlayer.clip = music; 
        audioPlayer.Play();
    }

    public void StopMusic()
    {
        audioPlayer.Stop();
    }

    public void PlayMenuMusic()
    {
        audioPlayer.clip = menuMusic; 
        audioPlayer.Play();
    }

    public void StopMenuMusic()
    {
        audioPlayer.Stop();
    }

}

