using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{


    [Header("Input Action Asset")] 
  
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")] 
    
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")] 
   
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string select = "Select";


    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction selectAction;

    public Vector2 moveInputValue { get; private set; }
    public Vector2 lookInputValue { get; private set; }
    public bool selectInputValue { get; private set; }


    public static PlayerInputHandler instance;

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
    }


    void FindActions()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        selectAction = playerControls.FindActionMap(actionMapName).FindAction(select);
    }

    private void OnEnable()
    {
        //makes sure the events only run when the object is in the scene
        moveAction.Enable();
        lookAction.Enable();
       selectAction.Enable();
        
    }
    
    
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        selectAction.Disable();
        
    }

    void RegisterInputActions()
    {
        //registers listeners to the event so that data can be returned

        //The event  //a placeholder method //the output var    //the output
        moveAction.performed += callback => moveInputValue = callback.ReadValue<Vector2>();
        moveAction.canceled += callback => moveInputValue = Vector2.zero;
        
        lookAction.performed += callback => lookInputValue = callback.ReadValue<Vector2>();
        lookAction.canceled += callback => lookInputValue = Vector2.zero;
        
        selectAction.performed += callback => selectInputValue = true;
        selectAction.canceled += callback => selectInputValue = false;
    }
    
    
    
    
}
