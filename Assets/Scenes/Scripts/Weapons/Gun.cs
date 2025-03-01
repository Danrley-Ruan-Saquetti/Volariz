using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ProceduralRecoil))]
public abstract class Gun : MonoBehaviour {

  public GunData gunData;

  public Transform cameraTransform;

  protected ProceduralRecoil proceduralRecoil;

  int currentAmmo = 0;
  float nextTimeToFire = 0f;

  bool isReloading = false;

  public bool IsAmmoFull {
    get { return currentAmmo == gunData.magazineSize; }
  }

  public bool HasAmmo {
    get { return currentAmmo > 0f; }
  }

  public bool IsTimeToFire {
    get { return Time.time >= nextTimeToFire; }
  }

  public virtual void Start() {
    proceduralRecoil = GetComponent<ProceduralRecoil>();
    currentAmmo = gunData.magazineSize;
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

  public abstract void Shoot();

  public virtual void ApplyRecoil() {
    proceduralRecoil.ApplyRecoil();
  }

  public bool CanReload() {
    return !isReloading && !IsAmmoFull;
  }

  public bool CanShoot() {
    return !isReloading && HasAmmo && IsTimeToFire;
  }
}
