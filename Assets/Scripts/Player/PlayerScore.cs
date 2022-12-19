using Mirror;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerScore : NetworkBehaviour
    {
        [SyncVar] private int _score = 0;
        private static readonly int ScoreToWin = 3;
    
        [Command]
        public void CmdAddScore()
        {
            _score++;
            if (_score == ScoreToWin)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
