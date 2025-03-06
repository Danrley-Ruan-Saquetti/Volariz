using UnityEngine;
using Volariz.Editor.Inspector.Attributes;
using Volariz.Gameplay.Weapon.Components;

namespace Volariz.Gameplay.Weapon {

  [RequireComponent(typeof(ProceduralRecoil))]
  public class Gun : MonoBehaviour {

    [Header("References")]
    [SerializeField] Camera viewPoint;

    ProceduralRecoil proceduralRecoil;

    [Header("Shoot Settings")]
    [SerializeField] float fireRate = 5f;

    [Header("State")]
    [SerializeField, Readonly] float _nextTimeToFire = 0f;

    public bool IsTimeToFire { get { return Time.time >= _nextTimeToFire; } }

    void Start() {
      proceduralRecoil = GetComponent<ProceduralRecoil>();
    }

    void Update() {
      if (CanFire() && Input.GetButton("Fire1")) {
        HandleShoot();
      }
    }

    void HandleShoot() {
      Shoot();
      ApplyRecoil();
    }

    public void Shoot() {
      _nextTimeToFire = Time.time + (1 / fireRate);

      Debug.Log("Shoot");
    }

    public virtual void ApplyRecoil() {
      proceduralRecoil.ApplyRecoil();
    }

    public bool CanFire() {
      return IsTimeToFire;
    }
  }
}
