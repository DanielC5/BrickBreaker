using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    //all the audio clips lol
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
        //wall and paddle bounce game 
        audioPlayer.PlayOneShot(bounceSFX);

    }

    public void PlayBrickBounce()
    {
        //brick bounce noise
        audioPlayer.PlayOneShot(brickBounceSFX);

    }

    public void PlayBreak()
    {
        //block break noise
        audioPlayer.PlayOneShot(breakSFX);

    }

    public void PlayButton()
    {
        //button noise lol
        audioPlayer.PlayOneShot(buttonSFX);

    }

    public void PlayDeath()
    {
        //plays death sound effects
        audioPlayer.PlayOneShot(deathSFX);

    }

    public void PlayMusic()
    {
        //plays game music
        audioPlayer.clip = music; 
        audioPlayer.Play();
    }

    public void StopMusic()
    {
        //stops the music
        audioPlayer.Stop();
    }

    public void PlayMenuMusic()
    {
        //plays the menu music
        audioPlayer.clip = menuMusic; 
        audioPlayer.Play();
    }

    public void StopMenuMusic()
    {
        //stops the menu music
        audioPlayer.Stop();
    }

}

