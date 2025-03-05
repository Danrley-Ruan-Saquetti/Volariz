using UnityEngine;

namespace Volariz.Gameplay.Player {

  [RequireComponent(typeof(CharacterController))]
  public class PlayerMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] Transform headTransform;

    CharacterController character;

    [Header("Movement")]
    [SerializeField] float walkSpeed = 7;
    [SerializeField] float sprintSpeed = 12;

    [Header("State")]
    [SerializeField] float speed;

    void Start() {
      character = GetComponent<CharacterController>();

      speed = walkSpeed;
    }

    void Update() {

    }
  }
}
