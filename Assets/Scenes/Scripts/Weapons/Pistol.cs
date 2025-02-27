using UnityEngine;

public class Pistol : Gun {

  public override void Update() {
    base.Update();

    if (Input.GetKey(KeyCode.R)) {
      TryReload();
    } else if (Input.GetButtonDown("Fire1")) {
      TryShoot();
    }
  }

  public override void Shoot() {
    RaycastHit hit;

    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange, gunData.targetLayerMask)) {
      Debug.Log(gunData.name + " hit " + hit.collider.name);
    }
  }
}
