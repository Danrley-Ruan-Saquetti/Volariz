using UnityEngine;

public class CameraMovement : MonoBehaviour {

  [Header("Movement")]
  public Vector2 speed = new(200f, 200f);
  public Vector2 acceleration = Vector2.one;

  [Header("Rotation")]
  public float maxRotationVertical = 90f;
  Vector3 rotation;

  [Header("References")]
  [SerializeField] Transform referenceHorizontal;
  [SerializeField] Transform referenceVertical;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    ReadInput();

    RotateReferenceVertical(CalculateRotationVerticalMouseMove());
    RotateReferenceHorizontal(CalculateRotationHorizontalMouseMove());
  }

  void ReadInput() {
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed.x * acceleration.x;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed.y * acceleration.y;

    rotation.y += mouseX;
    rotation.x -= mouseY;

    rotation.x = AdjustRotationVertical(rotation.x);
  }

  public float AdjustRotationVertical(float x) {
    return Mathf.Clamp(x, -maxRotationVertical, maxRotationVertical);
  }

  public void RotateReferenceVertical(Quaternion rotation) {
    referenceVertical.rotation = rotation;
  }

  public void RotateReferenceHorizontal(Quaternion rotation) {
    referenceHorizontal.rotation = rotation;
  }

  public Quaternion CalculateRotationVerticalMouseMove() {
    return Quaternion.Euler(rotation.x, rotation.y, rotation.z);
  }

  public Quaternion CalculateRotationHorizontalMouseMove() {
    return Quaternion.Euler(0, rotation.y, rotation.z);
  }
}
