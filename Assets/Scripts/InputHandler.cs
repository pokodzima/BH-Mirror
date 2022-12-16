using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

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
        _inputControllableAxis.GetHorizontalAxis(Input.GetAxis("Horizontal"));
        _inputControllableAxis.GetVerticalAxis(Input.GetAxis("Vertical"));
        _lmbControllable.SetLMBDown(Input.GetMouseButtonDown(0));
    }
}
