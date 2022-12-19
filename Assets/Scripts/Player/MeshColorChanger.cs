using System;
using Mirror;
using UnityEngine;

namespace Player
{
    public class MeshColorChanger : NetworkBehaviour
    {
        [SerializeField] private MeshRenderer[] _meshRenderers;
        private Color[] _startColors;
        [SyncVar] private bool _isRed = false;

        private void Awake()
        {
            _startColors = new Color[_meshRenderers.Length];
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _startColors[i] = _meshRenderers[i].material.color;
            }
        }
        
        [Command]
        public void CmdChangeColor(bool IsInvisible)
        {
            _isRed = IsInvisible;
            RpcChangeColor();
        }
        
        
        
        [ClientRpc]
        private void RpcChangeColor()
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material.color = _isRed ? Color.red : _startColors[i];
            }
        }
    }
}