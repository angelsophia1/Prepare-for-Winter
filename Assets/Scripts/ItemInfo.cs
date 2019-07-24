using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemInfo : ScriptableObject
{
    public int id;
    public string infoText;
    public int numberOfMeatCosts;
}
