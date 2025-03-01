using UnityEngine;

public class Pistol : Gun {

  public override void Update() {
    base.Update();

    if (Input.GetKey(KeyCode.R)) {
      TryReload();
    } else if (IsToShoot()) {
      TryShoot();
    }
  }

  public override void Shoot() {
    RaycastHit hit;

    if (Physics.Raycast(viewPoint.transform.position, viewPoint.transform.forward, out hit, gunData.shootingRange, gunData.targetLayerMask)) {

    }
  }

  public bool IsToShoot() {
    return !isSingle ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
  }
}
