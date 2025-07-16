using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string blockName;

    public int blockScoreValue;
  
  [SerializeField] private List<BlockModifier> onMoveModifierList;
  [SerializeField] private List<BlockModifier> onTurnStartModifierList;
  [SerializeField] private List<BlockModifier> onTurnEndModifierList;
  [SerializeField] private List<BlockModifier> onRemoveModifierList;


  private GameManager manager;
  private Rigidbody rigid;

  public bool selected;

  private void Awake()
  {
      rigid = GetComponent<Rigidbody>();
      manager = FindAnyObjectByType<GameManager>();
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
      }
      else
      {
          rigid.useGravity = true;
      }
  }
}
