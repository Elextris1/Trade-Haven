using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        TryGetComponent(out animator);
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

    /*This is very improvised since my old system with Sprite Library 
     * wasn't working after changing the version. */
    public void ChangeSprites(Transform prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity, GetComponentInParent<PlayerController>().transform);
        Destroy(gameObject);
    }
}
