using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Gun", menuName = "Weapons/Guns/Gun Data")]
public class GunData : ScriptableObject {

  public string gunName;
  public LayerMask targetLayerMask;

  [Header("Fire Config")]
  public float shootingRange;
  public float fireRate;

  [Header("Reload Config")]
  public float magazineSize;
  public float reloadTime;
}
