using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public CursorManager cursorManager;
    private AudioSource audioSource;
    private float audioLength;
    private void Start()
    {
        cursorManager.OnButtonPointerExit();
        audioSource = GetComponent<AudioSource>();
        audioLength = audioSource.clip.length;
    }
    public void StartButtonClicked()
    {
        StartCoroutine(PlayAndLoad(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
    IEnumerator PlayAndLoad(int sceneIndex)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioLength);
        SceneManager.LoadScene(sceneIndex);
    }
    public void CreditsButtonClicked()
    {
        StartCoroutine(PlayAndLoad(SceneManager.GetActiveScene().buildIndex + 4));
    }
}
