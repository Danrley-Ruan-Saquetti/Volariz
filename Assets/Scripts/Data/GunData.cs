using UnityEngine;

namespace Volariz.Data {
  [CreateAssetMenu(fileName = "Weapon Gun", menuName = "Weapons/Gun Data")]
  public class GunData : ScriptableObject {

    public string gunName;
    public LayerMask targetLayerMask;

    [Header("Fire Config")]
    public float shootingRange = 10f;
    public float fireRate = 10f;
    public bool isSingle = false;

    [Header("Reload Config")]
    public int magazineSize = 0;
    public float reloadTime = 1_000f;

    [Header("Projectile Config")]
    public ProjectileData projectileData;

    [Header("Recoil Settings")]
    public Vector3 minRecoil = Vector3.zero;
    public Vector3 maxRecoil = Vector3.zero;
    public float resetSpeed = 5;
    public float ricochetSpeed = 8;
    public float kickBackZ = 0f;
  }
}
