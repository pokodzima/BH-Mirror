using System;
using Interfaces;
using Mirror;
using UnityEngine;

namespace Player
{
    public class DashAttacked : NetworkBehaviour, IDashSubject
    {
        [SerializeField] private float _maxHealth = 3;
        [SerializeField] private float _invincibilityDuration = 3f;
        private float _timeToRemoveInvincibility;


        [SyncVar] private float _currentHealth = 0;

        [SyncVar(hook = nameof(ChangeMeshColor))] private bool _isInvincible = false;
        private MeshColorChanger _meshColorChanger;


        private void Awake()
        {
            _meshColorChanger = GetComponent<MeshColorChanger>();
        }

        private void Update()
        {
            if (isServer)
            {
                if (_isInvincible)
                {
                    if (NetworkTime.time > _timeToRemoveInvincibility)
                    {
                        _timeToRemoveInvincibility = 0;
                        _isInvincible = false;
                    }
                }
            }
        }

        public override void OnStartLocalPlayer()
        {
            CmdResetHealth();
        }

        [Command(requiresAuthority = false)]
        public void OnDashed()
        {
            if (_isInvincible)
            {
                print("Invincible");
                return;
            }

            print($"Dashed {gameObject.name}");
            SubtractHealth();
        }

        [Command]
        public void CmdResetHealth()
        {
            _currentHealth = _maxHealth;
        }


        private void SubtractHealth()
        {
            print("Subtracting health");
            _isInvincible = true;
            _timeToRemoveInvincibility = (float)(NetworkTime.time + _invincibilityDuration);
            _currentHealth--;
            if (_currentHealth <= 0)
            {
                print($"Player {gameObject.name} died");
                return;
            }
        }
        
        private void ChangeMeshColor(bool oldInvincibility, bool newInvincibility)
        {
            _meshColorChanger.CmdChangeColor(newInvincibility);
        }

        
        private void CmdResetInvincibility()
        {
            print("Resetting invincibility");
            _isInvincible = false;
        }
    }
}