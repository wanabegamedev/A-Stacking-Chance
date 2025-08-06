using UnityEngine;

public class StickyGlue : BlockModifier
{
    [SerializeField] private float stickyRange = 5;

    [SerializeField] private Transform parentHolder;
    
    
    public override void ActivateModifier()
    {
        var targets = Physics.OverlapSphere(transform.position, stickyRange);

        foreach (var block in targets)
        {
            if (block.TryGetComponent(out Block referenceBlock) && referenceBlock.transform.parent != parentHolder)
            {
                block.transform.parent = parentHolder;
            }
        }
    }
}
