using UnityEngine;

public class CameraMovement : MonoBehaviour {

  public float speedX = 100f;
  public float speedY = 100f;
  public float accelerationX = 1f;
  public float accelerationY = 1f;

  public float maxRotationVertical = 90f;

  protected float rotationX;
  protected float rotationY;

  public Transform referenceHorizontal;
  public Transform referenceVertical;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    ReadInput();

    RotateVertical(referenceVertical);
    RotateHorizontal(referenceHorizontal);
  }

  protected void ReadInput() {
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speedX * accelerationX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedY * accelerationY;

    rotationY += mouseX;
    rotationX -= mouseY;

    rotationX = Mathf.Clamp(rotationX, -maxRotationVertical, maxRotationVertical);
  }

  void RotateVertical(Transform transform) {
    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
  }

  void RotateHorizontal(Transform transform) {
    transform.rotation = Quaternion.Euler(0, rotationY, 0);
  }
}
