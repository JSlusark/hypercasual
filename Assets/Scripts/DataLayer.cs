using UnityEngine;


public class DataLayer : MonoBehaviour
{
	// === MACROS PLAYERPREFS ===
	private const string ACTIVE_CHARACTER = "SelectedCharacter";
	private const string HIGHSCORE = "HighScore";

	// =========== ATTRIBUTES ==========
	[Header("Character Database")]
	public CharacterData[] characterList; // Drag your .asset files here!
	private int selectedCharacter = 0;

	// =========== PUBLIC GETTERS ==========
	public int GetCharacterIndex => selectedCharacter; // public getter for selectedCharacter


	// =========== SINGLETON PATTERN AND DATA PERSISTENCE ==========
	public static DataLayer Instance;
	private void Awake() // inizializer method called when the script instance is being loaded and before any Start methods
	{
		if (Instance != null) // if instance is not null means it was aleady assigned before by another SelectionState instance
		{
			Destroy(gameObject); // destroys the gameObject to avoid we have a duplicate instance of SelectionState
			return;
		}
		Instance = this; // assigns THIS to the inialized static variable, making it a globally accessible(persistent) singleton(unique)
		DontDestroyOnLoad(gameObject); // it's what ensures persistence as it tells the editor to not destroy the gameObject that contains this script, so that its attributes and methods persists across scenes
									   // Debug.Log($"[DataLayer] Created instance: {GetInstanceID()}"); // helps to debug if we have multiple instances

		// Should i move these into their own class? Like a SaveLoadManager?
		LoadAllHighscores();
		LoadSelectedCharacter();
	}


	// ========== SAVE / LOAD FUNCTIONS TEST ==========
	public void SaveCharacterScore(int scoreFromGame, ref string message)
	{
		CharacterData character = characterList[selectedCharacter];
		string key = HIGHSCORE + character.danceStyleName;

		if (character.SetNewHighScore(scoreFromGame))
		{
			PlayerPrefs.SetInt(HIGHSCORE + character.danceStyleName, character.highScore); // saves character.highScore to a device storage with key HIGHSCORE+character.danceStyleName
			PlayerPrefs.Save(); // forces save to disk
			message = "You got a new High Score!";
			Debug.Log($"New highscore {character.highScore} for {character.danceStyleName} saved to device.");
		}

	}

	private void LoadAllHighscores()
	{
		string key;

		foreach (CharacterData character in characterList)
		{
			key = HIGHSCORE + character.danceStyleName;
			character.highScore = PlayerPrefs.GetInt(key, 0);
		}

		Debug.Log("All high scores loaded from device.");
	}

	public void SaveActiveCharacter(int newSelection)
	{
		if (newSelection != selectedCharacter) // makes sense to overwrite and save only if selection changed
		{
			selectedCharacter = newSelection;
			PlayerPrefs.SetInt(ACTIVE_CHARACTER, selectedCharacter);
			PlayerPrefs.Save();
			Debug.Log($"Selection saved to device character[{selectedCharacter}]: {characterList[selectedCharacter].danceStyleName}.");
		}
		else
			Debug.Log("Selection unchanged, nothing saved to device.");
	}

	private void LoadSelectedCharacter()
	{
		selectedCharacter = PlayerPrefs.GetInt(ACTIVE_CHARACTER, 0);
		Debug.Log($"Loaded previous character selection: [{selectedCharacter}]: {characterList[selectedCharacter].danceStyleName}");
	}
}