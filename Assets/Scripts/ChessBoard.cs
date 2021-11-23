using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject CellStart;
    [SerializeField]
    private GameObject WhiteCell;
    [SerializeField]
    private GameObject BlackCell;
    [SerializeField]
    private GameObject FigurePrefab;
    [SerializeField]
    private GameObject[,] Cells;
    [SerializeField]
    private FigureSpawner Spawner;
    private CellSwitch CurrentCell = CellSwitch.WhiteCell;
    private enum CellSwitch{
        BlackCell,
        WhiteCell
    };
    void Start()
    {
        InitializeCells(8,8);
        InitializeFigures();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private GameObject NextCell(){
        if(CurrentCell == CellSwitch.WhiteCell){
            CurrentCell = CellSwitch.BlackCell;
            return BlackCell;
        }
        else if(CurrentCell == CellSwitch.BlackCell){
            CurrentCell = CellSwitch.WhiteCell;
            return WhiteCell;
        }
        return null;
    }

    void InitializeCells(int x, int y){
        Cells = new GameObject[x, y];
        for(int i = 0; i < x;i++){
            for(int j = 0; j < y; j++){
                var newCell = Instantiate(NextCell(), CellStart.transform.position + new Vector3(i, 0, j), CellStart.transform.rotation);
                Cells[i, j] = newCell;
                newCell.transform.parent = CellStart.transform;
            }
            NextCell();
        }
    }
    void InitializeFigures(){
        FigureSpawner.Figures[] row = Spawner.PawnRow;
        for(int i = 0; i < row.Length; i++){
            Spawner.SpawnFigure(Spawner.MainRow[i], true, Cells, i, 0);
            Spawner.SpawnFigure(Spawner.PawnRow[i], true, Cells, i, 1);
            Spawner.SpawnFigure(Spawner.PawnRow[i], false, Cells, i, 6);
            Spawner.SpawnFigure(Spawner.MainRow[i], false, Cells, i, 7);
        }
    }
    public GameObject[,] GetCells(){
        return Cells;
    }
}
