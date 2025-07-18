using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
   [SerializeField] private UIDocument gameUI;
   
   private GameManager manager;

   private PointBoundary boundary;

   private VisualElement rootElement;
   
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
}
