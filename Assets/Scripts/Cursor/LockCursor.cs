using UnityEngine;

namespace Cursor
{
    public class LockCursor : MonoBehaviour
    {
        private void OnApplicationFocus(bool hasFocus)
        {
            UnityEngine.Cursor.lockState = hasFocus ? CursorLockMode.Confined : CursorLockMode.None;
        }
    }
}
