using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

  [HideInInspector] public Vector3 direction = Vector3.zero;
  [HideInInspector] public Vector3 shootForce = Vector3.zero;
  [HideInInspector] public Vector3 upwardForce = Vector3.zero;

  Rigidbody rb;

  bool collided = false;

  void Start() {
    rb = GetComponent<Rigidbody>();

    transform.forward = direction;

    rb.AddForce(shootForce, ForceMode.Impulse);
    rb.AddForce(upwardForce, ForceMode.Impulse);
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided) {
      collided = true;
      Destroy(gameObject);
    }
  }
}
