using UnityEngine;
using UnityEditor;

public class PlayerPrefsClear : EditorWindow
{
    [MenuItem("PlayerPrefsPro/Clear")]
    private static void DeleteAllPlayerPrefs() => PlayerPrefs.DeleteAll();
}
