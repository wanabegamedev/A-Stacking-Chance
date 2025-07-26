using UnityEngine;
using Tweens;

public class Shrink : BlockModifier
{
    
    [SerializeField] private float shrinkScale = 0.9f;
    public override void ActivateModifier()
    {
        var scaleTween = new LocalScaleTween
        {
            to = new Vector3(transform.localScale.x * shrinkScale, transform.localScale.y * shrinkScale,  transform.localScale.z * shrinkScale),
            duration = 0.2f
            
        };

        gameObject.AddTween(scaleTween);

    }
}
