using UnityEngine;
using DefaultNamespace.ScriptableObjects;

    public class CharacterModel
    {
        private readonly CharacterConfig _config;
        private readonly CharacterData _data;

        public CharacterModel(CharacterConfig config, CharacterData data)
        {
            _config = config;
            _data = data;

            // sets Moshpit as the default unlocked character - might find a cleaner solution for this perhaps by adding in PlayerData
            if (config.id == CharacterID.Moshpit)
                _data.isUnlocked = true;
        }
        
        
        // Character Info
        public CharacterID Id => _config.id;
        public string Name => _config.dancerName; 
        public int ExperienceLevel => _data.experienceLevel;
        public float Followers => _data.followers;
        
        // Sprites
        public Sprite RosterSprite => _config.rosterSprite;
        public Sprite IdleSprite => _config.idleSprite;
        public Sprite OnSetComplete => _config.onSetComplete;
        public Sprite OnFailSprite => _config.onFailSprite;
        public Sprite DanceMoveSpriteUp => _config.danceMoveSpriteUp;
        public Sprite DanceMoveSpriteRight => _config.danceMoveSpriteRight;
        public Sprite DanceMoveSpriteDown => _config.danceMoveSpriteDown;
        public Sprite DanceMoveSpriteLeft => _config.danceMoveSpriteLeft;

       // Roster info
        public bool IsUnlocked => _data.isUnlocked;
        public float CostToUnlock => _config.costToUnlock;

        
        public void Unlock()
        {
            if (!_data.isUnlocked)
            {
                _data.isUnlocked = true;
                Debug.Log($"{Name} has been unlocked!");
            }
        }

        // Other Methods
        // Add followers
        // LevelUp
        
        // private void LevelUp()
        // {
        // }
    
}