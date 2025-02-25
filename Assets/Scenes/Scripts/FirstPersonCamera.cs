using UnityEngine;

public class FirstPersonCamera : MouseMovement {

  public Transform reference;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    ReadInput();

    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    reference.rotation = Quaternion.Euler(0, rotationY, 0);
  }
}
