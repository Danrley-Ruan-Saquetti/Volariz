using UnityEngine;
using Volariz.Editor.Inspector.Attributes;

namespace Volariz.Gameplay.Weapon {

  [RequireComponent(typeof(Rigidbody))]
  public class Projectile : MonoBehaviour {

    [Header("References")]
    [SerializeField, Readonly] Rigidbody rb;

    [Header("State")]
    [Readonly] public Vector3 direction;
    [Readonly] public Vector3 shootForce;
    [Readonly] public Vector3 upwardForce;

    [SerializeField, Readonly] bool collided = false;

    void Start() {
      rb = GetComponent<Rigidbody>();

      transform.forward = direction;

      rb.AddForce(shootForce, ForceMode.Impulse);
      rb.AddForce(upwardForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) {
      if (!collided && !collision.gameObject.CompareTag("Weapon") && !collision.gameObject.CompareTag("Player")) {
        collided = true;
        Destroy(gameObject);
      }
    }
  }
}
