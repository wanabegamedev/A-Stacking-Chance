using UnityEngine;

public class StickyGlue : BlockModifier
{
    [SerializeField] private float stickyRange = 5;
    public override void ActivateModifier()
    {
        var targets = Physics.OverlapSphere(transform.position, 5);

        foreach (var block in targets)
        {
            if (block.TryGetComponent(out Block referenceBlock) && referenceBlock.transform.parent != transform)
            {
                block.transform.parent = transform;
            }
        }
    }
}
