using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  [SerializeField] Transform orientation;
  Rigidbody rb;

  [Header("Keybinds")]
  [SerializeField] KeyCode jumpKey = KeyCode.Space;

  [Header("Movement")]
  [SerializeField] float moveSpeed = 0;

  [SerializeField] float groundDrag;

  [SerializeField] float jumpForce;
  [SerializeField] float jumpSpeedMultiplier;
  [SerializeField] float jumpCooldown;
  Vector3 moveDirection;

  [Header("Ground Check")]
  [SerializeField] float playerHeight;
  [SerializeField] LayerMask groundLayer;
  bool isGrounded;

  float horizontalInput;
  float verticalInput;

  void Start() {
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }

  void Update() {
    isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

    ReadInput();

    if (CanJump() && Input.GetKey(jumpKey)) {
      Jump();
    }

    SpeedControl();

    if (isGrounded) {
      rb.linearDamping = groundDrag;
    } else {
      rb.linearDamping = 0;
    }
  }

  void FixedUpdate() {
    MovePlayer();
  }

  private void ReadInput() {
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");
  }

  private void MovePlayer() {
    moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

    Vector3 force = 10f * moveSpeed * moveDirection.normalized;

    if (!isGrounded) {
      force *= jumpSpeedMultiplier;
    }

    rb.AddForce(force, ForceMode.Force);
  }

  private void SpeedControl() {
    var flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

    if (flatVel.magnitude > moveSpeed) {
      Vector3 limitedVel = flatVel.normalized * moveSpeed;

      rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
    }
  }

  private void Jump() {
    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
  }

  bool CanJump() {
    return isGrounded;
  }
}
