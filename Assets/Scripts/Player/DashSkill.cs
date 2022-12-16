using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Mirror;
using UnityEngine;

public class DashSkill : NetworkBehaviour, ILMBControllable
{
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _sphereCastRadius = 1f;
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
            RaycastHit[] results = new RaycastHit[4];
            if (Physics.SphereCastNonAlloc(transform.position, _sphereCastRadius, transform.forward, results, _dashDistance,
                    ~LayerMask.NameToLayer("Player")) > 0)
            {
                print("SphereCast Worked");
                foreach (var raycastHit in results)
                {
                    if (raycastHit.transform != null && raycastHit.transform.TryGetComponent(out IDashSubject dashSubject))
                    {
                        dashSubject.OnDashed();
                    }
                }
            }

            _characterController.Move(transform.rotation * new Vector3(0f, 0f, _dashDistance));
        }
    }

    public void SetLMBDown(bool down)
    {
        _dashActive = down;
    }
}