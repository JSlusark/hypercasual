using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "NewCharacter", menuName = "ScriptableObject/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
	[Header("Profile")]
	public CharacterID id;
	public string dancerName;
	public string danceStyle;
	
	[Header("Sprites")]
	public Sprite rosterSprite;
	public Sprite idleSprite;
	public AnimationClip idleAnimation;
	[FormerlySerializedAs("onSuccessSprite")] public Sprite onSetComplete;
	public Sprite onFailSprite;
	public Sprite danceMoveSpriteUp;
	public Sprite danceMoveSpriteRight;
	public Sprite danceMoveSpriteDown;
	public Sprite danceMoveSpriteLeft;
	
	[Header("Roster Info")]
	public float costToUnlock;
}