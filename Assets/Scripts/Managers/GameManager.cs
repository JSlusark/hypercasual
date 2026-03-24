using UnityEngine;

/*
 * Later make it inherit from Singleton as TouchManager
 * as T will ensure it's not confused with other singleton type children
 * 
 */

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharactersDatabase characterDatabase;
    [SerializeField] private CharacterData defaultCharacter;
    
    
    public static GameManager Instance { get; private set; } // Singleton instance shared globally
    public CharacterData SelectedCharacter { get; private set; }
    

    private void Awake() // Uses the Instance of the GameManager so that it persists across scenes 
    {
        if (Instance != null)
        {
            Debug.Log($"[GameManager] Duplicate found, destroying. Existing ID: {Instance.GetInstanceID()}, This ID: {GetInstanceID()}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        // Debug.Log($"[OldGameManager] Created instance: {GetInstanceID()}"); // helps to debug if we have multiple instances
        
        // sets default character to start here for now
        Debug.Log($"[GameManager] Instance set. ID: {GetInstanceID()}");
        SelectedCharacter = defaultCharacter;
    }
    
    public CharactersDatabase CharactersDatabase => characterDatabase;
    
    public void SetSelectedCharacter(CharacterData character)
    {
        SelectedCharacter = character;
        Debug.Log($"[GAME MANAGER] Selected new Character {SelectedCharacter.name}");
    }
}