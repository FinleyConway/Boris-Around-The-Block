using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource, effectSource, dialogueSource;

    [Header("Pooling 3D Audio Sources")]
    [SerializeField] private Transform SoundPoolLocation;
    private GameObject soundGameObject;
    private Queue<GameObject> soundGameObjectPool = new Queue<GameObject>();
    [SerializeField] private int defaultPoolSoundObjectSize = 10;

    private void Start()
    {
        // Set null object to a empty object.
        soundGameObject = new GameObject("3DSoundTemplate");
        soundGameObject.transform.parent = SoundPoolLocation.transform;

        // Set a default amount of 3D audio sources in the game.
        for (int i = 0; i < defaultPoolSoundObjectSize; i++)
        {
            GameObject sound = Instantiate(soundGameObject);
            sound.transform.parent = SoundPoolLocation.transform;
            sound.AddComponent<AudioSource>();
            soundGameObjectPool.Enqueue(sound);
            sound.SetActive(false);
        }
    }

    // Change overall volume.
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    
    // Change dialogue volume.
    public void ChangeDialogueVolume(float value)
    {
        AudioListener.volume = value;
    }

    // mute/unmute audio sources
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    // mute/unmute audio sources
    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }

    // Play sound.
    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    // Play sound with a position.
    public void PlaySound(AudioClip clip, Vector3 position)
    {
        // Grab pool object from queue.
        GameObject newSound = Get3DSound();
        // Set object to position.
        newSound.transform.position = position;
        // Play sound.
        newSound.TryGetComponent<AudioSource>(out AudioSource soundSource);
        soundSource.spatialBlend = 1;
        soundSource.PlayOneShot(clip);
        // Returns the audio clip back into the pool when the audio clip is done playing.
        StartCoroutine(ReturnSound(soundSource));
    }

    // Get or instantiate 3D sounds from the queue.
    private GameObject Get3DSound()
    {
        if (soundGameObjectPool.Count > 0)
        {
            GameObject sound = soundGameObjectPool.Dequeue();
            sound.SetActive(true);
            return sound;
        }
        else
        {
            GameObject sound = Instantiate(soundGameObject);
            sound.transform.parent = SoundPoolLocation.transform;
            sound.AddComponent<AudioSource>();
            return sound;
        }
    }

    // Return used 3D sounds back into the queue when clip is done.
    private IEnumerator ReturnSound(AudioSource sound)
    {
        yield return new WaitWhile(() => sound.isPlaying);
        soundGameObjectPool.Enqueue(sound.gameObject);
        sound.gameObject.SetActive(false);
    }

    // Play dialouge.
    public void PlayDialogueClip(AudioClip clip)
    {
        dialogueSource.PlayOneShot(clip);
    }

    // Stops dialouge. - probs a temp function since dialogue clips are not actual dialouge with fixed clip length
    public void StopDialougeClip()
    {
        dialogueSource.Stop();
    }
}