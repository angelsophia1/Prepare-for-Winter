using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class ShopMenu : MonoBehaviour
{
    public TMP_InputField inputText;
    public TextMeshProUGUI itemInfoText,statusText;
    public GameObject infoToDisplay;
    public Button itemBuyButton;
    public ItemInfo[] items;
    public CursorManager cursorManager;
    private int numOfSlavesToBuy,idOfItemClicked;
    private AudioSource audioSource;
    private float audioLength;
    private void Start()
    {
        cursorManager.OnButtonPointerExit();
        idOfItemClicked = 0;
        if (GameManager.itemOwnStatus == null)
        {
            GameManager.itemOwnStatus = new bool[items.Length];
        }
        audioSource = GetComponent<AudioSource>();
        audioLength = audioSource.clip.length;
    }
    public void MapButtonClicked()
    {
        StartCoroutine(PlayAndLoad());
    }
    public void HRBuyButtonClicked()
    {
        numOfSlavesToBuy = int.Parse(inputText.text);
        if (GameManager.numberOfMeat >= numOfSlavesToBuy)
        {
            GameManager.numberOfMeat -= numOfSlavesToBuy;
            GameManager.numberOfAvailSlaves += numOfSlavesToBuy;
            GameManager.tmpNumbers[1].text = GameManager.numberOfAvailSlaves.ToString();
            GameManager.tmpNumbers[4].text = GameManager.numberOfMeat.ToString();
        }
    }
    public void NPCClicked(GameObject NPCUI)
    {
        if (NPCUI.activeSelf)
        {
            NPCUI.SetActive(false);
        }
        else
        {
            NPCUI.SetActive(true);
        }
    }
    public void ItemClicked(int id)
    {
        idOfItemClicked = id;
        foreach (ItemInfo item in items)
        {
            if (item.id == id)
            {
                string textTmp = item.infoText.Replace(";","\n");
                itemInfoText.text = textTmp;
                if (!GameManager.itemOwnStatus[id])
                {
                    statusText.text = "Status: Not owned";
                    itemBuyButton.interactable = true;
                }
                else
                {
                    statusText.text = "Status: Owned";
                    itemBuyButton.interactable = false;
                }
            }
        }
        infoToDisplay.SetActive(true);
    }
    public void ItemBuyButtonClicked()
    {
        statusText.text = "Status: Owned";
        itemBuyButton.interactable = false;
        GameManager.itemOwnStatus[idOfItemClicked] = true;
        switch (idOfItemClicked)
        {
            case 0:
                GameManager.numberOfMeat -= items[idOfItemClicked].numberOfMeatCosts;
                GameManager.tmpNumbers[4].text = GameManager.numberOfMeat.ToString();
                GameManager.maxProbOfDeath  *= 0.5f;
                break;
            case 1:
                GameManager.numberOfMeat -= items[idOfItemClicked].numberOfMeatCosts;
                GameManager.tmpNumbers[4].text = GameManager.numberOfMeat.ToString();
                GameManager.maxProbOfInjure *= 0.5f;
                break;
            case 2:
                GameManager.numberOfMeat -= items[idOfItemClicked].numberOfMeatCosts;
                GameManager.tmpNumbers[4].text = GameManager.numberOfMeat.ToString();
                GameManager.maxProbOfExploreSuccess *= 2;
                break;
        }
    }
    IEnumerator PlayAndLoad()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioLength);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
