using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FigureClicker : MonoBehaviour
{
    [SerializeField]
    public GameObject SelectedFigure;
    [SerializeField]
    public GameObject[,] Cells;
    private GameObject[] AvailableCells;
    private bool turn = true;//White-true Black-false
    void Start()
    {
        Cells = GetComponent<CenterRotate>().Focus.GetComponent<ChessBoard>().GetCells();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                if(SelectedFigure == null && hit.transform.tag == "Figure")//Selecting a figure
                {
                    SelectFigure(hit.transform.parent.gameObject);
                }
                else if(SelectedFigure && hit.transform.tag == "Cell")//Moving a figure
                {
                    if(Array.Exists(AvailableCells, x => x == hit.transform.gameObject)){
                        SelectedFigure.GetComponent<Figure>().MoveFigure(hit.transform.gameObject);
                        SelectedFigure = null;
                        AvailableCells = null;
                        PassTurn();
                    }
                }
                else if(SelectedFigure && hit.transform.tag == "Figure")//Selecting a figure when figure is selected
                {
                    SelectFigure(hit.transform.parent.gameObject);
                    if(Array.Exists(AvailableCells, x => x == hit.transform.parent.gameObject.GetComponent<Figure>().Cell)){
                        SelectedFigure.GetComponent<Figure>().MoveFigure(hit.transform.parent.gameObject.GetComponent<Figure>().Cell);
                        SelectedFigure = null;
                        AvailableCells = null;
                        PassTurn();
                    }
                }
            }
            else
            {
                SelectedFigure = null;
                AvailableCells = null;
            }
            LigtCells(AvailableCells);
        }
    }
    private void LigtCells(GameObject[] cells){
        foreach(GameObject cell in Cells){
            cell.GetComponent<Outline>().enabled = false;
        }
        if(cells == null) return;
        foreach(GameObject cell in cells){
            cell.GetComponent<Outline>().enabled = true;
        }
    }
    private void SelectFigure(GameObject figure){
        if(figure.GetComponent<Figure>().GetColor() == turn){
            SelectedFigure = figure;
            AvailableCells = SelectedFigure.GetComponent<Figure>().GetAvailibleCells(Cells);
            if(AvailableCells == null){
                SelectedFigure = null;
            }
        }
    }
    private void PassTurn(){//Switching between White and Black turns
        if(turn){
            turn = false;
        }
        else{
            turn = true;
        }
    }
}
