using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirtualMouseUI : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRectTransform;
    private VirtualMouseInput virtualMouseInput;
    private PlayerInputHandler inputHandler;
    
    [SerializeField] private GameObject virtualMouseUI;

    private void Start()
    {
        virtualMouseInput = GetComponent<VirtualMouseInput>();
        
        inputHandler = PlayerInputHandler.instance;

        inputHandler.OnGameDeviceChanged += PlayerInput_OnGameDeviceChanged;
        
        virtualMouseUI.SetActive(false);
    }



    private void LateUpdate()
    {
      Vector2 virtualMousePos =  virtualMouseInput.virtualMouse.position.value;

      virtualMousePos.x = Mathf.Clamp(virtualMousePos.x, 0, Screen.width);
      virtualMousePos.y = Mathf.Clamp(virtualMousePos.y, 0, Screen.height);
      
      InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePos);
    }

    private void Update()
    {
        //Accounts for screen scaling for the mouse position
        transform.localScale = Vector3.one * (1f / canvasRectTransform.localScale.x);
        
        transform.SetAsLastSibling();
    }

    private void PlayerInput_OnGameDeviceChanged(object sender, EventArgs e)
    {
        
        if (inputHandler.ReturnActiveGameDevice() == PlayerInputHandler.GameDevice.Gamepad)
        {
           
            virtualMouseUI.SetActive(true);
            Cursor.visible = false;
        }
        else
        {
           
            virtualMouseUI.SetActive(false);
            Cursor.visible = true;
        }
    }
  
}
