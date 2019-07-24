using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MapPointManager : MonoBehaviour
{
    public GameObject huntUI, exploreUI;
    public TextMeshProUGUI[] huntUITexts, exploreUITexts;
    public Information[] huntInformation, exploreInformation;
    public static bool[] isExplored , requestHunt , requestExplore ;
    public static Image[] mapPoints;
    public static GameObject[] slavesAssignedToPoints;
    public static int numberOfPointClicked = 0,countOfMapPoints;
    public Sprite[] mapPointsExplored;
    public Sprite mapPoints2Explore, lockedMapPoints;
    private static Sprite[] exploredMapPoins;
    private static Sprite mapPointsToExplore, mapPointsLocked;
    private static int[] numOfSlavesToPoints;
    void Start()
    {
        mapPointsToExplore = mapPoints2Explore;
        mapPointsLocked = lockedMapPoints;
        countOfMapPoints = transform.childCount;
        mapPoints = new Image[countOfMapPoints];
        slavesAssignedToPoints = new GameObject[countOfMapPoints];
        for (int i = 0; i < countOfMapPoints; i++)
        {
            exploredMapPoins = mapPointsExplored;
            mapPoints[i] = transform.GetChild(i).GetComponent<Image>();
            slavesAssignedToPoints[i] = mapPoints[i].gameObject.transform.GetChild(0).gameObject;
        }
        if (isExplored == null)
        {
            isExplored = new bool[countOfMapPoints];
            requestHunt = new bool[countOfMapPoints];
            requestExplore = new bool[countOfMapPoints];
            numOfSlavesToPoints = new int[countOfMapPoints];
        }
        displayMapPoints();
    }
    public static void displayMapPoints()
    {
        for (int i = 0; i < GameManager.mapPointsCleared + 1; i++)
        {
            mapPoints[i].sprite = exploredMapPoins[i];
            isExplored[i] = true;
            if (!requestHunt[i])
            {
                mapPoints[i].GetComponent<Button>().interactable = true;
            }else
            {
                mapPoints[i].GetComponent<Button>().interactable = false;
                slavesAssignedToPoints[i].GetComponent<TextMeshProUGUI>().text = numOfSlavesToPoints[i].ToString();
                slavesAssignedToPoints[i].SetActive(true);
            }
        }
        for (int i = GameManager.mapPointsCleared + 1; i < mapPoints.Length; i++)
        {
            mapPoints[i].sprite = mapPointsToExplore;
            isExplored[i] = false;
            if (!requestExplore[i])
            {
                mapPoints[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                mapPoints[i].GetComponent<Button>().interactable = false;
                slavesAssignedToPoints[i].GetComponent<TextMeshProUGUI>().text = numOfSlavesToPoints[i].ToString();
                slavesAssignedToPoints[i].SetActive(true);
            }
            if (i > GameManager.mapPointsCleared + 1)
            {
                mapPoints[i].sprite = mapPointsLocked;
                mapPoints[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void MapPointClicked(int i)
    {
        numberOfPointClicked = i;
        if (isExplored[numberOfPointClicked])
        {
            ShowInformation(StateOfInformation.Hunt, numberOfPointClicked);
        }
        else
        {
            if (numberOfPointClicked == GameManager.mapPointsCleared+1)
            {
                ShowInformation(StateOfInformation.Explore, numberOfPointClicked);
            }
        }
    }
    public void HuntButtonClicked()
    {
        requestHunt[numberOfPointClicked] = CalAndDisactiveButton(huntInformation[numberOfPointClicked].numberOfSlavesNeeded,  mapPoints[numberOfPointClicked].GetComponent<Button>(), huntUI);
    }
    public void ExploreButtonClicked()
    {
        requestExplore[numberOfPointClicked]  = CalAndDisactiveButton(exploreInformation[numberOfPointClicked].numberOfSlavesNeeded,  mapPoints[numberOfPointClicked].GetComponent<Button>(), exploreUI);
    }
    public void CancelButtonClicked(GameObject objectToInvisible)
    {
        numberOfPointClicked = 0;
        objectToInvisible.SetActive(false);
    }
    private void ShowInformation(StateOfInformation infoState, int numOfPointClicked )
    {
        switch (infoState)
        {
            case StateOfInformation.Hunt:
                huntUI.SetActive(true);
                huntUITexts[0].text = huntInformation[numOfPointClicked].title;
                string huntInfo= huntInformation[numOfPointClicked].informationContext.Replace(";","\n");
                huntUITexts[1].text = huntInfo;
                break;
            case StateOfInformation.Explore:
                exploreUI.SetActive(true);
                exploreUITexts[0].text = exploreInformation[numOfPointClicked].title;
                string exploreInfo = exploreInformation[numOfPointClicked].informationContext.Replace(";", "\n");
                exploreUITexts[1].text = exploreInfo;
                break;
        }
    }
    private bool CalAndDisactiveButton(int numberOfSlavesNeeded,Button toDisinteract,GameObject informationToDisactive)
    {
        if (GameManager.numberOfAvailSlaves >= numberOfSlavesNeeded)
        {
            toDisinteract.interactable = false;
            slavesAssignedToPoints[numberOfPointClicked].SetActive(true);
            slavesAssignedToPoints[numberOfPointClicked].GetComponent<TextMeshProUGUI>().text = numberOfSlavesNeeded.ToString();
            numOfSlavesToPoints[numberOfPointClicked] = numberOfSlavesNeeded;
            GameManager.numberOfAvailSlaves -= numberOfSlavesNeeded;
            GameManager.numberOfBusySlaves += numberOfSlavesNeeded;
            GameManager.tmpNumbers[1].text = GameManager.numberOfAvailSlaves.ToString();
            GameManager.tmpNumbers[2].text = GameManager.numberOfBusySlaves.ToString();
            informationToDisactive.SetActive(false);
            numberOfPointClicked = 0;
            return true;
        }
        return false;
    }
}
