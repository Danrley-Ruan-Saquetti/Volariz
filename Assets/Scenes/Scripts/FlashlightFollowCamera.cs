using UnityEngine;

public class FlashlightFollowCamera : MouseMovement {

  void Update() {
    ReadInput();

    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
  }
}
