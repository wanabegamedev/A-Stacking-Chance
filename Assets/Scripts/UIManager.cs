
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIManager : MonoBehaviour
{
   [SerializeField] private UIDocument gameUI;
   [SerializeField] private UIDocument upgradeUI;
   
   private GameManager manager;

   private PointBoundary boundary; 

   private VisualElement rootElement;

   private VisualElement upgradeRootElement;

   
   
   private Label score;
   private Label progressBar;
   private Label multiplier;
   
   private VisualElement timer;
   private Label timerText;
    
    

    void Start()
    {
        rootElement = gameUI.rootVisualElement;

        manager = FindAnyObjectByType<GameManager>();
        boundary = FindAnyObjectByType<PointBoundary>();
        
        score = rootElement.Q("score-text") as Label;
        progressBar = rootElement.Q("progress-bar") as Label;
        multiplier = rootElement.Q("multiplier-text") as Label;
        timer = rootElement.Q("timer-ui");

        timerText = timer.Q("timer-text") as Label;
        
        upgradeRootElement = upgradeUI.rootVisualElement.Q("card-holder");
        upgradeUI.rootVisualElement.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        score.text = boundary.gracePeriodScoreHolder.ToString();
        progressBar.text = manager.score + "/" + manager.scoreUntilNextRound;

        if (boundary.gracePeriodActive)
        {
            timer.visible = true;
            multiplier.visible = true;

            timerText.text = boundary.timePassed.ToString();
            
            multiplier.text = manager.scoreMultiplier +"X";
        }
        else
        {
            timer.visible = false;
            multiplier.visible = false;
            
            
        }

    }

    public void DisplayUpgradeUI()
    {
        //uses the root element to make sure no upgrade UI is displayed
        upgradeUI.rootVisualElement.visible = true;
       
        var selectionCards = upgradeRootElement.Query<VisualElement>("selection").ToList();
        
        List<Upgrade> selectedUpgrades = manager.SelectUpgrades();

        var increment = 0;

        foreach (var upgrade in selectedUpgrades)
        {
            //Get selection and assosiated child elements
            var root = selectionCards[increment];

            var title = root.Q("title") as Label;
            var description =  root.Q("description") as Label;
            var image = root.Q("image");


            title.text = upgrade.upgradeName;
            description.text = upgrade.upgradeDesc;
            

        }
        
        
        
    }
}
