using UnityEngine;

public class CameraPivot : MonoBehaviour
{

    [SerializeField] private float orbitRadius = 5f;

    [SerializeField] private float minimumOrbitRadius = 2;
    [SerializeField] private float maximumOrbitRadius = 20;

    [SerializeField] private float orbitSensitivity = 5;
    private bool orbitActive = true;

    [SerializeField] private Transform lookAtPoint;

    private float yaw = 0;
    private float pitch = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (orbitActive)
        {
            if (Input.GetMouseButton(0))
            {
                OrbitPoint();
            }
          
        }
    }


    void OrbitPoint()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");


        yaw += mouseX * orbitSensitivity;
        pitch += mouseY * orbitSensitivity;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        
        

        orbitRadius -= Input.mouseScrollDelta.y / orbitSensitivity;

        orbitRadius = Mathf.Clamp(orbitRadius, minimumOrbitRadius, maximumOrbitRadius);

        transform.position = lookAtPoint.position - transform.forward * orbitRadius;

    }
}
