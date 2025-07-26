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

        stringToReturn += "Turn Start Modifiers:";

        stringToReturn += EOL;

        stringToReturn += ReturnModifiers(block.onTurnStartModifierList);
        
        stringToReturn += EOL;

        stringToReturn += "Move Modifiers:";
        
        stringToReturn += EOL;
        
        stringToReturn += ReturnModifiers(block.onMoveModifierList);
        
        stringToReturn += EOL;

        stringToReturn += "Turn End Modifiers:";
        
        stringToReturn += EOL;
        
        stringToReturn += ReturnModifiers(block.onTurnEndModifierList);
        
        stringToReturn += EOL;

        stringToReturn += "On Remove Modifiers:";
        
        stringToReturn += ReturnModifiers(block.onRemoveModifierList);
        
        stringToReturn += EOL;
        
        stringToReturn += "Other Modifiers:";
        
        stringToReturn += ReturnModifiers(block.otherModifierList);
        
        stringToReturn += EOL;
        
        
        
        return stringToReturn;
        
        
     
    }

    private string ReturnModifiers(List<BlockModifier> mods)
    {
     

        string modifiersString = "";
        
        foreach (var mod in mods)
        {
            modifiersString += EOL;
            modifiersString += mod.modifierName + " : " + mod.modifierDescription + EOL;
            modifiersString += EOL;
        }

        return modifiersString;

    }
}
