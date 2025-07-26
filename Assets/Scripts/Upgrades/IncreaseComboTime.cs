using UnityEngine;

public class IncreaseComboTime : Upgrade
{
    private PointBoundary boundary;

    [SerializeField] private float increaseAmount = 0.5f;
    
    public override void TriggerUpgrade()
    {
        boundary = FindAnyObjectByType<PointBoundary>();

        boundary.gracePeriodTime += increaseAmount;

    }
}
