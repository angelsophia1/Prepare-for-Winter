using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameObject statusCanvas,gameOver,successCanvas,dailyReport;
    public static int mapPointsCleared = 0, dayNumber = 1,numberOfAvailSlaves = 10,numberOfBusySlaves = 0,numberOfInjuredSlaves = 0,numberOfMeat = 50, numberOfInjured,numberOfDeaths,totalNumOfMeatToConsume,totalNumOfMeatToCol;
    public static int[] numOfMeatToCol;
    public static TextMeshProUGUI[] tmpNumbers,reportTexts = new TextMeshProUGUI[5];
    public static float maxProbOfInjure, maxProbOfDeath,maxProbOfExploreSuccess;
    public static bool[] itemOwnStatus;
    private Information[] huntInfo;
    private static bool checkExploreSuccess;
    private static GameManager instance = null;

    // Game Instance Singleton
    public static GameManager Instance
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

            mapPointsCleared = 0;
            dayNumber = 1;
            numberOfAvailSlaves = 10;
            numberOfBusySlaves = 0;
            numberOfInjuredSlaves = 0;
            numberOfMeat = 50;
            totalNumOfMeatToConsume = 0;
            totalNumOfMeatToCol = 0;

            numberOfInjured = 0;
            numberOfDeaths = 0;

            maxProbOfInjure = 0.4f;
            maxProbOfDeath = 0.2f;
            maxProbOfExploreSuccess = 0.4f;

            checkExploreSuccess = false;
            if (itemOwnStatus!=null)
            {
                for (int i = 0; i<itemOwnStatus.Length;i++)
                {
                    itemOwnStatus[i] = false;
                }
            }
        }

        statusCanvas = FindObjectOfType<Status>().gameObject;
        gameOver = statusCanvas.transform.GetChild(0).gameObject;
        successCanvas = statusCanvas.transform.GetChild(1).gameObject;
        dailyReport = statusCanvas.transform.GetChild(2).gameObject;
        tmpNumbers = new TextMeshProUGUI[statusCanvas.transform.GetChild(3).childCount];
        for (int i = 0; i < reportTexts.Length; i++)
        {
            reportTexts[i] = dailyReport.transform.GetChild(i+2).GetComponent<TextMeshProUGUI>();
        }
        for (int i = 0; i< statusCanvas.transform.GetChild(3).childCount;i++)
        {
            tmpNumbers[i] = statusCanvas.transform.GetChild(3).GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }
    private void Start()
    {
        huntInfo = FindObjectOfType<MapPointManager>().huntInformation;
        numOfMeatToCol = new int[huntInfo.Length];
        for (int i = 0; i < huntInfo.Length; i++)
        {
            numOfMeatToCol[i] = huntInfo[i].numberOfMeatToCollect;
        }
    }
    public static void TurnOver()
    {
        dayNumber++;
        totalNumOfMeatToConsume = numberOfAvailSlaves + numberOfBusySlaves + numberOfInjuredSlaves;
        numberOfMeat -= totalNumOfMeatToConsume;
        numberOfDeaths = Mathf.RoundToInt(numberOfBusySlaves * Random.Range(0f, maxProbOfDeath));
        numberOfInjured = Mathf.RoundToInt((numberOfBusySlaves - numberOfDeaths) * Random.Range(0f, maxProbOfInjure));
        numberOfAvailSlaves = numberOfAvailSlaves + numberOfBusySlaves + numberOfInjuredSlaves -numberOfDeaths-numberOfInjured;
        numberOfInjuredSlaves = numberOfInjured;
        if (numberOfBusySlaves > 0)
        {
            for (int i = 0; i < MapPointManager.countOfMapPoints;i++)
            {
                if (MapPointManager.requestExplore[i])
                {
                    MapPointManager.requestExplore[i] = false;
                    checkExploreSuccess = Random.Range(0f, 1f) > (1-maxProbOfExploreSuccess);
                    if (checkExploreSuccess)
                    {
                        mapPointsCleared += 1;
                    }
                }
                if (MapPointManager.requestHunt[i])
                {
                    MapPointManager.requestHunt[i] = false;
                    totalNumOfMeatToCol += numOfMeatToCol[i];
                }
            }
        }
        numberOfMeat += totalNumOfMeatToCol;
        numberOfBusySlaves = 0;
        tmpNumbers[0].text = dayNumber.ToString();
        tmpNumbers[1].text = numberOfAvailSlaves.ToString();
        tmpNumbers[2].text = numberOfBusySlaves.ToString();
        tmpNumbers[3].text = numberOfInjuredSlaves.ToString();
        tmpNumbers[4].text = numberOfMeat.ToString();

        MapPointManager.displayMapPoints();
        reportTexts[0].text = numberOfInjured + " SLAVES GOT INJURED";
        reportTexts[1].text = numberOfDeaths + " SLAVES DIED";
        reportTexts[2].text = totalNumOfMeatToCol + " MEAT COLLECTED";
        reportTexts[3].text = totalNumOfMeatToConsume + " MEAT CONSUMED";
        if (checkExploreSuccess)
        {
            reportTexts[4].text = "EXPLORE SUCCEEDED";
        }
        else
        {
            reportTexts[4].text = "EXPLORE FAILED OR NO EXPLORE AT ALL";
        }
        checkExploreSuccess = false;
        dailyReport.SetActive(true);
        totalNumOfMeatToCol = 0;
        for (int i = 0; i < MapPointManager.slavesAssignedToPoints.Length; i++)
        {
            if (MapPointManager.slavesAssignedToPoints[i].activeSelf)
            {
                MapPointManager.slavesAssignedToPoints[i].GetComponent<TextMeshProUGUI>().text = "0";
                MapPointManager.slavesAssignedToPoints[i].SetActive(false);
            }
        }
    }    
    public static void CheckGameOver()
    {
        if (dayNumber < 12)
        {
            if (numberOfMeat < 0)
            {
                gameOver.SetActive(true);
            }
        }
        else
        {
            if (numberOfMeat >= 200)
            {
                successCanvas.SetActive(true);
            }
            else
            {
                gameOver.SetActive(true);
            }
        }
    }
}
