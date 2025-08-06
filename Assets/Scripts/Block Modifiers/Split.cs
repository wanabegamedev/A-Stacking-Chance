using UnityEngine;

public class Split : BlockModifier
{
  [SerializeField] private Transform parentHolder;

    public override void ActivateModifier()
    {
        for (int i = 0; i < parentHolder.childCount; i++)
        {
            parentHolder.GetChild(i).GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 200);
        }
        
        parentHolder.DetachChildren();
    }
}
