using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    private static AudioManager sInstance;

    public static AudioManager Instance
    {
        get
        {
            if (sInstance == null)
                sInstance = GameObject.FindObjectOfType<AudioManager>();
            return sInstance;
        }
    }

    [SerializeField]
    private AudioSource m_AudioSE;
    [SerializeField]
    private AudioSource m_AudioBGM;

    public AudioClip[] AudioClips;

    public Dictionary<string, AudioClip> m_AudioSEClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> m_AudioBGMClips = new Dictionary<string, AudioClip>();

    public void Awake()
    {
    }

    public void BGMPlay(string name)
    {
        m_AudioBGM.clip = m_AudioBGMClips[name];
        m_AudioBGM.Play();
    }

    public void BGMStop()
    {
        m_AudioBGM.Stop();
    }

    public void SEPlay(string name)
    {
        m_AudioSE.PlayOneShot(m_AudioSEClips[name]);
    }

}
