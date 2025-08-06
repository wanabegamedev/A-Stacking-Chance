
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

   private GameManager manager;

   private PointBoundary boundary; 
   
   
   
   
   [SerializeField]  private TextMeshProUGUI score;
   [SerializeField]  private TextMeshProUGUI progressBar;
   [SerializeField] private TextMeshProUGUI multiplier;
   
   [SerializeField] private TextMeshProUGUI timer;


   [Header("Upgrade System")] 
   [SerializeField] private UpgradeHolder selection1;
   [SerializeField] private UpgradeHolder selection2;
   [SerializeField] private UpgradeHolder selection3;


   [Header("Game Over UI")] [SerializeField]
   private TextMeshProUGUI highscoreText;
   

   [Header("Canvas")]
   [SerializeField] private GameObject upgradeCanvas;
   [SerializeField] private GameObject gameUICanvas;
   [SerializeField] private GameObject endUICanvas;

   
   
   
    void Start()
    {

        manager = FindAnyObjectByType<GameManager>();
        boundary = FindAnyObjectByType<PointBoundary>();


        upgradeCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        score.text = boundary.gracePeriodScoreHolder.ToString();
        progressBar.text = manager.score + "/" + manager.scoreUntilNextRound;

        if (boundary.gracePeriodActive)
        {
            timer.gameObject.SetActive(true);
            multiplier.gameObject.SetActive(true);

            timer.text = "Combo Time: " + boundary.timePassed;
            
            multiplier.text = manager.scoreMultiplier +"X";
        }
        else
        {
            timer.gameObject.SetActive(false);
            multiplier.gameObject.SetActive(false);
            
            
        }

        //makes sure UI support is enabled when a controller is plugged in
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame &&
            EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(selection1.gameObject);
        }

    }

    public void DisplayUpgradeUI()
    {
        //always set selected game object for controller support
        EventSystem.current.SetSelectedGameObject(selection1.gameObject);
       
        //Makes sure the first button is always selected
        selection1.GetComponentInChildren<Button>().Select();
        
        manager.inUpgradePhase = true;
        gameUICanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        //uses the root element to make sure no upgrade UI is displayed
        
        
        List<Upgrade> selectedUpgrades = manager.SelectUpgrades();

        var increment = 0;

        foreach (var upgrade in selectedUpgrades)
        {

            switch (increment)
            {
                case 0:
                    selection1.SetupHolder(upgrade);
                    break;
                case 1:
                    selection2.SetupHolder(upgrade);
                    break;
                case 2:
                    selection3.SetupHolder(upgrade);
                    break;
            }

            increment++;

        }
        
        
        
    }

    public void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
        gameUICanvas.SetActive(true);
    }

    public void ShowEndUI()
    {
        endUICanvas.SetActive(true);
        
        upgradeCanvas.SetActive(false);
        gameUICanvas.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(endUICanvas.GetComponentInChildren<Button>().gameObject);

        highscoreText.text = "High Score: " + manager.highScore;
    }
}
