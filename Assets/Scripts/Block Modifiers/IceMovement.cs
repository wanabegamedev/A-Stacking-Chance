using UnityEngine;

public class IceMovement : BlockModifier
{
    [SerializeField] private float iceSpeed = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateModifier()
    {
        var xAxis = Input.GetAxis("Horizontal");
        var zAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(xAxis * iceSpeed * Time.deltaTime, 0, zAxis * iceSpeed * Time.deltaTime);

        transform.Translate(direction, Space.World);


    }
}
