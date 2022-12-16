using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DashSkill : NetworkBehaviour, ILMBControllable
{
    [SerializeField] private float _dashDistance;
    private CharacterController _characterController;

    private bool _dashActive;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (_dashActive)
        {
            _characterController.Move(transform.rotation * new Vector3(0f, 0f, _dashDistance));
        }
    }

    public void SetLMBDown(bool down)
    {
        _dashActive = down;
    }
}