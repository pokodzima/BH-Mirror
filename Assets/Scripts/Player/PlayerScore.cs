using System;
using Mirror;
using UI;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerScore : NetworkBehaviour
    {
        [SyncVar] private int _score = 0;
        private static readonly int ScoreToWin = 3;
        private ScoreText _scoreText;

        public override void OnStartLocalPlayer()
        {
            print("OnStartLocalPlayer");
            UploadScore();
        }
        
        

        
        private void UploadScore()
        {
            if (_scoreText != null)
            {
                _scoreText.UploadScore(_score, $"Player {netId}");
                print("UploadScore");
            }
            else
            {
                _scoreText = FindObjectOfType<ScoreText>();
                UploadScore();
            }
        }

        [Command]
        public void RpcAddScore()
        {
            _score++;
            UploadScore();
        }
    }
}