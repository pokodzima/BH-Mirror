using Interfaces;
using Mirror;
using UnityEngine;

namespace Player
{
    public class DashAbility : NetworkBehaviour, ILMBControllable
    {
        [SerializeField] private float _dashDistance;
        [SerializeField] private float _sphereCastRadius = 1f;
        private CharacterController _characterController;
        private PlayerScore _playerScore;
        private IDashSubject _currentDashSubject;

        private bool _dashActive;
        private RaycastHit[] _results;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _playerScore = GetComponent<PlayerScore>();
        }


        void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            if (_dashActive)
            {
                _results = new RaycastHit[4];
                if (Physics.SphereCastNonAlloc(transform.position + transform.forward, _sphereCastRadius,
                        transform.forward, _results,
                        _dashDistance - 1f,
                        ~LayerMask.NameToLayer("Player")) > 0)
                {
                    foreach (var raycastHit in _results)
                    {
                        if (raycastHit.transform != null &&
                            raycastHit.transform.TryGetComponent(out IDashSubject dashSubject) &&
                            raycastHit.transform != transform)
                        {
                            if (dashSubject is ICanBeInvincible invincible)
                            {
                                if (invincible.IsInvincible())
                                {
                                    continue;
                                }

                                {
                                    dashSubject.OnDashed();
                                    _playerScore.RpcAddScore();
                                }
                            }
                            else
                            {
                                dashSubject.OnDashed();
                                _playerScore.RpcAddScore();
                            }
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
}