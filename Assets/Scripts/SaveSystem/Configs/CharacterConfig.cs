using DefaultNamespace.ScriptableObjects;
using UnityEngine;

/*
 * Contains static immutable data for a character
 * in the character list
 * 
 */


[CreateAssetMenu(fileName = "NewCharacter", menuName = "ScriptableObject/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
	[Header("Profile")]
	public CharacterID danceStyle;
	public string dancerName;
	
	[Header("Sprites")]
	public Sprite rosterSprite;
	public Sprite idleSprite;
	public Sprite onSuccessSprite;
	public Sprite onFailSprite;
	public Sprite danceMoveSprite1;
	public Sprite danceMoveSprite2;
	public Sprite danceMoveSprite3;
	
	[Header("Roster Info")]
	public float costToUnlock;
}