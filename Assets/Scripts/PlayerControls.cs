using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for new input system
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    //Add the serialized fields for the new input system
    [SerializeField] float thrustPower = 25f;
    [SerializeField] float xRange = 14f;
     [SerializeField] float yRange = 9f;
     [SerializeField] float posPitchFactor = -3f;
     [SerializeField] float controlPitchFactor = -10f;
     [SerializeField] float posYawFactor = 3.5f;
     [SerializeField] float controlRollFactor = -25f;
    [SerializeField] InputAction movement;
    [SerializeField, Range(0f,100f)] float controlGravityFactor = 10f;

    float xThrustRaw, yThrustRaw, xDelta, yDelta, xThrust, yThrust;
    
    //[SerializeField] InputAction fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xThrustRaw = movement.ReadValue<Vector2>().x;
        yThrustRaw = movement.ReadValue<Vector2>().y;
        PrepareThrust();
        ProcessTranslation();
        ProcessRotation();

    }

    void PrepareThrust()
    {
        xDelta = Mathf.MoveTowards(xDelta,xThrustRaw, Time.fixedDeltaTime * controlGravityFactor);
        yDelta = Mathf.MoveTowards(yDelta,yThrustRaw, Time.fixedDeltaTime * controlGravityFactor);

        xThrust = Mathf.Clamp(xDelta,-1f,1f);
        yThrust = Mathf.Clamp(yDelta,-1f,1f);
    }
    void ProcessRotation()
    {
        // things todo with rotation (pitch, yaw, roll)
        // pitch - nod head, yaw - shake head, roll - cock head
        float positionPitchImpact = transform.localPosition.y * posPitchFactor;
        float thrustPitchImpact = yThrust * controlPitchFactor;

        float positionYawImpact = transform.localPosition.x * posYawFactor;

        float thrustRollImpact = xThrust * controlRollFactor;
        
        float pitch = positionPitchImpact + thrustPitchImpact;
        float yaw = positionYawImpact;
        float roll = thrustRollImpact;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        //New input system
        float xOffset = thrustPower * xThrust * Time.deltaTime;
        float yOffset = thrustPower * yThrust * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        //stop plane flying off the screen
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
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
