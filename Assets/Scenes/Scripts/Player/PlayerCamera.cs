using UnityEngine;

public class PlayerCamera : MonoBehaviour {

  [SerializeField] ViewPointCamera viewPoint;

  [Header("Movement")]
  public Vector2 speed = Vector2.one;
  public Vector2 acceleration = Vector2.one;

  void Update() {
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed.x * acceleration.x;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed.y * acceleration.y;

    viewPoint.ApplyRotation(new Vector3(-mouseY, mouseX, 0));
  }
}
