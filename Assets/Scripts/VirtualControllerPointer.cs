using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VirtualControllerPointer : MonoBehaviour
{

    [SerializeField] private GameObject pointerUI;

    private PlayerInputHandler inputHandler;

    [SerializeField] private float pointerSpeed;

    private PlayerInput playerInputData;

    private void Start()
    {
        playerInputData = GetComponent<PlayerInput>();
        inputHandler = PlayerInputHandler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null && inputHandler.lookInputValue.x != 0 || inputHandler.lookInputValue.y != 0)
        {
           pointerUI.SetActive(true);
           
           MovePointer();
        }
        else
        {
            pointerUI.SetActive(false);
        }
    }


    void MovePointer()
    {
        if (inputHandler.cameraPivotInputValue <= 0)
        {
            print("moving pointer");
            print(playerInputData.currentControlScheme);
            Vector2 moveVector = inputHandler.lookInputValue;
            
            pointerUI.transform.Translate(moveVector * pointerSpeed, Space.Self);
            
        }
    }
}
