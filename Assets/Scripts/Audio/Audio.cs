using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public static class Audio
{
    public enum Mixer
    {
        Master,
        SoundFX,
        Music
    }

    private static AudioSource audioSourcePrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        audioSourcePrefab = Resources.Load<AudioSource>("Audio/AudioSource");
    }

    public static void Play(Sound sound, bool globalSound = false)
    {
        AudioSource audioSource = Object.Instantiate(audioSourcePrefab);
        audioSource.clip = sound.Clip;
        audioSource.volume = sound.Volume;
        audioSource.pitch = sound.Pitch + Random.Range(0f, sound.RandomPitch);
        audioSource.Play();

        if (globalSound)
            SceneManager.MoveGameObjectToScene(audioSource.gameObject, SceneManager.GetSceneByBuildIndex(0));

        Object.Destroy(audioSource.gameObject, sound.Clip.length);
    }
}
