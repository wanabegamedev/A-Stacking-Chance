using UnityEngine;
using Tweens;

public class Grow : BlockModifier
{
    
    [SerializeField] private float growthScale = 0.1f;
    public override void ActivateModifier()
    {
        var scaleTween = new LocalScaleTween
        {
            to = new Vector3(transform.localScale.x + growthScale, transform.localScale.y + growthScale,  transform.localScale.z + growthScale),
            duration = 0.2f
            
        };

        gameObject.AddTween(scaleTween);

    }
}
