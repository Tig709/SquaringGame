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
        [SerializeField] private GameEvent onInbetweenButtonClicked;
        [SerializeField] private GameEvent onOutsideButtonClicked;
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
            onInbetweenButtonClicked.AddListener(CheckInbetween);
            onOutsideButtonClicked.AddListener(CheckOutside);
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

                    card.NeighbourCount = neighbourCount;
                }
            }
        }

        /// <summary>
        /// Checks if a neighboring tile exists in the given direction and returns its InteractableCard component.
        /// </summary>
        /// <param name="cardGrid">2D array of cached InteractableCard components.</param>
        /// <param name="row">Row index of the neighboring tile.</param>
        /// <param name="column">Column index of the neighboring tile.</param>
        /// <returns>The neighboring tile's InteractableCard component, or null if out of bounds or empty.</returns>
        private InteractableCard CheckDirection(InteractableCard[,] cardGrid, int row, int column)
        {
            if (row < 0 || row >= cardRowSize || column < 0 || column >= cardRowSize)
            {
                return null;
            }

            return cardGrid[row, column];
        }

        private void SaveClickedCard(GameObject card)
        {
            _clickedCard = card;
        }

        private void CheckHigher()
        {
            CardComparison(true);
        }

        private void CheckLower()
        {
            CardComparison(false);
        }

        private void CheckInbetween()
        {
            CardComparison(false);
        }

        private void CheckOutside()
        {
            CardComparison(true);
        }

        private void CardComparison(bool isHigher)
        {
            List<int> neighbourCardValues = new();

            int clickedCardValue = GetCardValue(GetCardIndex(_clickedCard));

            foreach (int i in GetNeighbours(GetCardIndex(_clickedCard)))
            {
                neighbourCardValues.Add(GetCardValue(i));
            }

                //TODO call interactablecard.turnaround
            if (neighbourCardValues.Count == 1)
            {
                if ((isHigher && neighbourCardValues[0] <= clickedCardValue) || (!isHigher && neighbourCardValues[0] >= clickedCardValue))
                    Debug.Log("Right answer HL");

                else
                    Debug.Log("Wrong answer HL");
            }
            else
            {
                List<int> neigboursToCheck = new() { neighbourCardValues[0], neighbourCardValues[1] };
                if (neighbourCardValues.Count == 3)
                {
                    int differenceAB = Mathf.Abs(neighbourCardValues[0] - neighbourCardValues[1]);
                    int differenceAC = Mathf.Abs(neighbourCardValues[0] - neighbourCardValues[2]);
                    int differenceBC = Mathf.Abs(neighbourCardValues[1] - neighbourCardValues[2]);

                    int maxDiff = differenceAB;
                    List<int> maxPairValues = new() { neighbourCardValues[0], neighbourCardValues[1] };

                    int minDiff = differenceAB;
                    List<int> minPairValues = new() { neighbourCardValues[0], neighbourCardValues[1] };

                    if (differenceAC > maxDiff)
                    {
                        maxDiff = differenceAC;
                        maxPairValues = new List<int> { neighbourCardValues[0], neighbourCardValues[2] };
                    }
                    if (differenceBC > maxDiff)
                    {
                        maxDiff = differenceBC;
                        maxPairValues = new List<int> { neighbourCardValues[1], neighbourCardValues[2] };
                    }

                    if (differenceAC < minDiff)
                    {
                        minDiff = differenceAC;
                        minPairValues = new List<int> { neighbourCardValues[0], neighbourCardValues[2] };
                    }
                    if (differenceBC < minDiff)
                    {
                        minDiff = differenceBC;
                        minPairValues = new List<int> { neighbourCardValues[1], neighbourCardValues[2] };
                    }

                    neigboursToCheck = maxPairValues;
                    if (maxDiff > minDiff)
                        neigboursToCheck = minPairValues;
                }

                if (isHigher && clickedCardValue >= Mathf.Max(neigboursToCheck[0], neigboursToCheck[1]) || isHigher && clickedCardValue <= Mathf.Min(neigboursToCheck[0], neigboursToCheck[1]) || !isHigher && clickedCardValue <= Mathf.Max(neigboursToCheck[0], neigboursToCheck[1]) || !isHigher && clickedCardValue >= Mathf.Min(neigboursToCheck[0], neigboursToCheck[1]))
                    Debug.Log("right answer IO");
                else
                    Debug.Log("Wrong answer IO");
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

        private List<int> GetNeighbours(int cardIndex)
        {
            List<int> neighbours = new();

            int row = cardIndex / cardRowSize;
            int column = cardIndex % cardRowSize;

            foreach (var direction in Directions)
            {
                int neighbourRow = row + direction.x;
                int neighbourCol = column + direction.y;

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
