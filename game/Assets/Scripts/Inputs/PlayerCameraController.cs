using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(0f, 15f);
    [SerializeField] private static float CAMERA_VELOCITY_X = 50f;
    [SerializeField] private static float CAMERA_VELOCITY_Y = 0.25f;
    private Vector2 cameraVelocity = new Vector2(CAMERA_VELOCITY_X, CAMERA_VELOCITY_Y);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private bool canMove = true;
    
    private Controls controls;
    private Controls Controls {
        get
        {
            if(controls != null) { return controls; }
            return controls = new Controls();
        }
    }
    private CinemachineTransposer transposer;



    public override void OnStartAuthority() {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);

        enabled = true;

        controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        // controls.Player.Questions.performed += ctx => SetCanMove();
    }

    public void SetCanMove() => canMove = !canMove;

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis) {
        float deltaTime = Time.deltaTime;

        if(canMove) { 
            cameraVelocity.y = CAMERA_VELOCITY_Y;
            cameraVelocity.x = CAMERA_VELOCITY_X;
        } else {
            cameraVelocity.y = 0f;
            cameraVelocity.x = 0f;
        }

        transposer.m_FollowOffset.y = Mathf.Clamp(
            transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime),
            maxFollowOffset.x,
            maxFollowOffset.y);

        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    }
    
}
