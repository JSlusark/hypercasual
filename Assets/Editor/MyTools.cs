#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

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
}
#endif
