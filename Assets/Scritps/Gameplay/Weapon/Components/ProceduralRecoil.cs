using UnityEngine;

namespace Volariz.Gameplay.Weapon.Components {

  public class ProceduralRecoil : MonoBehaviour {

    [Header("References")]
    [SerializeField] Camera viewPoint;
    [SerializeField] Transform handHolder;

    [Header("Recoil Settings")]
    [SerializeField] Vector3 minRecoil;
    [SerializeField] Vector3 maxRecoil;

    void Update() {

    }

    public void ApplyRecoil() {
      Vector3 targetPosition = new Vector3(
        Random.Range(minRecoil.x, maxRecoil.x),
        Random.Range(minRecoil.y, maxRecoil.y),
        Random.Range(minRecoil.z, maxRecoil.z)
      );

      viewPoint.transform.position += targetPosition;
    }
  }
}
