using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Information", menuName = "Information")]
public class Information : ScriptableObject
{
    public StateOfInformation infoState;
    public int idOfMapPoints;
    public string title, informationContext;
    public int numberOfSlavesNeeded,numberOfMeatToCollect;
}
public enum StateOfInformation {Hunt,Explore };
