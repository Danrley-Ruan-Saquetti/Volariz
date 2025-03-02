using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ProceduralRecoil))]
public abstract class Gun : MonoBehaviour {

  public GunData gunData;
  public Transform attackPoint;
  public bool isSingle;

  [SerializeField] protected ViewPointCamera viewPoint;
  protected Camera cameraView;
  protected ProceduralRecoil proceduralRecoil;

  [SerializeField, Readonly] int currentAmmo = 0;
  [SerializeField, Readonly] float nextTimeToFire = 0f;
  [SerializeField, Readonly] bool isReloading = false;

  public bool IsAmmoFull { get { return currentAmmo == gunData.magazineSize; } }
  public bool HasAmmo { get { return currentAmmo > 0f; } }
  public bool IsTimeToFire { get { return Time.time >= nextTimeToFire; } }

  public virtual void Awake() {
    currentAmmo = gunData.magazineSize;
    isSingle = gunData.isSingle;
  }

  public virtual void Start() {
    proceduralRecoil = GetComponent<ProceduralRecoil>();
    cameraView = viewPoint.GetComponent<Camera>();
  }

  public virtual void Update() { }

  public void TryReload() {
    if (CanReload()) {
      StartCoroutine(Reload());
    }
  }

  public void TryShoot() {
    if (CanShoot()) {
      HandleShoot();
    }
  }

  IEnumerator Reload() {
    isReloading = true;

    yield return new WaitForSeconds(gunData.reloadTime);

    isReloading = false;
    currentAmmo = gunData.magazineSize;
  }

  public void HandleShoot() {
    nextTimeToFire = Time.time + (1 / gunData.fireRate);
    currentAmmo--;

    Shoot();
    ApplyRecoil();
  }

  public virtual void Shoot() {
    CreateProjectile();
  }

  public virtual void ApplyRecoil() {
    proceduralRecoil.ApplyRecoil();
  }

  public Vector3 GetTargetPoint() {
    Ray ray = cameraView.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

    Vector3 targetPoint;
    if (Physics.Raycast(ray, out RaycastHit hit, gunData.shootingRange, gunData.targetLayerMask)) {
      targetPoint = hit.point;
    } else {
      targetPoint = ray.GetPoint(gunData.shootingRange);
    }

    return targetPoint;
  }

  public Vector3 GetDirectionTarget(Vector3 target) {
    Vector3 directionWithoutSpread = target - attackPoint.position;
    Vector3 directionSpread = new(
      Random.Range(-gunData.projectileData.spread, gunData.projectileData.spread),
      Random.Range(-gunData.projectileData.spread, gunData.projectileData.spread),
      0f
    );
    Vector3 directionWithSpread = directionWithoutSpread - directionSpread;

    return directionWithSpread.normalized;
  }

  public virtual GameObject CreateProjectile() {
    Vector3 direction = GetDirectionTarget(GetTargetPoint());

    return CreateProjectile(direction);
  }

  public virtual GameObject CreateProjectile(Vector3 direction) {
    GameObject projectile = Instantiate(gunData.projectileData.model, attackPoint.position, Quaternion.identity);

    Rigidbody rb = projectile.GetComponent<Rigidbody>();

    projectile.transform.forward = direction;

    rb.AddForce(direction * gunData.projectileData.shootForce, ForceMode.Impulse);
    rb.AddForce(cameraView.transform.up * gunData.projectileData.upwardForce, ForceMode.Impulse);

    return projectile;
  }

  public bool CanReload() {
    return !isReloading && !IsAmmoFull;
  }

  public bool CanShoot() {
    return !isReloading && HasAmmo && IsTimeToFire;
  }
}
