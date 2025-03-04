using UnityEngine;

namespace Volariz.Gameplay.Weapon.Type {
  public class Pistol : Gun {

    public override void Update() {
      base.Update();

      if (Input.GetKey(KeyCode.R)) {
        TryReload();
      } else if (IsToShoot()) {
        TryShoot();
      }
    }

    public bool IsToShoot() {
      return !isSingle ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
    }
  }
}
