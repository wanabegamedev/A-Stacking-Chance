using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
    
    public enum GameDevice {

        KeyboardMouse,
        Gamepad,
        
    }

    
    [Header("Input Action Asset")] 
  
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")] 
    
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")] 
   
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string select = "Select";
    [SerializeField] private string cameraPivot = "CameraPivot";
    [SerializeField] private string cameraZoom = "cameraZoom";


    private InputAction moveAction;
    private InputAction lookAction;
    public InputAction selectAction;
    private InputAction cameraPivotAction;
    private InputAction cameraZoomAction;

    public Vector2 moveInputValue { get; private set; }
    public Vector2 lookInputValue { get; private set; }
    public bool selectInputValue { get; private set; }
    
    public float cameraPivotInputValue { get; private set; }
    
    public Vector2 cameraZoomInputValue { get; private set; }


    public static PlayerInputHandler instance;

    private GameDevice activeGameDevice;
    public event EventHandler OnGameDeviceChanged;

    private Coroutine stopRumbleAfterTime;


    public Vector2 lookInputRawValue;
    public Vector2 zoomInputRawValue;
    
    public static class InputActionButtonExtensions
    {
        public static bool GetButton(InputAction action) => action.ReadValue<float>() > 0;
        public static bool GetButtonDown(InputAction action) => action.triggered && action.ReadValue<float>() > 0;
        public static bool GetButtonUp(InputAction action) => action.triggered && action.ReadValue<float>() == 0;
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            
        }
        else
        {
            instance = this;
        }
        
        FindActions();
        
        RegisterInputActions();

        InputSystem.onActionChange += InputSystem_OnActionChange;
    }

    private void Update()
    {
        lookInputRawValue = lookAction.ReadValue<Vector2>();
        zoomInputRawValue = cameraZoomAction.ReadValue<Vector2>();
    }

    void FindActions()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        selectAction = playerControls.FindActionMap(actionMapName).FindAction(select);
        cameraPivotAction = playerControls.FindActionMap(actionMapName).FindAction(cameraPivot);
        cameraZoomAction = playerControls.FindActionMap(actionMapName).FindAction(cameraZoom);
    }

    private void OnEnable()
    {
        //makes sure the events only run when the object is in the scene
        moveAction.Enable();
        lookAction.Enable();
       selectAction.Enable();
       cameraPivotAction.Enable();
       cameraZoomAction.Enable();
        
    }
    
    
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        selectAction.Disable();
        cameraPivotAction.Disable();
        cameraZoomAction.Disable();
        
    }

    void RegisterInputActions()
    {
        //registers listeners to the event so that data can be returned

        //The event  //a placeholder method //the output var    //the output
        
         
        cameraPivotAction.performed += callback => cameraPivotInputValue = callback.ReadValue<float>();
        cameraPivotAction.canceled += callback => cameraPivotInputValue = 0f;
        
        moveAction.performed += callback => moveInputValue = callback.ReadValue<Vector2>();
        moveAction.canceled += callback => moveInputValue = Vector2.zero;
        
        lookAction.performed += callback => lookInputValue = callback.ReadValue<Vector2>();
        lookAction.canceled += callback => lookInputValue = Vector2.zero;
        
        selectAction.performed += callback => selectInputValue = true;
        selectAction.canceled += callback => selectInputValue = false;
       
        cameraZoomAction.performed += callback => cameraZoomInputValue = callback.ReadValue<Vector2>();
        cameraZoomAction.canceled += callback => cameraZoomInputValue =  Vector2.zero;
    }

    private void InputSystem_OnActionChange(object arg1, InputActionChange inputActionChange)
    {
        //Checks to see if an input has been performed
        if (inputActionChange == InputActionChange.ActionPerformed && arg1 is InputAction)
        {
            InputAction inputAction = arg1 as InputAction;

            //if it is the virtual mouse return, as that is not an input device e.g. controller or keyboard
            if (inputAction.activeControl.device.displayName == "VirtualMouse")
            {
                return;
            }

            if (inputAction.activeControl.device is Gamepad)
            {
                if (activeGameDevice != GameDevice.Gamepad)
                {
                    ChangeActiveGameDevice(GameDevice.Gamepad);
                }
            } 
            else
            {
                if (activeGameDevice != GameDevice.KeyboardMouse)
                {
                    ChangeActiveGameDevice(GameDevice.KeyboardMouse);
                }
            }
        
            
            //Room for expansion with other control devices
        
        }
    }

    private void ChangeActiveGameDevice(GameDevice activeGameDevice)
    {
        //updates script activeGameDevice to input game device
        this.activeGameDevice = activeGameDevice;
        
        print("Active Game Device is now:" + activeGameDevice);
        
        OnGameDeviceChanged?.Invoke(this, EventArgs.Empty);

     

        
        
    }


    public GameDevice ReturnActiveGameDevice()
    {
        return activeGameDevice;
    }

    public void RumblePulse(float lowFreq, float highFreq, float duration)
    {
        if (ReturnActiveGameDevice() != GameDevice.Gamepad)
        {
            return;
        }
        
        Gamepad controller = Gamepad.current;
        
        
        controller.SetMotorSpeeds(lowFreq, highFreq);

        stopRumbleAfterTime = StartCoroutine(StopRumble(duration));



    }

    private IEnumerator StopRumble(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (ReturnActiveGameDevice() == GameDevice.Gamepad)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
    
}
