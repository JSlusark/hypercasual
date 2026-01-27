#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

public static class MyTools
{
    // --------------------
    // SCENE / DOMAIN RELOAD
    // --------------------

    [MenuItem("Tools/Reload Scene")]
    public static void ReloadScene()
    {
        // if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        //     return;

        var scene = EditorSceneManager.GetActiveScene();
        EditorSceneManager.OpenScene(scene.path);
    }

    [MenuItem("Tools/Reload Domain")]
    public static void ReloadDomain()
    {
        EditorUtility.RequestScriptReload();
    }

    // --------------------
    // PLAY MODE SETTINGS
    // --------------------

    [MenuItem("Tools/Play Mode/Reload Scene Only")]
    public static void SetReloadSceneOnly()
    {
        EditorSettings.enterPlayModeOptionsEnabled = true;
        EditorSettings.enterPlayModeOptions =
            EnterPlayModeOptions.DisableDomainReload;

    }

    [MenuItem("Tools/Play Mode/Reload Scene and Domain")]
    public static void SetReloadSceneAndDomain()
    {
        EditorSettings.enterPlayModeOptionsEnabled = false;

    }

     // --------------------
    // SAVE DATA CLEARANCE
    // ~/Library/Application Support/DefaultCompany
    // --------------------
    [MenuItem("Tools/Clear Save Data")]
    public static void ClearSave()
    {
        string path = Application.persistentDataPath + "/savegame.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted from: " + path);
        }
        else
        {
            Debug.Log("No save file found at: " + path);
        }
    }
}
#endif
