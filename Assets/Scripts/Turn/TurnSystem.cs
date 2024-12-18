using Events.GameEvents;
using Events.GameEvents.Typed;
using System.Collections;
using System.Collections.Generic;
using UI.PlayerScreen;
using UnityEngine;

namespace Turn
{
    /// <summary>
    /// Holds function for turn system
    /// </summary>
    public class TurnSystem : MonoBehaviour
    {
        [SerializeField] private StringGameEvent setNextTurnEvent;
        [SerializeField] private IntGameEvent onWrongAnswer;
        [SerializeField] private GameEvent onPlayersScoresUpdated;
        private PlayerInfoHolder _playerInfoHolder;
        private List<string> _players = new();
        private int _currentPlayerIndex;
        private int _amountToAdd = 1;

        private void Awake()
        {
            _playerInfoHolder = PlayerInfoHolder.Instance;
            onWrongAnswer.AddListener(UpdatePlayerScores);
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
            setNextTurnEvent.Invoke(_players[_currentPlayerIndex]);
        }

        public void SetTurn()
        {
            StartCoroutine(invokeAfterSeconds(2));
        }

        private void UpdatePlayerScores(int scoreToAdd)
        {
            _playerInfoHolder.PlayerScores[_currentPlayerIndex] += scoreToAdd;
            onPlayersScoresUpdated.Invoke();
        }

        private IEnumerator invokeAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            setNextTurnEvent.Invoke(_players[IndexBoundaryCheck(_currentPlayerIndex + _amountToAdd)]);
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
