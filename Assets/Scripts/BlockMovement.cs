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
        var xAxis = Input.GetAxis("Horizontal");
        var zAxis = Input.GetAxis("Vertical");

        if (xAxis != 0 || zAxis != 0)
        {
            Vector3 direction = new Vector3(xAxis * Time.deltaTime * movementSpeed, 0, zAxis * Time.deltaTime * movementSpeed);

            foreach (var block in manager.selectedBlocks)
            {
                block.ActivateMoveModifiers();
                block.transform.position += direction;
            }
        }

      
    }
}
