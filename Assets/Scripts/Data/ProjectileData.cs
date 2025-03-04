using UnityEngine;

namespace Volariz.Data {
  [CreateAssetMenu(fileName = "Projectiles", menuName = "Projectiles/Projectile Data")]
  public class ProjectileData : ScriptableObject {

    [Header("Settings")]
    public GameObject model;
    public float shootForce;
    public float upwardForce;

    [Header("Spread Config")]
    public float spread;
  }
}
