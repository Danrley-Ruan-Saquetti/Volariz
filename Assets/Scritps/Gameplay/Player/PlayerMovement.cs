using UnityEngine;
using Volariz.Editor.Inspector.Attributes;

namespace Volariz.Gameplay.Player {

  [RequireComponent(typeof(CharacterController))]
  public class PlayerMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] Transform headTransform;

    CharacterController character;

    [Header("Keybinds")]
    [SerializeField] KeyCode sprintKey;
    [SerializeField] KeyCode jumpKey;

    [Header("Movement")]
    [SerializeField] float walkSpeed = 7f;
    [SerializeField] float sprintSpeed = 20f;
    [SerializeField] float speedTransition = 1f;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 1f;

    [Header("Gravity")]
    [SerializeField] float gravity = 9.81f;

    [Header("State")]
    [SerializeField, Readonly] Vector3 _velocity;
    [SerializeField, Readonly] float _speed;

    public float Speed { get { return _speed; } }
    public Vector3 Velocity { get { return _velocity; } }
    public bool IsGrounded { get { return character.isGrounded; } }

    void Start() {
      character = GetComponent<CharacterController>();

      _speed = walkSpeed;
    }

    void Update() {
      ReadInputs();
      CalculateSpeed();
      CalculateGravity();
      MovePlayer();
    }

    void ReadInputs() {
      if (IsGrounded) {
        _velocity = new Vector3(Input.GetAxisRaw("Vertical"), _velocity.y, Input.GetAxisRaw("Horizontal"));
      }
    }

    void MovePlayer() {
      Vector3 direction = headTransform.right * _velocity.z + headTransform.forward * _velocity.x;

      direction = direction.normalized * _speed;

      direction.y = _velocity.y;

      character.Move(direction * Time.deltaTime);
    }

    void CalculateSpeed() {
      if (!IsGrounded) {
        return;
      }

      float speedTarget = walkSpeed;

      if (Input.GetKey(sprintKey)) {
        speedTarget = sprintSpeed;
      }

      _speed = Mathf.Lerp(_speed, speedTarget, speedTransition * Time.deltaTime);
    }

    void CalculateGravity() {
      if (IsGrounded) {
        _velocity.y = -1f;

        if (Input.GetKey(jumpKey)) {
          _velocity.y = Mathf.Sqrt(jumpHeight * gravity * 2f);
        }
      } else {
        _velocity.y -= gravity * Time.deltaTime;
      }
    }
  }
}
