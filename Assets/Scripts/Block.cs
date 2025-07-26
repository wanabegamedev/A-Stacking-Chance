using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string blockName;

    public int blockScoreValue;

    [Range(1, 100)]
    public int blockWeight = 10;
  
  public List<BlockModifier> onMoveModifierList;
  public List<BlockModifier> onTurnStartModifierList;
  public List<BlockModifier> onTurnEndModifierList;
  public List<BlockModifier> onRemoveModifierList;
  
  //Holds modifiers that run in their own time
  public List<BlockModifier> otherModifierList;

  public bool blockLocked = false;


  private GameManager manager;
  private Rigidbody rigid;
  private MeshRenderer renderer;
  private TooltipManager tooltip;

  
  public bool selected;

  private void Awake()
  {
      rigid = GetComponent<Rigidbody>();
      manager = FindAnyObjectByType<GameManager>();
      renderer = GetComponent<MeshRenderer>();
      tooltip = FindAnyObjectByType<TooltipManager>();
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
      foreach (var modifier in onTurnEndModifierList)
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
  
  public void ActivateOnRemoveModifiers()
  {
      foreach (var modifier in onRemoveModifierList)
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

  private void OnMouseEnter()
  {
      tooltip.SetupTooltip(this);
  }

  public void OnMouseExit()
  {
      tooltip.HideTooltip();
  }
}
