using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image characterSprite;
    [SerializeField] private Animator characterAnimator;

    [Header("Animation Sockets")]
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip danceClip; // keyframe template, only image changes
    
    private AnimatorOverrideController _animatorOverride;

    private void Start()
    {
        // 1. Set up the local override architecture
        _animatorOverride = new AnimatorOverrideController(characterAnimator.runtimeAnimatorController);
        characterAnimator.runtimeAnimatorController = _animatorOverride;
     
        // 2. Boot up with the catalogue's active character idle
        var activeConfig = CharacterCatalogue.Instance.activeCharacter.Config;
        SetClip(activeConfig.idleAnimation);
    }
    
    public void SetSprite(Sprite idle)
    {
        characterSprite.sprite = idle;
    }

    // Changes standard looping animations (like idles)
    public void SetClip(AnimationClip characterClip)
    {
        if (characterClip == null) return;

        _animatorOverride[idleClip] = characterClip;
        int currentState = characterAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
        characterAnimator.Play(currentState, 0, 0f);
    }
    
    // Changes the graphic and triggers your shared editor wiggle clip
    public void ShowDanceClip(Sprite danceSprite)
    {
        if (danceSprite == null) return;

        characterSprite.sprite = danceSprite;
        SetClip(danceClip);
    }
    
    // Triggered seamlessly by the blue diamond Animation Event on this GameObject!
    public void OnClipEnd()
    {
        var activeConfig = CharacterCatalogue.Instance.activeCharacter.Config;
        SetClip(activeConfig.idleAnimation);
    }
}