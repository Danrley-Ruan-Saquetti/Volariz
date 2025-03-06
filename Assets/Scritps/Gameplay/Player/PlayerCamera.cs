using UnityEngine;
using Volariz.Editor.Inspector.Attributes;

namespace Volariz.Gameplay.Player {

  public class PlayerCamera : MonoBehaviour {

    [Header("References")]
    [SerializeField] Camera viewCamera;
    [SerializeField] Transform playerBody;

    [Header("Rotation")]
    [SerializeField, Readonly] Vector3 masterRotation;
    public float maxRotationVertical = 90f;

    [Header("Movement")]
    public Vector2 speed;
    public Vector2 acceleration;

    void Start() {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    void Update() {
      ReadInput();
      RotatePlayerBody(CalculateRotationHorizontal());
      RotateCamera(CalculateRotationVertical());
    }

    void ReadInput() {
      Vector3 mouseMove = new Vector3(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0);

      ApplyRotation(Time.deltaTime * speed * acceleration * mouseMove);
    }

    public void SetRotation(Vector3 rotation) {
      masterRotation = rotation;

      masterRotation.x = AdjustRotationVertical(masterRotation.x);
    }

    public void ApplyRotation(Vector3 offset) {
      masterRotation += offset;

      masterRotation.x = AdjustRotationVertical(masterRotation.x);
    }

    public void RotatePlayerBody(Quaternion rotation) {
      playerBody.localRotation = rotation;
    }

    public void RotateCamera(Quaternion rotation) {
      transform.localRotation = rotation;
    }

    private float AdjustRotationVertical(float x) {
      return Mathf.Clamp(x, -maxRotationVertical, maxRotationVertical);
    }

    public Quaternion CalculateRotationVertical() {
      return Quaternion.Euler(masterRotation.x, masterRotation.y, 0);
    }

    public Quaternion CalculateRotationHorizontal() {
      return Quaternion.Euler(0, masterRotation.y, 0);
    }
  }
}
