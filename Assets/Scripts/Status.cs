using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Status : MonoBehaviour
{
    private static Status instance = null;
    private AudioSource audioSource;
    private float audioLength;
    // Game Instance Singleton
    public static Status Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioLength = audioSource.clip.length;
    }
    public void ContinueToCredits()
    {
        StartCoroutine(PlayAndLoad());
        Destroy(FindObjectOfType<GameManager>().gameObject);
        Destroy(gameObject,audioLength+0.1f); 
    }
    public void ReportReturnButtonClicked()
    {
        GameManager.CheckGameOver();
    }
    IEnumerator PlayAndLoad()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioLength);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
