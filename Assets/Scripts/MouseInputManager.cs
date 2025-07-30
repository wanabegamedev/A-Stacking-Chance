using System;
using Mono.Cecil;
using UnityEngine;

public class BlockHoverEventArgs : EventArgs
{
    public Block hoveredBlock;

    public BlockHoverEventArgs(Block blockHoveredOver)
    {
        hoveredBlock = blockHoveredOver;
    }
    
    
}

public class MouseInputManager : MonoBehaviour
{
    private PlayerInputHandler inputHandler;

    [SerializeField] private GameObject virtualMouseUI;

    public event  EventHandler<BlockHoverEventArgs> onCursorHover;

    private TooltipManager tooltip;

    private void Start()
    {
        inputHandler = PlayerInputHandler.instance;
        tooltip = FindAnyObjectByType<TooltipManager>();
    }
    

    private void Update()
    {
        if (PlayerInputHandler.InputActionButtonExtensions.GetButtonDown(inputHandler.selectAction))
        {
            
            FireMouseCheck();
        }
        
        MouseHoverCheck();
    }

    private void FireMouseCheck()
    {
        RaycastHit hit;
        Ray outputRay = new Ray();

        if (inputHandler.ReturnActiveGameDevice() == PlayerInputHandler.GameDevice.Gamepad)
        {

            outputRay = Camera.main.ScreenPointToRay(virtualMouseUI.transform.position);

        }
        else if (inputHandler.ReturnActiveGameDevice() == PlayerInputHandler.GameDevice.KeyboardMouse)
        {
            outputRay =  Camera.main.ScreenPointToRay(Input.mousePosition);
              
        }
        
        if (Physics.Raycast(outputRay, out hit, Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent(out Block blockRef))
            {
                blockRef.OnBlockClick();
            }
            else
            {
                tooltip.HideTooltip();
            }
        }
        
    }

    private void MouseHoverCheck()
    {
        //This function checks every frame if cursor is over block and shows relevent information
        
        RaycastHit hit;
        Ray outputRay = new Ray();

        if (inputHandler.ReturnActiveGameDevice() == PlayerInputHandler.GameDevice.Gamepad)
        {

            outputRay = Camera.main.ScreenPointToRay(virtualMouseUI.transform.position);

        }
        else if (inputHandler.ReturnActiveGameDevice() == PlayerInputHandler.GameDevice.KeyboardMouse)
        {
            outputRay =  Camera.main.ScreenPointToRay(Input.mousePosition);
              
        }
        
        if (Physics.Raycast(outputRay, out hit, Mathf.Infinity))
        {
     
            if (hit.transform.TryGetComponent(out Block blockRef))
            {
              
               onCursorHover?.Invoke(this, new BlockHoverEventArgs(blockRef));
              
            }
          
            
        }
        else
        {
                tooltip.HideTooltip();
        }
    }
}
