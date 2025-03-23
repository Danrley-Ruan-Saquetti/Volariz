using UnityEngine;
using Volariz.Core;
using Volariz.Editor.Inspector.Attributes;

namespace Volariz.Gameplay.Player {

  public class PlayerCamera : MonoBehaviour {

    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] MouseReadInput mouseRead;

    [Header("Rotation")]
    [SerializeField, Readonly] Vector3 masterRotation;
    public float maxRotationVertical = 90f;

    [Header("Movement")]
    public Vector2 speed;
    public Vector2 acceleration = Vector3.one;

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
      Vector2 mouseEnter = new Vector2(-mouseRead.mouseEnter.y, mouseRead.mouseEnter.x);

      ApplyRotation(Time.deltaTime * speed * acceleration * mouseEnter);
    }

    void ApplyRotation(Vector3 offset) {
      masterRotation += offset;

      masterRotation.x = AdjustRotationVertical(masterRotation.x);
    }

    void RotatePlayerBody(Quaternion rotation) {
      player.localRotation = rotation;
    }

    void RotateCamera(Quaternion rotation) {
      transform.localRotation = rotation;
    }

    private float AdjustRotationVertical(float x) {
      return Mathf.Clamp(x, -maxRotationVertical, maxRotationVertical);
    }

    Quaternion CalculateRotationVertical() {
      return Quaternion.Euler(masterRotation.x, 0, 0);
    }

    Quaternion CalculateRotationHorizontal() {
      return Quaternion.Euler(0, masterRotation.y, 0);
    }
  }
}
