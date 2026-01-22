using System.Data.Common;
using UnityEngine;

/*
	Model(data) provides the data structure to be used by the Controller(logic) and rendered by the View(UI).
	Data has to persist across scenes.

	DataLayer is the persistence layer.
	Monobehaviour: the class logic script that can be attached to game objects in unity
	GameObject: anything that is added in the hierarchy in unity, hey become useful by having Components attached (scripts, SpriteRenderer, Rigidbody, etc).

	Serialiser: a system that converts objects/fields in memory into stored data, and later reconstructs them.
	[System.Serializable]: makes the struct serializable( able to be saved in memory from a persistance object)
 */

public class DataLayer : MonoBehaviour // name of c class bluerint, Monobehaviour  makes so that the class can be attached to game objects in unity
{

	// [System.Serializable] // makes the struct serializable( able to be saved in memory from a persistance object)
	// public struct CharacterData // data on the selected charcter, persistent while playing the game
	// {
	// 	[SerializeField] private SpriteRenderer selectedCharacterSprite; // sprite renderer of the selected character

	// 	// public int characterIndex;        // passed to the LevelController so it can render the selected character in the levelScene

	// 	// to be expanded with fields like:
	// 	// public int[] experiencePerCharacter;
	// 	// public bool[] unlockedCharacters;
	// }




	[System.Serializable]
	public struct Character // holds data on each character available
	{
		// public bool isSelected; // is this the currently selected character
		// public IdentifierCase identifierCase; // unique identifier for the character
		// public int index; // index of the character in the selectionOptions array
		// 				  // public GameObject characterPrefab;
		public bool isUnlocked;
		public string characterDanceStyle;
		public Sprite characterSprite;
		public AnimationClip idleAnimation; // animation played when the player is idle
		public AnimationClip failAnimation; // animation played when the player fails a move
		public AnimationClip levelCompleteAnimation; // animation played when dance level increases
		public int highScore; // highest score achieved with this character in a dance session
							  // public int level; // level required to unlock the character's evolution
							  // public int expPoints;
							  // public int evolutionLevel; // number of evolution levels for this character
							  // public int evolutionState; // number of evolution levels for this character

	}

	// i am tempted to put selectionOptions here too
	// but that would couple the data layer with
	// the definition layer
	// breaking the separation of concerns principle
	// ---- data layer vs definition layer.. is selectionoptions really definition layer though?

	// public CharacterData CharacterData; // We need to instantiate the struct to use the properties inside it,
	public Character[] characterList; // since its's just a data container it does not need to be static
	public Character selectedCharacter; // selected character data


	public static DataLayer Instance; // the GLOBAL static variable initialization, holds THIS when Awake



	private void Awake() // inizializer method called when the script instance is being loaded and before any Start methods
	{
		if (Instance != null) // if instance is not null means it was aleady assigned before by another SelectionState instance
		{
			Destroy(gameObject); // destroys the gameObject to avoid we have a duplicate instance of SelectionState
			return;
		}
		Instance = this; // assigns THIS to the inialized static variable, making it a globally accessible(persistent) singleton(unique)
		DontDestroyOnLoad(gameObject); // it's what ensures persistence as it tells the editor to not destroy the gameObject that contains this script, so that its attributes and methods persists across scenes

		// for (int i = 0; i < characterList.Length; i++)
		// {
		// 		characterList[i].index = i;
		// 		characterList[i].index = i;

		// }

	}
}