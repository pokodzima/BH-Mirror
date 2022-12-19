using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;

namespace UI
{
    public class ScoreText : NetworkBehaviour
    {
        private TextMeshProUGUI _scoreText;
        [SyncVar]private string _scoreTextString;
    
        private Dictionary<string,int> _playerScores = new Dictionary<string, int>();

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isServer)
            {
                var scores =_playerScores.OrderBy(p => p.Value);
                _scoreTextString = "";
                foreach (var score in scores)
                {
                    _scoreTextString += score.Key + ": " + score.Value + "\n";
                }
            }
            
            _scoreText.text = _scoreTextString;
        }
    
        
        public void UploadScore(int score, string playerName)
        {
            if (_playerScores.ContainsKey(playerName))
            {
                _playerScores[playerName] = score;
            }
            else
            {
                _playerScores.Add(playerName,score);
            }
        }
    }
}
