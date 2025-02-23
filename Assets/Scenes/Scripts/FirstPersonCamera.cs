using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

  public Transform reference;
  public float speedX = 100f;
  public float speedY = 100f;

  public float maxRotationVertical = 90f;

  float rotationX;
  float rotationY;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speedX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedY;

    rotationY += mouseX;
    rotationX -= mouseY;

    rotationX = Mathf.Clamp(rotationX, -maxRotationVertical, maxRotationVertical);

    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    reference.rotation = Quaternion.Euler(0, rotationY, 0);
  }
}
