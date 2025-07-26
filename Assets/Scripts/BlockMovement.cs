using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    private GameManager manager;

    [SerializeField] private float movementSpeed = 20;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePieces();
    }

    void MovePieces()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        
        //provides normalised vectors in local space, but converted to world space
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        //Remove Y axis to stop the camera interfering with vertical movement
        camForward.y = 0;
        camRight.y = 0;

        //stops any slowdown when the camera is tilted
       camForward = camForward.normalized;
       camRight = camRight.normalized;


        Vector3 relativeForwardInput = vertical * camForward;
        Vector3 relativeHorizontalInput = horizontal * camRight;

        Vector3 cameraRelativeInput = relativeForwardInput + relativeHorizontalInput;

        if (cameraRelativeInput != new Vector3(0, 0, 0))
        {
           

            foreach (var block in manager.selectedBlocks)
            {
                if (block == null)
                {
                    print("Error: Block Does Not Exist");
                    manager.DeselectAllBlocks();
                    return;
                }
                
                block.ActivateMoveModifiers();
                block.transform.Translate(cameraRelativeInput * (movementSpeed * Time.deltaTime), Space.World);
            }
        }

      
    }
}
