using UnityEngine;
using Tweens;
public class RotateTween : MonoBehaviour
{
    [SerializeField] private float rotateAmount = 20;
    [SerializeField] private float rotateTime = 5;
    
    

    void OnEnable()
    {
        var rotateTween = new RotationTween
        {
            from = Quaternion.Euler(0, 0, -rotateAmount), 
            to = Quaternion.Euler(0, 0, rotateAmount),
            duration = rotateTime,
            usePingPong = true,
            isInfinite = true
        };
        
        gameObject.AddTween(rotateTween);
    }
    
}
