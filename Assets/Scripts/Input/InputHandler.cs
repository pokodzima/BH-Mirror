using Interfaces;
using Mirror;
using UnityEngine;

namespace Input
{
    public class InputHandler : NetworkBehaviour
    {
        [SerializeField] private GameObject _inputControllableGameObject;
        [SerializeField] private GameObject _lmbControllableGameObject;
        private IInputControllableAxis _inputControllableAxis;
        private ILMBControllable _lmbControllable;
    
        void Start()
        {
            _inputControllableAxis = _inputControllableGameObject.GetComponent<IInputControllableAxis>();
            _lmbControllable = _lmbControllableGameObject.GetComponent<ILMBControllable>();
        }

    
        void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            _inputControllableAxis.GetHorizontalAxis(UnityEngine.Input.GetAxis("Horizontal"));
            _inputControllableAxis.GetVerticalAxis(UnityEngine.Input.GetAxis("Vertical"));
            _lmbControllable.SetLMBDown(UnityEngine.Input.GetMouseButtonDown(0));
        }
    }
}
