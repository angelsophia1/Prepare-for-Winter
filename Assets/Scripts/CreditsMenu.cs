using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsMenu : MonoBehaviour
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
    public void ReturnButtonClicked()
    {
        StartCoroutine(PlayAndLoad());
    }
    IEnumerator PlayAndLoad()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioLength);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }
}
