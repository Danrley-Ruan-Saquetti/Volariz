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

    ProceduralRecoil proceduralRecoil;

    [Header("State")]
    [SerializeField, Readonly] float _nextTimeToFire;

    void Start() {
      proceduralRecoil = GetComponent<ProceduralRecoil>();
    }

    void Update() {
      if (CanFire() && Input.GetButton("Fire1")) {
        HandleShoot();
      }
    }

    void HandleShoot() {
      _nextTimeToFire = Time.time + (1 / gunData.fireRate);

      Shoot();
      ApplyRecoil();
    }

    public virtual void Shoot() { }

    public virtual void ApplyRecoil() {
      proceduralRecoil.ApplyRecoil();
    }

    public bool CanFire() {
      return Time.time >= _nextTimeToFire;
    }
  }
}
