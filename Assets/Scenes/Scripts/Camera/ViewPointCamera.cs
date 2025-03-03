using UnityEngine;

public class ViewPointCamera : MonoBehaviour {

  [HideInInspector] public Camera Camera;

  [Header("Rotation")]
  [SerializeField, Readonly] Vector3 masterRotation;
  public float maxRotationVertical = 90f;

  [Header("References")]
  [SerializeField] Transform reference;

  [Header("Shake")]
  [SerializeField, Readonly] Vector3 shakeRotation = Vector3.zero;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    Camera = GetComponent<Camera>();
  }

  void Update() {
    RotateReference(CalculateRotationHorizontal());
    RotateCamera(CalculateRotationVertical());
  }

  public void SetRotation(Vector3 rotation) {
    masterRotation = rotation;

    masterRotation.x = AdjustRotationVertical(masterRotation.x);
  }

  public void ApplyRotation(Vector3 offset) {
    masterRotation += offset;

    masterRotation.x = AdjustRotationVertical(masterRotation.x);
  }

  public void ApplyShake(Vector3 shake) {
    shakeRotation = shake;

    shakeRotation.x = AdjustRotationVertical(masterRotation.x + shakeRotation.x) - masterRotation.x;
  }

  public void RotateReference(Quaternion rotation) {
    reference.localRotation = rotation;
  }

  public void RotateCamera(Quaternion rotation) {
    transform.localRotation = rotation;
  }

  private float AdjustRotationVertical(float x) {
    return Mathf.Clamp(x, -maxRotationVertical, maxRotationVertical);
  }

  public Quaternion CalculateRotationVertical() {
    return Quaternion.Euler(masterRotation.x + shakeRotation.x, masterRotation.y + shakeRotation.y, shakeRotation.z);
  }

  public Quaternion CalculateRotationHorizontal() {
    return Quaternion.Euler(shakeRotation.x, masterRotation.y + shakeRotation.y, shakeRotation.z);
  }
}
