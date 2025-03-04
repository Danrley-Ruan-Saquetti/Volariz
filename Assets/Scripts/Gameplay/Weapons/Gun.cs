using System.Collections;
using UnityEngine;
using Volariz.Editor.Inspector.Attributes;
using Volariz.Gameplay.Weapon.Components;
using Volariz.Gameplay.Weapon.Projectiles;
using Volariz.Data;

namespace Volariz.Gameplay.Weapon {

  [RequireComponent(typeof(ProceduralRecoil))]
  public abstract class Gun : MonoBehaviour {

    [Header("References")]
    public Camera viewPoint;
    public GunData gunData;
    public Transform attackPoint;

    protected ProceduralRecoil proceduralRecoil;

    [Header("State")]
    [SerializeField, Readonly] int currentAmmo = 0;
    [SerializeField, Readonly] float nextTimeToFire = 0f;
    [SerializeField, Readonly] bool isReloading = false;

    public bool isSingle;

    public bool IsAmmoFull { get { return currentAmmo == gunData.magazineSize; } }
    public bool HasAmmo { get { return currentAmmo > 0f; } }
    public bool IsTimeToFire { get { return Time.time >= nextTimeToFire; } }

    public virtual void Awake() {
      currentAmmo = gunData.magazineSize;
      isSingle = gunData.isSingle;
    }

    public virtual void Start() {
      proceduralRecoil = GetComponent<ProceduralRecoil>();
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

    public virtual GameObject CreateProjectile() {
      Vector3 direction = GetDirectionTarget(GetTargetPoint());

      return CreateProjectile(direction);
    }

    public Vector3 GetTargetPoint() {
      Ray ray = viewPoint.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

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

    public virtual GameObject CreateProjectile(Vector3 direction) {
      GameObject projectileGameObject = Instantiate(gunData.projectileData.model, attackPoint.position, Quaternion.identity);

      Projectile projectile = projectileGameObject.GetComponent<Projectile>();

      projectile.direction = direction;
      projectile.shootForce = direction * gunData.projectileData.shootForce;
      projectile.upwardForce = viewPoint.transform.up * gunData.projectileData.upwardForce;

      return projectileGameObject;
    }

    public bool CanReload() {
      return !isReloading && !IsAmmoFull;
    }

    public bool CanShoot() {
      return !isReloading && HasAmmo && IsTimeToFire;
    }
  }
}
