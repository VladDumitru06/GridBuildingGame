﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    #region Members
    [SerializeField] private Vector2 _endPosition;
    [SerializeField] private int _cellBlockSize;
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private GameObject _cellBlockPrefab;

    private bool isCellSelected = false;
    private CellBlock FirstSelectedCell;
    private CellBlock SecondSelectedCell;
    /// <summary>
    /// Prefab used for creating a single floor tile from multiple
    /// </summary>
    [SerializeField] private GameObject _roomFloorPrefab;
    /// <summary>
    /// All the cell blocks
    /// </summary>
    private List<CellBlock> _cellList;
    #endregion
    #region Unity Methods
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (FirstSelectedCell != SecondSelectedCell)
            {
                CreateGridFromSelectedCells(FirstSelectedCell, SecondSelectedCell);
            }
        }
    }
    private void Awake()
    {
        CreateGrid(_startPosition,_endPosition,Color.red,0.1f);
        //SetPlane(_gridSize, _startPosition);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Instantiates each Cell Block at it's respective positions and adds them to _cellList
    /// </summary>
    public void CreateGrid(Vector2 startPosition, Vector2 endPosition,Color color,float height)
    {
        _cellList = new List<CellBlock>();
        for (int i = 0; i <= endPosition.x - startPosition.x ; i++)
        {
            for (int j = 0; j <= endPosition.y - startPosition.y; j++)
            {

                GameObject _TempGOCellBlock = Instantiate(_cellBlockPrefab, new Vector3(i * _cellBlockSize, height, j * _cellBlockSize)+ new Vector3(startPosition.x,0f, startPosition.y), new Quaternion(0f, 0f, 0f, 0f));
                CellBlock _tempCellBlock = _TempGOCellBlock.GetComponent<CellBlock>();
                _tempCellBlock.Floor = _TempGOCellBlock;
                _tempCellBlock.Floor.name = "Floor X:" + (i - 0).ToString() + " Y: " + (j - 0).ToString();
                _tempCellBlock.Floor.GetComponent<MeshRenderer>().material.color = color;
                _tempCellBlock.gridController = this;
                _cellList.Add(_tempCellBlock);
            }
        }
    }
    public void SelectCell(CellBlock Cell)
    {
        if (!isCellSelected)
        {
            FirstSelectedCell = Cell;
            SecondSelectedCell = Cell;
            isCellSelected = true;
        }
        else
        {
            SecondSelectedCell = Cell;
            isCellSelected = false;
        }

    }
    private void CreateGridFromSelectedCells(CellBlock firstCell, CellBlock secondCell)
    {
        if (firstCell.Position.x < secondCell.Position.x && firstCell.Position.y < secondCell.Position.y)
        {
            CreateGrid(firstCell.Position, secondCell.Position, Color.cyan, .11f);
        }
        if (firstCell.Position.x == secondCell.Position.x && firstCell.Position.y < secondCell.Position.y)
        {
            CreateGrid(new Vector2(firstCell.Position.x, firstCell.Position.y), new Vector2(secondCell.Position.x, secondCell.Position.y), Color.cyan, .11f);
        }
        if (firstCell.Position.x < secondCell.Position.x && firstCell.Position.y == secondCell.Position.y)
        {
            CreateGrid(new Vector2(firstCell.Position.x, firstCell.Position.y), new Vector2(secondCell.Position.x, secondCell.Position.y), Color.cyan, .11f);
        }
        if (firstCell.Position.x == secondCell.Position.x && firstCell.Position.y > secondCell.Position.y)
        {
            CreateGrid(new Vector2(firstCell.Position.x, secondCell.Position.y), new Vector2(secondCell.Position.x, firstCell.Position.y), Color.cyan, .11f);
        }
        if (firstCell.Position.x > secondCell.Position.x && firstCell.Position.y == secondCell.Position.y)
        {
            CreateGrid(new Vector2(secondCell.Position.x, firstCell.Position.y), new Vector2(firstCell.Position.x, secondCell.Position.y), Color.cyan, .11f);
        }
        if (firstCell.Position.x > secondCell.Position.x && firstCell.Position.y < secondCell.Position.y)
        {
            CreateGrid(new Vector2(secondCell.Position.x,firstCell.Position.y), new Vector2(firstCell.Position.x, secondCell.Position.y), Color.cyan, .11f);
        }
        if (firstCell.Position.x < secondCell.Position.x && firstCell.Position.y > secondCell.Position.y)
        {
            CreateGrid(new Vector2(firstCell.Position.x, secondCell.Position.y), new Vector2(secondCell.Position.x, firstCell.Position.y), Color.cyan, .11f);
        }
        if (firstCell.Position.x > secondCell.Position.x && firstCell.Position.y > secondCell.Position.y)
        {
            CreateGrid(new Vector2(secondCell.Position.x, secondCell.Position.y), new Vector2(firstCell.Position.x, firstCell.Position.y), Color.cyan, .11f);
        }
    }
    /// <summary>
    /// Set's the plane to the size of the grid
    /// </summary>
    private void SetPlane(Vector2 gridsize, Vector2 startPosition)
    {
        _roomFloorPrefab.transform.position = new Vector3(gridsize.x/2f - 0.5f, _roomFloorPrefab.transform.position.y, gridsize.y / 2f - 0.5f) + new Vector3(_startPosition.x,0f,_startPosition.y);
        _roomFloorPrefab.transform.localScale = new Vector3(gridsize.x / 10, _roomFloorPrefab.transform.localScale.y, gridsize.y / 10);
    }
    #endregion
}
