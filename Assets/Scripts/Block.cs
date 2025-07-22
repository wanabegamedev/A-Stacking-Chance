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

  public bool blockLocked = false;


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
      if (blockLocked)
      {
          return;
      }
      
      selected = manager.SelectBlock(this);
      

      if (selected)
      {
          //Sets object to be outlined
          gameObject.layer = 3;
          rigid.useGravity = false;
      }
      else
      {
          //Sets object to be not outlined
          gameObject.layer = 0;
          rigid.useGravity = true;
      }
  }
}
