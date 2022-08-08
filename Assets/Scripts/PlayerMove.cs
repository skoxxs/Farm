using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int _turnSpeed = 1;
    [SerializeField] private float gravity = 9.18f;
    [SerializeField] private GameObject playerModel;

    private CharacterController characterController;
    private PlayerAI playerAI;
    void Start()
    {
        playerAI = GetComponent<PlayerAI>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 _moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            playerAI.PlayerIsMove(false);
        }
        else
        {
            playerAI.PlayerIsMove(true);
        }

        if (Vector3.Angle(Vector3.forward, _moveDirection) > 1f || Vector3.Angle(Vector3.forward, _moveDirection) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(playerModel.transform.forward, _moveDirection, _turnSpeed, 0.0f);
            playerModel.transform.rotation = Quaternion.LookRotation(direct);
        }

        _moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }
}
