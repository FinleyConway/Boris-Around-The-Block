using UnityEngine;

[System.Serializable]
public class SoundClips
{
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public float minDistance;
    public float maxDistance;
    public bool canSoundLoop;
    [HideInInspector]
    public AudioSource source;
}

public class SoundSystem : PresistentSingleton<SoundSystem>
{
    [Range(0, 1f)] public float dialogueVolume;
    public SoundClips[] soundClipArray;
    private GameObject oneShotGameObject;
    private AudioSource oneShotAudioSource;

    // plays 2d sounds
    public void PlaySound(string sound)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject(sound);
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        foreach (SoundClips soundClip in soundClipArray)
        {
            oneShotAudioSource.volume = soundClip.volume;
            oneShotAudioSource.pitch = soundClip.pitch;
            oneShotAudioSource.loop = soundClip.canSoundLoop;

        }
        oneShotAudioSource.clip = GetAudioClip(sound);
        oneShotAudioSource.Play();
    }

    // play a dialogue audio clip
    public void PlayDialogueSound(AudioClip sound)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject(sound.ToString());
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.volume = dialogueVolume;
        oneShotAudioSource.clip = sound;
        oneShotAudioSource.Play();
    }

    // plays 3d sounds with modifications
    public void PlaySound(string sound, Vector3 position)
    {
        GameObject soundGameObject = new GameObject(sound);
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        foreach (SoundClips soundClip in soundClipArray)
        {
            audioSource.spatialBlend = 1;
            audioSource.volume = soundClip.volume;
            audioSource.pitch = soundClip.pitch;
            audioSource.minDistance = soundClip.minDistance;
            audioSource.maxDistance = soundClip.maxDistance;

        }
        audioSource.Play();
        Destroy(soundGameObject, audioSource.clip.length);
    }

    // finds the audio clip in the array
    private AudioClip GetAudioClip(string sound)
    {
        foreach (SoundClips soundClip in soundClipArray)
        {
            if (soundClip.name == sound)
            {
                return soundClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " was not found :C");
        return null;
    }
}