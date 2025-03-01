using UnityEngine;

public class ViewPointCamera : MonoBehaviour {

  [Header("Rotation")]
  Vector3 rotation;
  public float maxRotationVertical = 90f;

  [Header("References")]
  [SerializeField] Transform reference;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    RotateReference(CalculateRotationHorizontal());
    RotateCamera(CalculateRotationVertical());
  }

  public void SetRotation(Vector3 rotation) {
    this.rotation = rotation;

    AdjustRotationVertical();
  }

  public void ApplyRotation(Vector3 rotation) {
    this.rotation += rotation;

    AdjustRotationVertical();
  }

  public void RotateReference(Quaternion rotation) {
    reference.localRotation = rotation;
  }

  public void RotateCamera(Quaternion rotation) {
    transform.localRotation = rotation;
  }

  private void AdjustRotationVertical() {
    this.rotation.x = Mathf.Clamp(this.rotation.x, -maxRotationVertical, maxRotationVertical);
  }

  public Quaternion CalculateRotationVertical() {
    return Quaternion.Euler(rotation.x, rotation.y, 0);
  }

  public Quaternion CalculateRotationHorizontal() {
    return Quaternion.Euler(0, rotation.y, 0);
  }
}
