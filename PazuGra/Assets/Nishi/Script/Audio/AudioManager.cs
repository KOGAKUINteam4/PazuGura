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

    public Dictionary<AudioList, AudioClip> m_AudioSEClips = new Dictionary<AudioList, AudioClip>();
    public Dictionary<AudioList, AudioClip> m_AudioBGMClips = new Dictionary<AudioList, AudioClip>();

    public void Awake()
    {
        int i = 0;
        foreach(AudioClip AC in AudioClips)
        {
            m_AudioSEClips.Add((AudioList)i,AC);
            i++;
        }
    }

    public void BGMPlay(AudioList name)
    {
        m_AudioBGM.clip = m_AudioBGMClips[name];
        m_AudioBGM.Play();
    }

    public void BGMStop()
    {
        m_AudioBGM.Stop();
    }

    public void SEPlay(AudioList name)
    {
        m_AudioSE.PlayOneShot(m_AudioSEClips[name]);
    }

}
