using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private Animator animator = null;

    private bool canMove = true;


    private Vector2 previousInput;

    private Controls controls;
    private Controls Controls {
        get
        {
            if(controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
      enabled = true;

      Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
      Controls.Player.Move.canceled += ctx => ResetMovement();
      // Controls.Player.Questions.performed += ctx => SetCanMove();
    }


    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    [ClientCallback]
    private void Update() {
      if(!isOwned) {return;}
      Move();
      animator.SetBool("isWalking", controller.velocity.magnitude > 0.2f); 
    }  

    [Client]
    private void SetMovement(Vector2 movement) => previousInput = movement;

    public void SetCanMove() => canMove = !canMove; 

    [Client]
    private void ResetMovement() => previousInput = Vector2.zero;

    [Client]
    private void Move() {
        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;
        right.y = 0f;
        forward.y = 0f;

        

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

        if(canMove) {
          movementSpeed = 5f;
        } else {
          movementSpeed = 0f;
        }
        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

}
