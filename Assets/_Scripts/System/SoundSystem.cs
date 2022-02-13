using UnityEngine;

public class SoundSystem : PresistentSingleton<SoundSystem>
{
    [SerializeField] private AudioSource musicSource, effectSource, dialogueSource;

    // Change overall volume.
    public void ChangeMasterVolume(float value)
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
        // insta sound object (object pool)

        // assign sound to object

        // set object to position

        // play sound
    }

    // Play dialouge.
    public void PlayDialogueClip(AudioClip clip)
    {
        dialogueSource.PlayOneShot(clip);
    }

    // Stops dialouge. - probs a temp function since dialogue clips are not actual dialouge  
    public void StopDialougeClip()
    {
        dialogueSource.Stop();
    }
}