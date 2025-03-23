using UnityEngine;
using Volariz.Data;
using Volariz.Editor.Inspector.Attributes;
using Volariz.Gameplay.Weapon.Components;

namespace Volariz.Gameplay.Weapon {

  [RequireComponent(typeof(ProceduralRecoil))]
  public class Gun : MonoBehaviour {

    [Header("References")]
    [SerializeField] Camera viewPoint;
    [SerializeField] protected GunData gunData;
    public Transform pointOriginShot;

    ProceduralRecoil proceduralRecoil;

    [Header("State")]
    [SerializeField, Readonly] float _nextTimeToFire;

    void Start() {
      proceduralRecoil = GetComponent<ProceduralRecoil>();
    }

    void Update() {
      if (CanFire() && Input.GetButton("Fire1")) {
        Shoot();
      }
    }

    protected virtual void Shoot() {
      LoadNextTimeToFire();
      CreateProjectile();
      ApplyRecoil();
    }

    protected virtual void LoadNextTimeToFire() {
      _nextTimeToFire = Time.time + (1 / gunData.fireRate);
    }

    protected virtual void ApplyRecoil() {
      proceduralRecoil.ApplyRecoil();
    }

    protected virtual GameObject CreateProjectile() {
      Vector3 direction = GetDirectionTarget(GetTargetPoint());

      return CreateProjectile(direction);
    }

    protected Vector3 GetTargetPoint() {
      Ray ray = viewPoint.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

      Vector3 targetPoint;
      if (Physics.Raycast(ray, out RaycastHit hit, gunData.shootingRange, gunData.targetLayerMask)) {
        targetPoint = hit.point;
      } else {
        targetPoint = ray.GetPoint(gunData.shootingRange);
      }

      return targetPoint;
    }

    protected Vector3 GetDirectionTarget(Vector3 target) {
      Vector3 directionWithoutSpread = target - pointOriginShot.position;
      Vector3 directionSpread = new(
        Random.Range(-gunData.projectileData.spread, gunData.projectileData.spread),
        Random.Range(-gunData.projectileData.spread, gunData.projectileData.spread),
        0f
      );
      Vector3 directionWithSpread = directionWithoutSpread - directionSpread;

      return directionWithSpread.normalized;
    }

    protected virtual GameObject CreateProjectile(Vector3 direction) {
      GameObject projectileGameObject = Instantiate(gunData.projectileData.model, pointOriginShot.position, Quaternion.identity);

      Projectile projectile = projectileGameObject.GetComponent<Projectile>();

      projectile.direction = direction;
      projectile.shootForce = direction * gunData.projectileData.shootForce;
      projectile.upwardForce = viewPoint.transform.up * gunData.projectileData.upwardForce;

      return projectileGameObject;
    }

    public bool CanFire() {
      return Time.time >= _nextTimeToFire;
    }
  }
}
