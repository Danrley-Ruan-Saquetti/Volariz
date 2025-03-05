using UnityEngine;

namespace Volariz.Core.Camera {

  public class CameraMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] UnityEngine.Camera viewCamera;

    void Update() {
      transform.rotation = viewCamera.transform.rotation;
    }
  }
}
