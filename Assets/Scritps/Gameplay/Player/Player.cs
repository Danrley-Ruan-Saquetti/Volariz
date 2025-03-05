using UnityEngine;

namespace Volariz.Gameplay.Player {
  public class Player : MonoBehaviour {

    void Start() {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }
}
