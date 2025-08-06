using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{

    [SerializeField] private GameObject tooltip;

    [SerializeField] private TextMeshProUGUI blockName;
    [SerializeField] private TextMeshProUGUI blockDescription;


    private const string EOL = "\\u000a";
    




    public void SetupTooltip(Block block)
    {
     
        tooltip.SetActive(true);
        blockName.text = block.blockName;
        blockDescription.text = GenerateDescription(block);


    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    private string GenerateDescription(Block block)
    {
        string stringToReturn = "";
        

        stringToReturn += ReturnModifiers("Turn Start Modifiers", block.onTurnStartModifierList);
        

        stringToReturn += ReturnModifiers("Move Modifiers", block.onMoveModifierList);
        

        stringToReturn += ReturnModifiers("Turn End Modifiers", block.onTurnEndModifierList);
        
        stringToReturn += ReturnModifiers("On Remove Modifiers", block.onRemoveModifierList);
        
        
        stringToReturn += ReturnModifiers("Other Modifiers", block.otherModifierList);


        if (stringToReturn == "")
        {
            stringToReturn = "Just a regular block!";
        }
        
        return stringToReturn;
        
        
     
    }

    private string ReturnModifiers(string modifierListName, List<BlockModifier> mods)
    {
     

        string modifiersString = "";

        if (mods.Count <= 0)
        {
            return "";
        }
        else
        {
            modifiersString += modifierListName + ": " + EOL;
        }
        
        foreach (var mod in mods)
        {
            modifiersString += EOL;
            modifiersString += mod.modifierName + " : " + mod.modifierDescription + EOL;
            modifiersString += EOL;
        }

        return modifiersString;

    }
}
