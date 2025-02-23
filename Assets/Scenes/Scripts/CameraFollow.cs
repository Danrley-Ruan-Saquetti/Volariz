using UnityEngine;

public class CameraFollow : MonoBehaviour {

  public Transform reference;

  void Update() {
    transform.position = reference.position;
  }
}
