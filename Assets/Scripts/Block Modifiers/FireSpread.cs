using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireSpread : BlockModifier
{
    [SerializeField] private float spreadRadius = 10;

    public int lifeSpan = 3;

    private MeshRenderer renderer;
    private readonly int lavaIntensity = Shader.PropertyToID("_Lava_Factor");
    private Material lavaMat;

    private void OnEnable()
    {
        
        
        
        renderer = GetComponent<MeshRenderer>();
        
        //we always need to make sure to assign the lava material when setting up the modifier
        lavaMat = renderer.material;
       
        renderer.material.SetFloat(lavaIntensity, 2);

    }

    public override void ActivateModifier()
    {
        var size = 10;
        Collider[] results = {};
        Physics.OverlapSphereNonAlloc(transform.position, spreadRadius, results);

        foreach (var block in results)
        {
            //if block already has fire spread return
            if (block.TryGetComponent(out FireSpread reference))
            {
                return;
            }
           FireSpread addedFireSpread =  block.gameObject.AddComponent(typeof(FireSpread)) as FireSpread;
           
           block.GetComponent<Block>().onTurnStartModifierList.Add(addedFireSpread);

           block.GetComponent<Renderer>().material = lavaMat;

        }
        
        
        //reduce lifespan
        renderer = GetComponent<MeshRenderer>();

        lifeSpan -= 1;
        renderer.material.SetFloat(lavaIntensity, 0.5f * lifeSpan );

        if (lifeSpan <= 1)
        {
            Destroy(gameObject);
        }
        
        
    }
}
