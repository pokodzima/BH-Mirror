using System;
using Interfaces;
using Mirror;
using UnityEngine;

namespace Player
{
    public class DashAttacked : NetworkBehaviour, IDashSubject, ICanBeInvincible
    {
        [SerializeField] private float _invincibilityDuration = 3f;
        private float _timeToRemoveInvincibility;

        [SyncVar(hook = nameof(ChangeMeshColor))]
        private bool _isInvincible = false;

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

        [Command(requiresAuthority = false)]
        public void OnDashed()
        {
            if (_isInvincible)
            {
                print("Invincible");
                return;
            }

            print($"Dashed {gameObject.name}");
            SetInvisible();
        }


        private void SetInvisible()
        {
            print("Set invisible");
            _isInvincible = true;
            _timeToRemoveInvincibility = (float)(NetworkTime.time + _invincibilityDuration);
        }

        private void ChangeMeshColor(bool oldInvincibility, bool newInvincibility)
        {
            _meshColorChanger.CmdChangeColor(newInvincibility);
        }

        public bool IsInvincible()
        {
            return _isInvincible;
        }
    }
}