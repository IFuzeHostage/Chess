using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Figure
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override ArrayList GetMovement(GameObject[,] cells, Vector2 pos){
        ArrayList result = new ArrayList();
        Vector2[] moves;
        Vector2[] attacks = new Vector2[2]{new Vector2(1,1), new Vector2(-1,1)};
        Vector2 direction;
        if(Color == Colors.White){
            direction = new Vector2(1,1);
        }
        else{
            direction = new Vector2(1, -1);
        }
        if(FirstMove)
            moves = new Vector2[2]{new Vector2(0,1), new Vector2(0,2)};
        else{
            moves = new Vector2[1]{new Vector2(0,1)};
        }
        //Add moves
        foreach(Vector2 move in moves)
        {
            try
            {
                GameObject cell = cells[(int)(pos + move*direction).x, (int)(pos + move*direction).y];
                if(cell.GetComponent<Cell>().PlacedFigure == null)
                {
                    //print(cell.GetComponent<Cell>().PlacedFigure + " " + (pos+ move*direction));
                    result.Add(cell);
                }
                else break;
            }
            catch{}
        }
        //Add enemies if present
        foreach(Vector2 move in attacks){
            try
            {
                GameObject cell = cells[(int)(pos + move*direction).x, (int)(pos + move*direction).y];
                if(cell.GetComponent<Cell>().PlacedFigure.GetComponent<Figure>().GetColor() != GetColor())
                {
                    result.Add(cell);
                }
            }
            catch{}
        }
        foreach(GameObject cell in result){
        }
        return result;
    }
}
