using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "DancerGame/CharacterData")]
public class CharacterData : ScriptableObject
{
	[Header("Character Info")]
	public string dancerName;
	public string danceStyle; // will change all into private with getters/setters later
	public Sprite idleSprite;
	public Sprite rosterSprite;
	public float costToUnlock;
	
	// Leaving a note: was advised from Claude to remove Progress Data as they should handled at runtime, not handling for now
	[Header("Progress")]
	public bool isUnlocked;
	public int highScore;
	
}