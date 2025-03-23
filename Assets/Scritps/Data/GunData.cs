using UnityEngine;

namespace Volariz.Data {

  [CreateAssetMenu(fileName = "Weapon Gun", menuName = "Weapons/Guns/Gun Data")]
  public class GunData : ScriptableObject {

    public string gunName;
    public LayerMask targetLayerMask;

    [Header("Fire Config")]
    public float shootingRange = 10f;
    public float fireRate = 10f;

    public bool isSingle = false;

    [Header("Reload Config")]
    public int magazineSize;
    public float reloadTime = 1_000f;

    [Header("Projectile Config")]
    public ProjectileData projectileData;

    [Header("Recoil Settings")]
    public Vector3 minRecoil;
    public Vector3 maxRecoil;

    public float resetAmount = 5;
    public float snappiness = 8;
    public float kickBackZ;
  }
}
