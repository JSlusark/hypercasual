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
	
	// Progress Data of a character is in CharacterProgress.cs (which handles runtime data)
	
}