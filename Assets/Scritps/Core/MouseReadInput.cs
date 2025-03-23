using UnityEngine;

namespace Volariz.Core {

  public class MouseReadInput : MonoBehaviour {

    public Vector2 mouseEnter;

    void Update() {
      mouseEnter = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
  }
}
