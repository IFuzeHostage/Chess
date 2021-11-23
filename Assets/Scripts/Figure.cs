using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Figure : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject Cell;
    [SerializeField]
    public enum Colors{White, Black};
    public enum MaterialTypes{Opaque, Transparent};
    [SerializeField]
    protected Colors Color;
    [SerializeField]
    protected Material WhiteMaterial;
    [SerializeField]
    protected Material BlackMaterial;
    [SerializeField]
    protected Material TransparentWhiteMaterial;
    [SerializeField]
    protected Material TransparentBlackMaterial;
    protected bool FirstMove = true;
    protected Vector2[] directions = new Vector2[4]{new Vector2(1,1), new Vector2(1,-1), new Vector2(-1,1), new Vector2(-1,-1)};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetMaterial(Colors color, MaterialTypes type){
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>()){
            if(color == Colors.White){
                if(type == MaterialTypes.Transparent){
                    renderer.material = TransparentWhiteMaterial;
                }
                else if(type == MaterialTypes.Opaque){
                    renderer.material = WhiteMaterial;
                }
            }
            else if(color == Colors.Black){
                if(type == MaterialTypes.Transparent){
                    renderer.material = TransparentBlackMaterial;
                }
                else if(type == MaterialTypes.Opaque){
                    renderer.material = BlackMaterial;
                }
            }
        }
    }

    public void SetColor(bool White){
        if(White){
            Color = Colors.White;
            SetMaterial(Colors.White, MaterialTypes.Opaque);
        }
        else{
            Color = Colors.Black;
            SetMaterial(Colors.Black, MaterialTypes.Opaque);
        }
    }

    public bool GetColor(){
        if(Color == Colors.White)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PlaceFigure(GameObject cell){
        Cell cellScript = cell.GetComponent<Cell>();
        //transform.position = cellScript.FigureSpot.transform.position;
        if(cellScript.PlacedFigure)
        {
            cellScript.PlacedFigure.GetComponent<Figure>().Demolish();
        }
        if(Cell)
        {
            Cell.GetComponent<Cell>().PlacedFigure = null;
        }
        Cell = cell;
        cellScript.PlacedFigure = transform.gameObject;
        return true;
    }

    public bool MoveFigure(GameObject cell){
        transform.DOMove(cell.transform.position, 5f).SetSpeedBased();
        PlaceFigure(cell);
        FirstMove = false;
        return true;
    }

    public void Demolish(){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).tag = "Untagged";
        }
        for(int i =0; i < transform.childCount; i++){
            Rigidbody body = transform.GetChild(i).gameObject.AddComponent<Rigidbody>() as Rigidbody;

            List<Material> mats = new List<Material>();
            body.GetComponent<MeshRenderer>().GetMaterials(mats);
            if(Color == Colors.White){
                SetMaterial(Colors.White, MaterialTypes.Transparent);
            }
            else {
                SetMaterial(Colors.Black, MaterialTypes.Transparent);
            }
            foreach(Renderer renderer in GetComponentsInChildren<Renderer>()){
                renderer.material.DOFade(0, 5);
            }
        }
        StartCoroutine(DelayedDestroy(5));
    }

    IEnumerator DelayedDestroy(int delay){
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(delay);
        //After we have waited 5 seconds print the time again.
        Destroy(transform.gameObject);
    }

    public GameObject[] GetAvailibleCells(GameObject[,] cells){
        var pos = GetCellPosition(cells, Cell);
        ArrayList result = GetMovement(cells, pos);
        GameObject[] goResult = new GameObject[result.Count];
        {
        int i = 0;
        foreach(Object cell in result.ToArray()){
            goResult[i] = cell as GameObject;
            i++;
        }
        return goResult;
        }
    }
    public Vector2 GetCellPosition(GameObject[,] cells, GameObject cell){
        for(int i = 0; i < cells.GetLength(0); i ++){
            for(int j = 0; j < cells.GetLength(1); j++){
                if(cells[i,j] == cell){
                    return new Vector2(i,j);
                }   
            }
        }
        return new Vector2(-1,-1);
    }   
    public virtual ArrayList GetMovement(GameObject[,] cells, Vector2 pos){
        ArrayList result = new ArrayList();
        try{
            if(Color == Colors.White){
                result.Add(cells[(int)pos.x, (int)pos.y+1]);
            }
            else{
                result.Add(cells[(int)pos.x, (int)pos.y-1]);
            }
        }
        catch {};
        return result;
    }
}
