using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Referenced https://docs.unity3d.com/ScriptReference/CharacterController.Move.html for simple character controller.
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = 6.5f;
    public float rotationSpeed = 720f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    //public float jumpHeight = 4.0f;
    public GameObject recipeCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Gravity
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Move
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Normalize diagonal movement speed
        if (move.magnitude > 0)
        {
            move.Normalize();
            transform.forward = Vector3.Slerp(transform.forward, move.normalized, Time.deltaTime * rotationSpeed);
        }
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Jump
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        // Gravity
        playerVelocity.y += gravityValue * Time.deltaTime * 4.0f;
        controller.Move(playerVelocity * Time.deltaTime * 4.0f);

        //Enable recipe camera while held
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            recipeCamera.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            recipeCamera.SetActive(false);
        }

    }
}
