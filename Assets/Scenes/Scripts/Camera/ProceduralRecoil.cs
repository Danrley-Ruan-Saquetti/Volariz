using UnityEngine;

public class ProceduralRecoil : MonoBehaviour {

  [SerializeField] Transform cameraTransform;

  [SerializeField] Vector3 recoil = Vector3.zero;
  [SerializeField] float kickBackZ;

  Vector3 currentRotation;
  Vector3 targetRotation;
  Vector3 currentPosition;
  Vector3 targetPosition;
  Vector3 initialGunPosition;

  public float resetAmount;
  public float snappiness;

  void Start() {
    initialGunPosition = transform.localPosition;
  }

  void Update() {
    targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * resetAmount);
    currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * snappiness);

    transform.localRotation = Quaternion.Euler(currentRotation);
    cameraTransform.localRotation = Quaternion.Euler(currentRotation);

    ApplyBackRecoil();
  }

  public void ApplyRecoil() {
    targetPosition -= new Vector3(0, 0, kickBackZ);
    targetRotation -= new Vector3(
      recoil.x,
      Random.Range(-recoil.y, recoil.y),
      Random.Range(-recoil.z, recoil.z)
    );
  }

  void ApplyBackRecoil() {
    targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * resetAmount);

    currentPosition = Vector3.Slerp(currentPosition, targetPosition, Time.fixedDeltaTime * snappiness);
    transform.localPosition = currentPosition;
  }
}
