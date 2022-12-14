using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //This is the single audio source within audiomanager game object that will handle
    //theme music.
    public AudioSource themeMusic;
    public AudioClip[] soundClip;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        //Will not destroy Audiomanager when changing scene.
        //DontDestroyOnLoad(gameObject);
    }
#region Old
//public Sound[] music;
//public Sound[] sound;
//public static AudioManager instance;


//private void Awake()
//{
//    if (instance == null)
//        instance = this;
//    else
//    {
//        Destroy(gameObject);
//        return;
//    }
//    //Will not destroy Audiomanager when changing scene.
//    DontDestroyOnLoad(gameObject);

//    foreach (Sound m in music)
//    {
//        m.source = gameObject.AddComponent<AudioSource>();
//        m.source.clip = m.clip;
//        m.source.volume = m.volume;
//        m.source.pitch = m.pitch;
//        m.source.loop = m.loop;
//    }

//    foreach (Sound s in sound)
//    {
//        s.source = gameObject.AddComponent<AudioSource>();
//        s.source.clip = s.clip;
//        s.source.volume = s.volume;
//        s.source.pitch = s.pitch;
//        s.source.loop = s.loop;
//    }
//}

//public void PlayMusic(string name)
//{
//    Sound m = Array.Find(music, sound => sound.name == name);
//    if (m == null)
//    {
//        Debug.LogWarning("Sound " + name + "was not found.");
//        return;
//    }
//    m.source.Play();
//}
#endregion


#region NEWCODE
#region Public Fields

// public static AudioManager Instance;
public static AudioManager Instance;

    public AudioMixerGroup masterGroup;
    public AudioMixer masterMixer;
    public AudioMixerGroup musicGroup;

    public AudioMixerGroup soundGroup;

    public int lowestDeciblesBeforeMute = -20;

    #endregion Public Fields

    #region Public Enums

    public enum AudioChannel { Master, Sound, Music }

    #endregion Public Enums

    #region Public Methods

    /// <summary>
    /// Plays a sound at the given point in space by creating an empty game object with an
    /// AudioSource in that place and destroys it after it finished playing.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="emitter"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource CreatePlaySource(AudioClip clip, Transform emitter, float volume, float pitch, bool music = false)
    {
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = emitter.position;
        go.transform.parent = emitter;
        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;

        // Output sound through the sound group or music group
        if (music)
            source.outputAudioMixerGroup = musicGroup;
        else
            source.outputAudioMixerGroup = soundGroup;

        source.Play();
        return source;
    }

    public AudioSource Play(AudioClip clip, Transform emitter)
    {
        return Play(clip, emitter, 1f, 1f);
    }

    public AudioSource Play(AudioClip clip, Transform emitter, float volume)
    {
        return Play(clip, emitter, volume, 1f);
    }

    /// <summary>
    /// Plays a sound by creating an empty game object with an AudioSource and attaching it to
    /// the given transform (so it moves with the transform). Destroys it after it finished playing.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="emitter"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
    {
        //Create an empty game object
        AudioSource source = CreatePlaySource(clip, emitter, volume, pitch);
        Destroy(source.gameObject, clip.length);
        return source;
    }

    public AudioSource Play(AudioClip clip, Vector3 point)
    {
        return Play(clip, point, 1f, 1f);
    }

    public AudioSource Play(AudioClip clip, Vector3 point, float volume)
    {
        return Play(clip, point, volume, 1f);
    }

    /// <summary>
    /// Plays a sound at the given point in space by creating an empty game object with an
    /// AudioSource in that place and destroys it after it finished playing.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="point"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
    {
        AudioSource source = CreatePlaySource(clip, point, volume, pitch);
        Destroy(source.gameObject, clip.length);
        return source;
    }

    /// <summary>
    /// Plays the sound effect in a loop. Should destroy the audio source in your script when it
    /// is ready to end.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="point"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource PlayLoop(AudioClip clip, Transform emitter, float volume = 1f, float pitch = 1f, bool music = true)
    {
        AudioSource source = CreatePlaySource(clip, emitter, volume, pitch, true);
        source.loop = true;
        return source;
    }

    /// <summary>
    /// Plays the sound effect in a loop. Should destroy the audio source in your script when it
    /// is ready to end.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="point"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource PlayLoop(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, bool music = true)
    {
        AudioSource source = CreatePlaySource(clip, point, volume, pitch, true);
        source.loop = true;
        return source;
    }

    /// <summary>
    /// Adjusts the volume on the audio channel in the unity audio mixer
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="volume">From 0 (mute) to 100 (full volume - 0 DB)</param>
    public void SetVolume(AudioChannel channel, int volume)
    {
        // Converts the 0 - 100 input into decibles | volume of 0 will mute, 1 should be ~the lowestDecibles set,
        // and 100 should be 0 DB offset from the base volume on the channel
        float adjustedVolume = lowestDeciblesBeforeMute + (-lowestDeciblesBeforeMute / 5 * volume / 20);
        // Effectively completed muted if volume if 0
        if (volume == 0)
        {
            adjustedVolume = -100;
        }
        PlayerPrefs.SetFloat("AdjustedVolume", adjustedVolume);
        switch (channel)
        {
            case AudioChannel.Master:
                masterMixer.SetFloat("MasterVolume", adjustedVolume);
                break;

            case AudioChannel.Sound:
                masterMixer.SetFloat("SoundVolume", adjustedVolume);
                break;

            case AudioChannel.Music:
                masterMixer.SetFloat("MusicVolume", adjustedVolume);
                break;
        }

    }

    #endregion Public Methods

    #region Private Methods

    private AudioSource CreatePlaySource(AudioClip clip, Vector3 point, float volume, float pitch, bool music = false)
    {
        //Create an empty game object
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = point;

        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;

        // Output sound through the sound group or music group
        if (music)
            source.outputAudioMixerGroup = musicGroup;
        else
            source.outputAudioMixerGroup = soundGroup;

        source.Play();
        return source;
    }

    /// <summary>
    /// Set up audio levels
    /// </summary>
    private void Start()
    {
        //This will set the audio source handling the music theme to correctly adjust to
        //audio of game.
        themeMusic.outputAudioMixerGroup = musicGroup;

        // Set the audio levels from player preferences
        int masterVolume = (int)(PlayerPrefs.GetFloat("SavedMasterVolume") * 100);
        int soundVolume = (int)(PlayerPrefs.GetFloat("SavedSoundVolume") * 100);
        int musicVolume = (int)(PlayerPrefs.GetFloat("SavedMusicVolume") * 100);

        // Update the audio mixer
        SetVolume(AudioChannel.Master, masterVolume);
        SetVolume(AudioChannel.Sound, soundVolume);
        SetVolume(AudioChannel.Music, musicVolume);
    }

    #endregion Private Methods

    #endregion NEWCODE



    #region Added Code
    public void ChangeTrack(AudioClip audio)
    {
        //Checks to make sure audio doesn't restart upon trigger.
        if (themeMusic.clip.name == audio.name)
        {
            return;
        }

        themeMusic.Stop();
        themeMusic.clip = audio;
        themeMusic.Play();
    }

    public void PlaySound(string soundName)
    {
        AudioClip m = Array.Find(soundClip, AudioClip => AudioClip.name == soundName);
        Debug.Log(m);
        if (m == null)
        {
            Debug.LogWarning("Sound " + soundName + "was not found.");
            return;
        }
        Play(m, transform);
    }
    #endregion Added Code
}