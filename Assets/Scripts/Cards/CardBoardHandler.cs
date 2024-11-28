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
        [SerializeField] private GameEvent onHigherButtonClicked;
        [SerializeField] private GameEvent onLowerButtonClicked;
        [SerializeField] private GameObjectGameEvent emptyTileFound;
        [SerializeField] private GameObjectGameEvent onCardClicked;

        [Header("GridGenerator")]
        [SerializeField] private GridGenerator gridGenerator;

        [Header("Variables")]
        [SerializeField] private int cardRowSize;

        private List<PlayableTile> _playableTiles = new();
        private List<PlayableTile> _cornerTiles = new();
        private List<List<GameObject>> _tiles = new();
        private static readonly Vector2Int[] Directions = { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) }; // Up, Down, Left, Right
        private GameObject _clickedCard;

        private void Awake()
        {
            eventToCheckOn.AddListener(CheckForEmptyTiles);
            onGridMade.AddListener(MakeStartingBoard);
            onCardClicked.AddListener(SaveClickedCard);
            onHigherButtonClicked.AddListener(CheckHigher);
            onLowerButtonClicked.AddListener(CheckLower);
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

            for (int i = 0; i < cardRowSize; i++)
            {
                _tiles.Add(new List<GameObject>());
                for (int j = 0; j < cardRowSize; j++)
                {
                    _tiles[i].Add(gridGenerator.GridTiles[i * cardRowSize + j]);
                }
            }

            CheckForEmptyTiles();
            SetStartRotations();
            CanCardBeTurned();
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
            {
                cornerGridTile.transform.localEulerAngles = new Vector3(180, 0, 0);
                cornerGridTile.GetComponentInChildren<InteractableCard>().TurnedAround = true;
            }
        }

        //TODO, invoke after every card played!
        /// <summary>
        /// Checks if card can be turned by checking if it has at least 1 neighbour
        /// </summary>
        private void CanCardBeTurned()
        {
            // Cache InteractableCard components for all tiles
            InteractableCard[,] cardGrid = new InteractableCard[cardRowSize, cardRowSize];
            for (int i = 0; i < cardRowSize; i++)
            {
                for (int j = 0; j < cardRowSize; j++)
                {
                    cardGrid[i, j] = _tiles[i][j].GetComponentInChildren<InteractableCard>();
                }
            }

            //CheckNeighbours
            for (int i = 0; i < cardRowSize; i++)
            {
                for (int j = 0; j < cardRowSize; j++)
                {
                    InteractableCard card = cardGrid[i, j];
                    if (card == null)
                        continue;

                    int neighbourCount = 0;

                    foreach (var direction in Directions)
                    {
                        InteractableCard neighbourCard = CheckDirection(cardGrid, i + direction.x, j + direction.y);
                        if (neighbourCard != null && neighbourCard.TurnedAround)
                            neighbourCount++;
                    }

                    card.HasNeighbour = neighbourCount >= 1; //TODO For now, change later for 2 or more active neigbours (change to int, hasNeigbour to amountNeighbour
                }
            }
        }

        /// <summary>
        /// Checks if a neighboring tile exists in the given direction and returns its InteractableCard component.
        /// </summary>
        /// <param name="cardGrid">2D array of cached InteractableCard components.</param>
        /// <param name="row">Row index of the neighboring tile.</param>
        /// <param name="col">Column index of the neighboring tile.</param>
        /// <returns>The neighboring tile's InteractableCard component, or null if out of bounds or empty.</returns>
        private InteractableCard CheckDirection(InteractableCard[,] cardGrid, int row, int col)
        {
            if (row < 0 || row >= cardRowSize || col < 0 || col >= cardRowSize)
            {
                return null;
            }

            return cardGrid[row, col];
        }

        private void SaveClickedCard(GameObject card)
        {
            _clickedCard = card;
        }

        //Make func for gettingneighbours because is written twice now (also above)
        private void CheckHigher()
        {
            CardComparison(true);
        }

        private void CheckLower()
        {
            CardComparison(false);
        }

        private void CardComparison(bool isHigher)
        {
            int i = GetCardIndex(_clickedCard);
            List<int> neighbours = GetNeighbours(i);
            int cardVal = GetCardValue(i);
            List<int> nCardVals = new List<int>();
            foreach (int j in neighbours)
            {
                nCardVals.Add(GetCardValue(j));
            }
            if (nCardVals.Count == 1)
            {
                if ((isHigher && nCardVals[0] <= cardVal) || (!isHigher && nCardVals[0] >= cardVal))
                    Debug.Log("Right aNswer");

                else
                    Debug.Log("Wrong aNswer");
            }
            else
            {
                Debug.Log("Multiple N");//TODO will be checked on earlier and seperated in other functions,atm there are checks if a card has multiple variabls. function for inbetween outside H/L seperated, inbetween outside knoppen maken
            }
        }
        private int GetCardIndex(GameObject card)
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                for (int j = 0; j < _tiles[i].Count; j++)
                {
                    if (_tiles[i][j].GetComponentInChildren<Card>() != null && card == _tiles[i][j].GetComponentInChildren<Card>().gameObject)
                    {
                        return i * cardRowSize + j;
                    }
                }
            }
            return 0;
        }

        private int GetCardValue(int cardIndex)
        {
            Card card = _tiles[cardIndex / cardRowSize][cardIndex % cardRowSize].GetComponentInChildren<Card>();

            return card.Value;
        }

        private List<int> GetNeighbours(int i)
        {
            List<int> neighbours = new List<int>();

            int row = i / cardRowSize;
            int col = i % cardRowSize;

            foreach (var direction in Directions)
            {
                int neighbourRow = row + direction.x;
                int neighbourCol = col + direction.y;

                if (neighbourRow >= 0 && neighbourRow < cardRowSize && neighbourCol >= 0 && neighbourCol < cardRowSize)
                {
                    InteractableCard neighbourCard = _tiles[neighbourRow][neighbourCol].GetComponentInChildren<InteractableCard>();

                    if (neighbourCard != null && neighbourCard.TurnedAround)
                        neighbours.Add(neighbourRow * cardRowSize + neighbourCol);
                }
            }

            return neighbours;
        }
    }
}
