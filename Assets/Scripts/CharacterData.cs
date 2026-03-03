using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "DancerGame/CharacterData")]
public class CharacterData : ScriptableObject
{
	[Header("Character Info")]
	public string dancerName;
	public string danceStyle; // will change all into private with getters/setters later
	public Sprite baseSprite;
	
	[Header("Character State")]
	public bool isUnlocked;
	public float costToUnlock;
	
	[Header("Progress Data")]
	public int highScore;

	public bool SetNewHighScore(int newScore)
	{
		if (newScore > highScore)
		{
			highScore = newScore;
			return true;
		}
		return false;
	}
}