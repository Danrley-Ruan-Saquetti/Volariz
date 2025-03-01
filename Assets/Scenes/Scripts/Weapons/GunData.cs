using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Gun", menuName = "Weapons/Guns/Gun Data")]
public class GunData : ScriptableObject {

  public string gunName;
  public LayerMask targetLayerMask;

  [Header("Fire Config")]
  public float shootingRange = 10f;
  public float fireRate = 10f;

  [Header("Reload Config")]
  public int magazineSize = 0;
  public float reloadTime = 1_000f;

  [Header("Recoil Settings")]
  public float recoilAmount = 1f;
  public Vector2 minRecoil = Vector3.zero;
  public Vector2 maxRecoil = Vector3.zero;
  public float recoilSpeed = 100f;
  public float resetRecoilSpeed = 100f;
}
