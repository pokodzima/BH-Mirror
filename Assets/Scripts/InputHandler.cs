using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class InputHandler : NetworkBehaviour
{
    [SerializeField] private GameObject _inputControllableGameObject;
    private IInputControllableAxis _inputControllableAxis;
    // Start is called before the first frame update
    void Start()
    {
        _inputControllableAxis = _inputControllableGameObject.GetComponent<IInputControllableAxis>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        _inputControllableAxis.GetHorizontalAxis(Input.GetAxis("Horizontal"));
        _inputControllableAxis.GetVerticalAxis(Input.GetAxis("Vertical"));
    }
}
