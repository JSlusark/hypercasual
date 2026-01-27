using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "DancerGame/CharacterData")]
public class CharacterData : ScriptableObject
{
	[Header("Type")]
	public string danceStyle; // will change all into private with getters/setters later
	public Sprite baseSprite;
	public bool isUnlocked;

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