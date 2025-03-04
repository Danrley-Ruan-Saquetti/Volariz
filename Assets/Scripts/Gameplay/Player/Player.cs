using UnityEngine;

namespace Volariz.Gameplay.Player {

  [RequireComponent(typeof(PlayerMovement))]
  public class Player : MonoBehaviour {

    void Start() {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }
}
