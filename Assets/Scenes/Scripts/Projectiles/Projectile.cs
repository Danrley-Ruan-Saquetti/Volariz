using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

  [Header("References")]
  [SerializeField, Readonly] Rigidbody rb;

  [Header("State")]
  [Readonly] public Vector3 direction = Vector3.zero;
  [Readonly] public Vector3 shootForce = Vector3.zero;
  [Readonly] public Vector3 upwardForce = Vector3.zero;

  [SerializeField, Readonly] bool collided = false;

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
