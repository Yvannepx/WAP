using UnityEngine;
using UnityEditor;

[ExecuteInEditMode] // This allows the script to run in the Unity Editor
public class PlayerPrefsRemover : EditorWindow
{
    [MenuItem("Tools/Player Prefs Remover")] // Creates a menu item in Unity Editor under 'Tools'
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll(); // Deletes all PlayerPrefs data
        Debug.Log("Player Prefs Deleted!"); // Logs the action
    }
}
