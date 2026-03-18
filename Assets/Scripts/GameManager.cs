using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDatabase;
    public static GameManager Instance { get; private set; } // Singleton instance shared globally
    public CharacterData SelectedCharacter { get; private set; }
    

    private void Awake() // Uses the Instance of the GameManager so that it persists across scenes 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        // Debug.Log($"[OldGameManager] Created instance: {GetInstanceID()}"); // helps to debug if we have multiple instances
    }
    
    public CharacterDatabase CharacterDatabase => characterDatabase;
    
    public void SetSelectedCharacter(CharacterData character)
    {
        SelectedCharacter = character;
        Debug.Log($"[GAME MANAGER] Selected new Charater {SelectedCharacter.name}");
    }
}