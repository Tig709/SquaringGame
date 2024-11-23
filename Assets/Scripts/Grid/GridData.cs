using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    /// <summary>
    /// Holds grid data and marks playable and notUsed tiles
    /// </summary>
    [CreateAssetMenu(fileName = "GridData", menuName = "Grid/GridData", order = 0)]
    public class GridData : ScriptableObject
    {
        private readonly IReadOnlyList<Vector2Int> _directions = new[]
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };

        [Tooltip("The indices of all playable cells for a card game")] 
        [SerializeField] private List<Vector2Int> playableIndices;
        
        [Header("Grid Settings")]
        [Tooltip("The size of a grid")] 
        [SerializeField] private int xGridSize;
        [SerializeField] private int yGridSize;

        public GridTileTypes[,] GeneratedGrid { get; private set; }

        public int XGridSize => xGridSize;

        public int YGridSize => yGridSize;

        public Vector2Int PlayableStartIndex => playableIndices[0]; 

        public Vector2Int PlayableEndIndex => playableIndices[^1]; 

        private void OnValidate()
        {
            GeneratedGrid = new GridTileTypes[xGridSize, yGridSize];

            MarkPlayable();
            MarkUnused();
        }

        // Marks all playable coordinates as playable
        private void MarkPlayable()
        {
            for (var i = 0; i < xGridSize; i++)
            {
                for (var j = 0; j < yGridSize; j++)
                {
                    if (playableIndices.Contains(new Vector2Int(i, j)))
                    {
                        GeneratedGrid[i, j] = GridTileTypes.Playable;
                    }
                }
            }
        }

        // Marks cells around the playable cells (which aren't paths) as unused
        private void MarkUnused()
        {
            foreach (var playableIndex in playableIndices)
            {
                foreach (var direction in _directions)
                {
                    var tileToCheck = playableIndex + direction;

                    if (
                        tileToCheck.x < 0 ||
                        tileToCheck.x > xGridSize - 1 ||
                        tileToCheck.y < 0 ||
                        tileToCheck.y > yGridSize - 1
                    ) continue;

                    if (GeneratedGrid[tileToCheck.x, tileToCheck.y] != GridTileTypes.Playable)
                    {
                        GeneratedGrid[tileToCheck.x, tileToCheck.y] = GridTileTypes.NotUsed;
                    }
                }
            }
        }
    }
}