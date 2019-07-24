using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapMenu : MonoBehaviour
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
    public void ShopButtonClicked()
    {
        StartCoroutine(PlayAndLoad());
    }
    public void NextTurnClicked()
    {
        GameManager.TurnOver();
    }
    IEnumerator PlayAndLoad()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioLength);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
