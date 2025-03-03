using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  [SerializeField] Transform orientation;
  CharacterController characterController;

  [Header("Keybinds")]
  [SerializeField] KeyCode jumpKey = KeyCode.Space;

  [Header("Movement")]
  [SerializeField, Readonly] Vector3 velocity;
  [SerializeField] float gravity = 9.81f;
  [SerializeField] float moveSpeed = 0;
  float verticalVelocity;

  [Header("Jump")]
  [SerializeField] float jumpHeight;

  public bool IsGrounded { get { return characterController.isGrounded; } }

  float horizontalInput;
  float verticalInput;

  void Start() {
    characterController = GetComponent<CharacterController>();
  }

  void Update() {
    ReadInput();
    Movement();
  }

  void ReadInput() {
    if (IsGrounded) {
      horizontalInput = Input.GetAxisRaw("Horizontal");
      verticalInput = Input.GetAxisRaw("Vertical");
    }
  }

  void Movement() {
    GroundMovement();
  }

  void GroundMovement() {
    Vector3 direction = orientation.right * horizontalInput + orientation.forward * verticalInput;

    direction = direction.normalized * moveSpeed;

    direction.y = CalculateGravity();

    characterController.Move(direction * Time.deltaTime);
  }

  float CalculateGravity() {
    if (IsGrounded) {
      verticalVelocity = -1f;

      if (Input.GetKey(jumpKey)) {
        verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
      }
    } else {
      verticalVelocity -= gravity * Time.deltaTime;
    }

    return verticalVelocity;
  }
}
