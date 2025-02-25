using UnityEngine;

public class MouseMovement : MonoBehaviour {

  public float speedX = 100f;
  public float speedY = 100f;
  public float accelerationX = 1f;
  public float accelerationY = 1f;

  public float maxRotationVertical = 90f;

  protected float rotationX;
  protected float rotationY;

  protected void ReadInput() {
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speedX * accelerationX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedY * accelerationY;

    rotationY += mouseX;
    rotationX -= mouseY;

    rotationX = Mathf.Clamp(rotationX, -maxRotationVertical, maxRotationVertical);
  }
}
