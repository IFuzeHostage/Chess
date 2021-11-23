using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject DefaultFigure;
    [SerializeField]
    private GameObject PawnFigure;
    [SerializeField]
    private GameObject RookFigure;
    [SerializeField]
    private GameObject KnightFigure;
     [SerializeField]
    private GameObject BishopFigure;
    [SerializeField]
    private GameObject KingFigure;
    [SerializeField]
    private GameObject QueenFigure;
    [SerializeField]
    private Material BlackFigure;
    [SerializeField]
    private Material WhiteFigure;
    public Figures[] PawnRow = {Figures.Pawn, Figures.Pawn, Figures.Pawn, Figures.Pawn, Figures.Pawn, Figures.Pawn, Figures.Pawn, Figures.Pawn};
    public Figures[] MainRow = {Figures.Rook, Figures.Knight, Figures.Bishop, Figures.King, Figures.Queen, Figures.Bishop, Figures.Knight, Figures.Rook};
    public enum Figures{
        Pawn,
        Rook,
        Bishop,
        Knight,
        King,
        Queen,
        Default
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnFigure(Figures figure, bool white, GameObject[,] cells, int x, int y){
        GameObject newFigure = DefaultFigure;

        if(figure == Figures.Pawn){
            newFigure = PawnFigure;
        }
        else if(figure == Figures.Rook){
            newFigure = RookFigure;
        }
        else if(figure == Figures.Bishop){
            newFigure = BishopFigure;
        }
        else if(figure == Figures.Knight){
            newFigure = KnightFigure;
        }
        else if(figure == Figures.King){
            newFigure = KingFigure;
        }
        else if(figure == Figures.Queen){
            newFigure = QueenFigure;
        }
        newFigure = Instantiate(newFigure, transform.position, transform.rotation);
        if(!white){
            newFigure.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        // cells[x, y].GetComponent<Cell>().PlaceFigure(newFigure);
        newFigure.GetComponent<Figure>().PlaceFigure(cells[x,y]);
        newFigure.transform.position=cells[x,y].GetComponent<Cell>().FigureSpot.transform.position  ;
        newFigure.GetComponent<Figure>().SetColor(white);
        return newFigure;
    }
}
