using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Figure
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
    Vector2[] moves = new Vector2[2]{new Vector2(2,1), new Vector2(1,2)};
    
    ArrayList result = new ArrayList();
    foreach(Vector2 move in moves){
        foreach(Vector2 direction in directions){
            try{
                GameObject cell = cells[(int)(pos + move*direction).x, (int)(pos + move*direction).y];
                if(cell.GetComponent<Cell>().PlacedFigure == null || cell.GetComponent<Cell>().PlacedFigure.GetComponent<Figure>().GetColor() != GetColor()){
                    result.Add(cell);
                }
                //result.Add(cell);
            }
            catch{};
        }
    }
    return result;
    }
}
