using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for new input system
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    //Add the serialized fields for the new input system
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //New input system
        float xThrust = movement.ReadValue<Vector2>().x;
        float yThrust = movement.ReadValue<Vector2>().y;
        Debug.Log(xThrust);
        Debug.Log(yThrust);

        //Old control binding workings
        // float horizontalThrust = Input.GetAxis("Horizontal");
        // Debug.Log(horizontalThrust);

        // float verticalThrust = Input.GetAxis("Vertical"); 
        // Debug.Log(verticalThrust);

    }
//required to turn on and off new movement controls
    void OnEnable() 
    {
        movement.Enable();   
    }

    void OnDisable() 
    {
        movement.Disable();     
    }
}
