
using UnityEngine;

public class Explosive : BlockModifier
{
    [SerializeField] private float explosionRadius = 10;
    [SerializeField] private float explosionPower = 2;

    [SerializeField] private AudioClip explosionClip;

    
    public override void ActivateModifier()
    {
       
        Vector3 explosionPosition = transform.position;

      
        RaycastHit[] colliders =  Physics.SphereCastAll(transform.position, explosionRadius, Vector3.up);
        foreach (RaycastHit hit in colliders)
        {
           print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            if (hit.transform.TryGetComponent(out Block b))
            {
                Rigidbody rigid = hit.transform.GetComponent<Rigidbody>();
                rigid.AddExplosionForce(explosionPower, explosionPosition, explosionRadius, 3f);
            
                print("exploded");
            }

           
        }
        
        AudioManager.instance.PlaySound(explosionClip);
        
        Destroy(gameObject);
    }
}
