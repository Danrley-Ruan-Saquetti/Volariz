using UnityEngine;
using Volariz.Editor.Inspector.Attributes;

public class PlayerMovement : MonoBehaviour {

  [Header("References")]
  [SerializeField] Transform orientation;
  CharacterController characterController;

  [Header("Keybinds")]
  [SerializeField] KeyCode jumpKey = KeyCode.Space;
  [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

  [Header("Movement")]
  [SerializeField] float gravity = 9.81f;
  [SerializeField] float walkSpeed = 5f;

  [Header("Movement Sprint")]
  [SerializeField] float sprintSpeed = 10f;
  [SerializeField] float sprintTransitSpeed = 5f;

  [Header("Jump")]
  [SerializeField] float jumpHeight = 1f;

  [Header("State")]
  [SerializeField, Readonly] float verticalVelocity;
  [SerializeField, Readonly] float speed;

  [SerializeField, Readonly] float horizontalInput;
  [SerializeField, Readonly] float verticalInput;

  public bool IsGrounded { get { return characterController.isGrounded; } }

  void Start() {
    characterController = GetComponent<CharacterController>();

    speed = walkSpeed;
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

    direction = direction.normalized * CalculateSpeed();

    direction.y = CalculateGravity();

    characterController.Move(direction * Time.deltaTime);
  }

  float CalculateSpeed() {
    if (!IsGrounded) {
      return speed;
    }

    if (Input.GetKey(sprintKey)) {
      speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
    } else {
      speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
    }

    return speed;
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
