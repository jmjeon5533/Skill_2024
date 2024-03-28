using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundState
    {
        BGM,
        SFX
    }
    public static SoundManager Instance;

    [SerializeField] AudioSource audioObj;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetAudio(AudioClip clip, SoundState state)
    {
        var sound = Instantiate(audioObj);
        sound.clip = clip;
        if (state == SoundState.BGM)
        {
            sound.loop = true;
        }
        else
        {
            sound.loop = false;
            Destroy(sound.gameObject, clip.length);
        }
        sound.Play();
    }
}
