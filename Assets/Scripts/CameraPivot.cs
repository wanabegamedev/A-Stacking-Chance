using UnityEngine;

public class CameraPivot : MonoBehaviour
{

    [SerializeField] private float orbitRadius = 5f;

    [SerializeField] private float minimumOrbitRadius = 2;
    [SerializeField] private float maximumOrbitRadius = 200;

    [SerializeField] private float orbitSensitivity = 5;
    private bool orbitActive = true;

    [SerializeField] private Transform lookAtPoint;

    private float yaw = 0;
    private float pitch = 0;

    private PlayerInputHandler inputHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputHandler = PlayerInputHandler.instance;
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        OrbitPoint();
    }

    // Update is called once per frame
    void Update()
    {
//        print(inputHandler.cameraPivotInputValue);
        
        if (orbitActive)
        {
            if (inputHandler.cameraPivotInputValue > 0)
            {
               RotatePoint();
               OrbitPoint();
            }
         
            orbitRadius -= inputHandler.cameraZoomInputValue.y * orbitSensitivity;
            orbitRadius = Mathf.Clamp(orbitRadius, minimumOrbitRadius, maximumOrbitRadius);
            
           
        }
    }


    void OrbitPoint()
    {

        transform.position = lookAtPoint.position - transform.forward * orbitRadius;
    }

    void RotatePoint()
    {
        var mouseX = inputHandler.lookInputValue.x;
        var mouseY = inputHandler.lookInputValue.y;


        yaw += mouseX * orbitSensitivity;
        pitch += mouseY * orbitSensitivity;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

    }
}
