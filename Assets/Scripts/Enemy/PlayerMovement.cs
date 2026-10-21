using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Gravity")]
    public float gravity = -20f;
    public float groundedForce = -2f;

    [Header("Camera")]
    public Transform cameraTransform;

    public Animator animator;
    private CharacterController controller;
    private Vector3 verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (Camera.main != null && cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // 1. Read movement input.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // 2. Calculate movement relative to the camera.
        Vector3 moveDirection = inputDirection;

        if (cameraTransform != null)
        {
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Ignore the camera's upward/downward tilt.
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            moveDirection =
                cameraForward * vertical +
                cameraRight * horizontal;

            moveDirection = Vector3.ClampMagnitude(
                moveDirection, 1f);
        }

        if (animator != null)
        {
            float movementAmount = inputDirection.magnitude;

            animator.SetFloat("Speed",movementAmount, 0.1f, Time.deltaTime);
        }

        // 3. Rotate the player toward the movement direction.
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        // 4. Apply gravity.
        if (controller.isGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = groundedForce;
        }

        verticalVelocity.y += gravity * Time.deltaTime;

        // 5. Combine horizontal movement and vertical velocity.f
        Vector3 velocity =moveDirection * moveSpeed + verticalVelocity;

        // 6. Move with collision detection.
        controller.Move(velocity * Time.deltaTime);
    }
}
