using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    static AudioManager instance = null;

    public AudioClip[] bgmClips;
    private AudioSource audioSource;
    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        audioSource = GetComponent<AudioSource>();
        //switch (level)
        //{
        //    case 0:
        //        if (audioSource.clip != bgmClips[0])
        //        {
        //            audioSource.Stop();
        //            audioSource.clip = bgmClips[0];
        //            audioSource.Play();
        //        }
        //        break;
        //    case 2:
        //        if (audioSource.clip != bgmClips[1])
        //        {
        //            audioSource.Stop();
        //            audioSource.clip = bgmClips[1];
        //            audioSource.Play();
        //        }
        //        break;
        //}
    }
}
