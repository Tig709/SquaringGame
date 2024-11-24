using Events.GameEvents;
using Events.GameEvents.Typed;
using Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    /// <summary>
    /// Holds card board state and handles functions
    /// </summary>
    public class CardBoardHandler : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameEvent eventToCheckOn;
        [SerializeField] private GameEvent onGridMade;
        [SerializeField] private GameObjectGameEvent emptyTileFound;

        [Header("GridGenerator")]
        [SerializeField] private GridGenerator gridGenerator;

        [Header("Variables")]
        [SerializeField] private int cardRowSize; 

        private List<PlayableTile> _playableTiles = new();
        private List<PlayableTile> _cornerTiles = new();

        private void Awake()
        {
            eventToCheckOn.AddListener(CheckForEmptyTiles);
            onGridMade.AddListener(MakeStartingBoard);
        }

        /// <summary>
        /// When grid is made, gets cornertiles, fills empty tiles and set rotations right
        /// </summary>
        private void MakeStartingBoard()
        {
            _playableTiles = gridGenerator.PlayableTiles;
            int[] cornerIndexes = { 0, cardRowSize - 1, _playableTiles.Count / 2, _playableTiles.Count - cardRowSize, _playableTiles.Count - 1 };
            for (int i = 0; i < cornerIndexes.Length; i++)
                _cornerTiles.Add(_playableTiles[cornerIndexes[i]]);

            CheckForEmptyTiles();
            SetStartRotations();
        }

        //TODO: Should be invoked at wrong played cards later, make check function if stack has cards
        /// <summary>
        /// Checks which tiles are empty and invokes event for this tile
        /// </summary>
        private void CheckForEmptyTiles()
        {
            foreach (PlayableTile playableTile in _playableTiles)
            {
                if (playableTile.GetComponentInChildren<Card>() == null)
                    emptyTileFound.Invoke(playableTile.gameObject);
            }
        }

        /// <summary>
        /// Sets cornertiles open at start
        /// </summary>
        private void SetStartRotations()
        {
            foreach (PlayableTile cornerGridTile in _cornerTiles)
                cornerGridTile.transform.localEulerAngles = new Vector3(180, 0, 0);
        }
    }
}
