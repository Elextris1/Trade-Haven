using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteLibrary spriteLibrary;

    private void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out spriteLibrary);
    }
    private void Update()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        var direction = InputManager.GetAxis2D();
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    public void ChangeSprites(SpriteLibraryAsset newSprites)
    {
        spriteLibrary.spriteLibraryAsset = newSprites;
    }
}
