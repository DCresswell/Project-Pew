using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for new input system
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    [Header("Objects")]
    //Add the serialized fields for the new input system
    [SerializeField] GameObject[] lasers;

    [Header("Ship movement settings")]
    [Tooltip("How fast ship moves up/down/left/right based on player input")][SerializeField] float thrustPower = 25f;
    [Tooltip("How far ship can move horizontally")][SerializeField] float xRange = 14f;
    [Tooltip("How far ship can move vertically")][SerializeField] float yRange = 9f;

    [Header("Ship rotation settings")]
     [Tooltip("'Head nod' rotation based on screen position")][SerializeField] float posPitchFactor = -3f;
     [Tooltip("'Head nod' rotation based on user input")][SerializeField] float controlPitchFactor = -10f;
     [Tooltip("'Head shake' rotation based on screen position")][SerializeField] float posYawFactor = 3.5f;
     [Tooltip("'Head cock' rotation based on user input")][SerializeField] float controlRollFactor = -25f;
    [Tooltip("Gravity delay factor on thrust user input")][SerializeField, Range(0f,100f)] float controlGravityFactor = 10f;

    [Header("Input controls")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    float xThrustRaw, yThrustRaw, xDelta, yDelta, xThrust, yThrust;

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
        ProcessFiring();

    }

    void ProcessFiring()
    {
        //Debug.Log(temp);
        if (fire.ReadValue<float>()>0.1f) 
        {
            //Debug.Log("fire");
            SetLasersActive(true);
        }
        else
        {
            //Debug.Log("NO FIRE");
            SetLasersActive(false);
        }
    }
        void SetLasersActive(bool value)
    {
        // for each laser in array - activate
        foreach (GameObject i in lasers)
        {
            var emissionModule = i.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = value;
        }
    }
    // process to create gravity effect on keyboard input
    void PrepareThrust()
    {
        //add a delay in input so movement ramps up to 1
        xDelta = Mathf.MoveTowards(xDelta,xThrustRaw, Time.fixedDeltaTime * controlGravityFactor);
        yDelta = Mathf.MoveTowards(yDelta,yThrustRaw, Time.fixedDeltaTime * controlGravityFactor);

        //limit movement value to 1.
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
        fire.Enable();  
    }

    void OnDisable() 
    {
        movement.Disable(); 
        fire.Disable();    
    }
}
