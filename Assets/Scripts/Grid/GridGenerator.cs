using System;
using System.Collections.Generic;
using Attributes;
using Events.GameEvents;
using UnityEngine;

namespace Grid
{
    /// <summary>
    /// Used to quickly generate a grid 
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        [Header("Tile Prefabs")]
        [SerializeField] private PlayableTile playableTile;
        [SerializeField] private NotUsedTile notUsedTile;

        [Header("Board Design")]
        [SerializeField, Expandable] private GridData gridData;

        [Header("Events")]
        [SerializeField] private GameEvent OnGridMade;

        private Transform _gridTileTransform;

        private List<GameObject> _gridTiles = new List<GameObject>();
        private List<PlayableTile> _playableTiles = new List<PlayableTile>();
        private bool _buildGrid = true;
        public List<GameObject> GridTiles => _gridTiles;
        public List<PlayableTile> PlayableTiles => _playableTiles;

        public Vector3 PlayableStartPosition => GetPositionFromIndex(gridData.PlayableStartIndex);

        public Vector3 PlayableEndPosition => GetPositionFromIndex(gridData.PlayableEndIndex);

        private void Awake()
        {
            _gridTileTransform = playableTile.transform;
        }

        private void Update()
        {
            if (!_buildGrid || !gridData.Start)
                return;

            _buildGrid = false;
            CreateGrid();
        }

        private void CreateGrid()
        {
            // Instantiates the right grid tiles at the positions defined in gridData,
            // determines start and end position of playables
            for (var i = 0; i < gridData.XGridSize; i++)
            {
                for (var j = 0; j < gridData.YGridSize; j++)
                {
                    switch (gridData.GeneratedGrid[i, j])
                    {
                        case GridTileTypes.Playable:
                            CreateTile(i, j, playableTile);
                            break;
                        case GridTileTypes.NotUsed:
                            CreateTile(i, j, notUsedTile);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            foreach (Transform child in transform)
            {
                _gridTiles.Add(child.gameObject);
                if (child.gameObject.GetComponent<PlayableTile>() != null)
                    _playableTiles.Add(child.gameObject.GetComponent<PlayableTile>());
            }
            OnGridMade.Invoke();
        }

        private void CreateTile(int i, int j, MonoBehaviour tile)
        {
            Instantiate(tile, GetPositionFromIndex(i, j), Quaternion.identity, transform);
        }

        private Vector3 GetPositionFromIndex(Vector2Int index)
        {
            return GetPositionFromIndex(index.x, index.y);
        }

        private Vector3 GetPositionFromIndex(int x, int y)
        {
            return new Vector3
            {
                x = x * _gridTileTransform.localScale.x + transform.position.x,
                y = 0,
                z = y * _gridTileTransform.localScale.z + transform.position.z
            };
        }
    }
}