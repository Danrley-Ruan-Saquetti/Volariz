using UnityEngine;

public class FirstPersonCamera : MouseMovement {

  public Transform referenceHorizontal;
  public Transform referenceVertical;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    ReadInput();

    RotateHorizontal(referenceHorizontal);
    RotateVertical(referenceVertical);
  }

  void RotateHorizontal(Transform transform) {
    transform.rotation = Quaternion.Euler(0, rotationY, 0);
  }

  void RotateVertical(Transform transform) {
    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
  }
}
