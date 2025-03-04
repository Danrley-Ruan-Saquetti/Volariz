using UnityEngine;
using Volariz.Editor.Inspector.Attributes;
using Volariz.Data;

namespace Volariz.Gameplay.Weapon.Components {
  public class ProceduralRecoil : MonoBehaviour {

    [Header("References")]
    [SerializeField] GunData gunData;

    [Header("State")]
    [SerializeField, Readonly] Vector3 currentRotation = Vector3.zero;
    [SerializeField, Readonly] Vector3 targetRotation = Vector3.zero;
    [SerializeField, Readonly] Vector3 targetPosition = Vector3.zero;
    [SerializeField, Readonly] Vector3 initialGunPosition = Vector3.zero;

    void Start() {
      initialGunPosition = transform.localPosition;
    }

    void Update() {
      UpdateRecoil();

      CalculateNextRotation();
      CalculateNextPosition();

      ResetRotation();
      ResetPosition();
    }

    public void ApplyRecoil() {
      targetPosition += new Vector3(0, 0, -gunData.kickBackZ);

      targetRotation += new Vector3(
        Random.Range(gunData.minRecoil.x, gunData.maxRecoil.x),
        Random.Range(gunData.minRecoil.y, gunData.maxRecoil.y),
        Random.Range(gunData.minRecoil.z, gunData.maxRecoil.z)
      );
    }

    void UpdateRecoil() {
      transform.localRotation = Quaternion.Euler(currentRotation);
    }

    void CalculateNextRotation() {
      currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * gunData.ricochetSpeed);
    }

    void CalculateNextPosition() {
      transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * gunData.ricochetSpeed);
    }

    void ResetRotation() {
      targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * gunData.resetSpeed);
    }

    void ResetPosition() {
      targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * gunData.resetSpeed);
    }
  }
}
