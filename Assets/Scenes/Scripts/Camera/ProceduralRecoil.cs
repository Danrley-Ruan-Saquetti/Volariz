using UnityEngine;

public class ProceduralRecoil : MonoBehaviour {

  [SerializeField] GunData gunData;
  [SerializeField] ViewPointCamera viewPoint;

  Vector3 currentRotation = Vector3.zero;
  Vector3 targetRotation = Vector3.zero;
  Vector3 targetPosition = Vector3.zero;
  Vector3 initialGunPosition = Vector3.zero;

  void Start() {
    initialGunPosition = transform.localPosition;
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

    transform.localRotation = Quaternion.Euler(currentRotation);
    viewPoint.transform.localRotation = Quaternion.Euler(currentRotation);
  }

  void UpdatePosition() {
    transform.localPosition = Vector3.Slerp(transform.localPosition, targetPosition, Time.deltaTime * gunData.snappiness);
    targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * gunData.resetAmount);
  }
}
