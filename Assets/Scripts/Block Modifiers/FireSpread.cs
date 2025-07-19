using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireSpread : BlockModifier
{
    [SerializeField] private float spreadRadius = 10;

    public int lifeSpan = 3;

    private MeshRenderer renderer;
    private readonly int baseColor = Shader.PropertyToID("_BaseColor");

    private void OnEnable()
    {
        renderer = GetComponent<MeshRenderer>();
        
        renderer.material.SetColor(baseColor, Color.red);

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
            
        }
        
        
        //reduce lifespan
    
        lifeSpan -= 1;
        renderer.material.color = Color.red;

        if (lifeSpan <= 1)
        {
            Destroy(gameObject);
        }
        
        
    }
}
