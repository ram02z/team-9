using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovment : MonoBehaviour
{
    //References to the controller moving the player and the main camera
    public CharacterController CharController;
    public Transform MainCamera;

    //Sensitivity of camera
    public float Speed = 4f;

    //How quickly the camera turns when using it to rotate player
    public float TurnTime = 0.1f;
    float TurnVelocity;


    void Start()
    {
        //Locks cursor to middle of screen and hides it from view
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        //Gets input from mouse
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Translates input into an (x,y,z) format
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Performs action if there is an input
        if (direction.magnitude >= 0.1f)
        {
            //Dictates how the character should move according to camera rotation and movment
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + MainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnVelocity, TurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Rotates the character with the camera rotation
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            CharController.Move(moveDir * Speed * Time.deltaTime);
        }
    }
}
