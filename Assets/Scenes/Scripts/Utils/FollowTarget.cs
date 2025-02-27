using UnityEngine;

public class FollowTarget : MonoBehaviour {

  public Transform reference;

  void Update() {
    transform.position = reference.position;
  }
}
