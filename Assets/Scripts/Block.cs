using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string blockName;

    public string blockScoreValue;
  
  [SerializeField] private List<BlockModifier> onMoveModifierList;
  [SerializeField] private List<BlockModifier> onTurnStartModifierList;
  [SerializeField] private List<BlockModifier> onTurnEndModifierList;
  [SerializeField] private List<BlockModifier> onRemoveModifierList;
  


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

  
}
