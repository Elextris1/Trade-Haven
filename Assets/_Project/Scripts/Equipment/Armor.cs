using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Armor : Equipment
{
    [SerializeField] private SpriteLibraryAsset armorSprites;

    public override void Use(Transform user)
    {
        var playerVisual = user.GetComponentInChildren<AnimationController>();
        playerVisual.ChangeSprites(armorSprites);
    }
}
