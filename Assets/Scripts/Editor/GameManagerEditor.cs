using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OldGameManager))]
public class GameManagerEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     // 1. Draw the standard Unity stuff first
    //     DrawDefaultInspector();
    //
    //     OldGameManager gm = (OldGameManager)target;
    //
    //     EditorGUILayout.Space(10);
    //     EditorGUILayout.LabelField("DATA SAVE MONITOR", EditorStyles.boldLabel);
    //
    //     // 2. Safety check: If 'character' is null, don't crash the inspector
    //     if (gm.Character != null)
    //     {
    //         EditorGUILayout.BeginVertical("box");
    //         EditorGUILayout.LabelField("Active Index:", gm.Index.ToString());
    //         EditorGUILayout.LabelField("Style Name:", gm.Character.danceStyle);
    //         // EditorGUILayout.LabelField("Current Score:", gm.Character.highScore.ToString());
    //         // EditorGUILayout.LabelField("Unlocked:", gm.Character.isUnlocked.ToString());
    //         EditorGUILayout.EndVertical();
    //     }
    //     else
    //     {
    //         EditorGUILayout.HelpBox("Character is currently NULL in memory. Start the game or check characterList.", MessageType.Warning);
    //     }
    //
    //     // 3. Debug Buttons
    //     if (GUILayout.Button("Manual Save Selected Character"))
    //     {
    //         // gm.SaveCharacter();
    //     }
    // }
}