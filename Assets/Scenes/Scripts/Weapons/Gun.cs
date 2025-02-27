using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

  public GunData gunData;

  public Transform cameraTransform;

  private float currentAmmo = 0f;

  private float nextTimeToFire = 0f;

  private bool isReloading = false;

  public bool IsAmmoFull {
    get { return currentAmmo == gunData.magazineSize; }
  }

  public bool HasAmmo {
    get { return currentAmmo > 0f; }
  }

  public bool IsTimeToFire {
    get { return Time.time >= nextTimeToFire; }
  }

  void Start() {
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

  private IEnumerator Reload() {
    isReloading = true;

    Debug.Log(gunData.gunName + " reloading");

    yield return new WaitForSeconds(gunData.reloadTime);

    Debug.Log(gunData.gunName + " reloaded");

    isReloading = false;
    currentAmmo = gunData.magazineSize;
  }

  public void HandleShoot() {
    nextTimeToFire = Time.time + (1 / gunData.fireRate);
    currentAmmo--;

    Debug.Log(gunData.gunName + " shoot");

    Shoot();
  }

  public abstract void Shoot();

  public bool CanReload() {
    return !isReloading && HasAmmo && !IsAmmoFull;
  }

  public bool CanShoot() {
    return !isReloading && HasAmmo && IsTimeToFire;
  }
}
