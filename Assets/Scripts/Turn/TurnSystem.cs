using Events.GameEvents.Typed;
using System.Collections.Generic;
using UI.PlayerScreen;
using UnityEngine;

namespace Turn
{
    public class TurnSystem : MonoBehaviour
    {
        [SerializeField] private StringGameEvent nextTurn;
        private PlayerInfoHolder _playerInfoHolder;
        private List<string> _players = new();
        private int _currentPlayerIndex;
        private int _amountToAdd = 1;

        private void Awake()
        {
            _playerInfoHolder = GetComponent<PlayerInfoHolder>();
        }

        private void Start()
        {
            _players = _playerInfoHolder.PlayerNames;
            SetRandomStartingPlayer();
            StartGame();
        }

        private void SetRandomStartingPlayer()
        {
            _currentPlayerIndex = Random.Range(0, _players.Count);
        }

        private void StartGame()
        {
            nextTurn.Invoke(_players[_currentPlayerIndex]);
        }

        public void SetTurn()
        {
            nextTurn.Invoke(_players[IndexBoundaryCheck(_currentPlayerIndex + _amountToAdd)]);
        }

        /// <summary>
        /// If wanted player index is bigger then amount of players, set right index, else do nothing
        /// </summary>
        /// <param name="indexToCheck"></param>
        /// <returns></returns>
        private int IndexBoundaryCheck(int indexToCheck)
        {
            while (indexToCheck >= _players.Count)
            {
                indexToCheck -= _players.Count;
            }
            return indexToCheck;
        }
    }
}
