using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string blockName;

    public int blockScoreValue;
  
  public List<BlockModifier> onMoveModifierList;
  public List<BlockModifier> onTurnStartModifierList;
  public List<BlockModifier> onTurnEndModifierList;
  public List<BlockModifier> onRemoveModifierList;


  private GameManager manager;
  private Rigidbody rigid;
  private MeshRenderer renderer;

  public bool selected;

  private void Awake()
  {
      rigid = GetComponent<Rigidbody>();
      manager = FindAnyObjectByType<GameManager>();
      renderer = GetComponent<MeshRenderer>();
  }


  public void ActivateTurnStartModifiers()
  {
      foreach (var modifier in onTurnStartModifierList)
      {
           modifier.ActivateModifier();
      }
  }
  
  public void ActivateOnTurnEndModifiers()
  {
      foreach (var modifier in onMoveModifierList)
      {
          modifier.ActivateModifier();
      }
  }
  
  
  public void ActivateMoveModifiers()
  {
    
      foreach (var modifier in onMoveModifierList)
      {
          modifier.ActivateModifier();
      }
  }
  
  public void OnRemoveModifiers()
  {
      foreach (var modifier in onMoveModifierList)
      {
          modifier.ActivateModifier();
      }
  }

  private void OnMouseUp()
  {
      
      
      selected = manager.SelectBlock(this);
      

      if (selected)
      {
          rigid.useGravity = false;
          renderer.material.color -= new Color(0, 0, 0, 0.7f);
      }
      else
      {
          rigid.useGravity = true;
          renderer.material.color += new Color(0, 0, 0, 0.7f);
      }
  }
}
