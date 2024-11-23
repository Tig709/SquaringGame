using System;
using Attributes;
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

        private Transform _gridTileTransform;

        public Vector3 PlayableStartPosition => GetPositionFromIndex(gridData.PlayableStartIndex);

        public Vector3 PlayableEndPosition => GetPositionFromIndex(gridData.PlayableEndIndex); 

        private void Awake()
        {
            _gridTileTransform = playableTile.transform;
        }

        private void Start()
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
                z = y * _gridTileTransform.localScale.y + transform.position.z
            };
        }
    }
}