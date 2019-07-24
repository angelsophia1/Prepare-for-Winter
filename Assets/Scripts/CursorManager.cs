using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[CreateAssetMenu(fileName = "New Cursor Manager", menuName = "Cursor Manager")]
public class CursorManager : ScriptableObject
{
    public Texture2D highlightedCursor, normalCursor, searchCursor;
    public static CursorManager instance;
    public void OnButtonPointerEnter()
    {
        Cursor.SetCursor(highlightedCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void OnButtonPointerExit()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void OnMapPointEnter()
    {
        Cursor.SetCursor(searchCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void OnMapPointExit()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
