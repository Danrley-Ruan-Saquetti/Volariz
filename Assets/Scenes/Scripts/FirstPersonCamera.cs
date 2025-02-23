using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

  public Transform reference;
  public float speed = 2f;
  public float lookHorizontalLimit = 45f;

  float rotationVertical = 0f;

  void Start() {
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update() {
    float offsetX = Input.GetAxis("Mouse X") * speed;
    float offsetY = Input.GetAxis("Mouse Y") * speed;

    rotationVertical -= offsetY;
    rotationVertical = Mathf.Clamp(rotationVertical, -lookHorizontalLimit, lookHorizontalLimit);

    transform.localEulerAngles = Vector3.right * rotationVertical;

    reference.Rotate(Vector3.up * offsetX);
  }
}
