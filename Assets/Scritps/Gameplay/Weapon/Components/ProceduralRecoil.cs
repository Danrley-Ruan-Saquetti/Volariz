using UnityEngine;
using Volariz.Data;
using Volariz.Editor.Inspector.Attributes;

namespace Volariz.Gameplay.Weapon.Components {

  public class ProceduralRecoil : MonoBehaviour {

    [Header("References")]
    [SerializeField] Camera viewPoint;
    [SerializeField] Transform handHolder;
    [SerializeField] GunData gunData;

    [Header("State")]
    [SerializeField, Readonly] Vector3 currentRotation;
    [SerializeField, Readonly] Vector3 targetRotation;
    [SerializeField, Readonly] Vector3 targetPosition;
    Vector3 initialGunPosition = Vector3.zero;

    void Start() {
      initialGunPosition = handHolder.localPosition;
    }

    void Update() {
      UpdateRotation();
      UpdatePosition();
    }

    public void ApplyRecoil() {
      targetPosition += new Vector3(0, 0, -gunData.kickBackZ);

      targetRotation += new Vector3(
        Random.Range(gunData.minRecoil.x, gunData.maxRecoil.x),
        Random.Range(gunData.minRecoil.y, gunData.maxRecoil.y),
        Random.Range(gunData.minRecoil.z, gunData.maxRecoil.z)
      );
    }

    void UpdateRotation() {
      currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.deltaTime * gunData.snappiness);
      targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * gunData.resetAmount);

      handHolder.localRotation = Quaternion.Euler(currentRotation);
      viewPoint.transform.localRotation *= Quaternion.Euler(currentRotation);
    }

    void UpdatePosition() {
      handHolder.localPosition = Vector3.Slerp(handHolder.localPosition, targetPosition, Time.deltaTime * gunData.snappiness);
      targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * gunData.resetAmount);
    }
  }
}
