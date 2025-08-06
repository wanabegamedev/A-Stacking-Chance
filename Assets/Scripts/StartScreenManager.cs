using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StartScreenManager : MonoBehaviour
{
   [SerializeField] private GameObject startButton;
   
   [SerializeField] private GameObject keyboardUI;
   
   [SerializeField] private GameObject controllerUI;

   private PlayerInputHandler inputHandler;
   
   
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputHandler = PlayerInputHandler.instance;
        //EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.ReturnActiveGameDevice() == PlayerInputHandler.GameDevice.Gamepad)
        {
            keyboardUI.SetActive(false);
            controllerUI.SetActive(true);
        }
        else
        {
            keyboardUI.SetActive(true);
            controllerUI.SetActive(false);
        } 
    }
}
