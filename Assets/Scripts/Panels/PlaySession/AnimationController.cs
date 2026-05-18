using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    // base animation clip with animator
    [SerializeField] private Animator _baseAnimator;
    [SerializeField] private AnimationClip baseClip;
    
    // movement clip is a static image that floats, the base is  the same for each character but the image should be different based on what received
    [SerializeField] private AnimationClip movementClip;
     [SerializeField] private Image movementImage;
    
    private AnimatorOverrideController _animatorOverride; // needs to be one per character

    private void Start()
    {
            
        // Creates override and assigns it to baseAnimator on runtime to override it
        _animatorOverride = new AnimatorOverrideController(_baseAnimator.runtimeAnimatorController);
        _baseAnimator.runtimeAnimatorController = _animatorOverride;
     
        ChangeClip(CharacterCatalogue.Instance.activeCharacter.Config.idleAnimation);
        
    }

    public void ChangeClip(AnimationClip characterClip)
    {
        if (characterClip == null) return;

        // looks for the baseClip in your state machine and replaces it with the characterClip
        _animatorOverride[baseClip] = characterClip;
        int _currentState =  _baseAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
        _baseAnimator.Play(_currentState, 0, 0f);
    }
    
    public void ShowDanceClip(Sprite danceSprite)
    {
        if (danceSprite == null) return;

        // 1. Substitute the image graphic directly on the UI component
        movementImage.sprite = danceSprite;
        
        // 2. Play the exact same dance clip (the wiggle template)
        ChangeClip(movementClip);
    }
    
    
    // 1. ADD THIS TARGET FUNCTION HERE
    // It must be public and have no parameters so the Unity Editor can see it.
    public void OnClipEnd()
    {
        AnimationClip idleClip = CharacterCatalogue.Instance.activeCharacter.Config.idleAnimation;
        ChangeClip(idleClip);
    }
    
    
    
}

