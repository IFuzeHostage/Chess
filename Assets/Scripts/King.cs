using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Figure
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override ArrayList GetMovement(GameObject[,] cells, Vector2 pos)
    {
        ArrayList result = new ArrayList();
        Vector2[] moves = new Vector2[8]{new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1)};
        foreach(Vector2 move in moves){
            try
            {
                GameObject cell = cells[(int)(pos + move).x, (int)(pos + move).y];
                if(cell.GetComponent<Cell>().PlacedFigure == null){
                    result.Add(cell);
                }
                else if(cell.GetComponent<Cell>().PlacedFigure.GetComponent<Figure>().GetColor() != GetColor()){
                    result.Add(cell);
                }
            }
            catch{}
        }
        return result;
    }
}
