using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "NewCharacter", menuName = "ScriptableObject/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
	[Header("Character Info")]
	public CharacterID id;
	public string name;
	public int costToUnlock;
	
	[Header("----------- ROSTER CONFIGS -----------")]
	[Header("🎨 Sprites")]
	public Sprite rosterSprite;
	
	
	[Header("----------- DANCESESSION CONFIGS -----------")]
	[Header("🎥 Clips")]
	public AnimationClip idleAnimation;

	[Header("🎨 Sprites")] 
	public Sprite[] reelBackground;
	public Sprite onSuccessSprite;
	public Sprite onFailSprite;
	//  change into public sprite[] DanceMoves and use enums ad
	public Sprite danceMoveSpriteUp;
	public Sprite danceMoveSpriteRight;
	public Sprite danceMoveSpriteDown;
	public Sprite danceMoveSpriteLeft;
	
	[Header("🔊 Audio")]
	public AudioClip onSetSuccess;
	
	
	[Header("----------- PROFILE CONFIGS -----------")]
	[Header("🎨 Sprites")]
	public Sprite idleSprite;
	public Sprite[] studioBackground; // 
	
}