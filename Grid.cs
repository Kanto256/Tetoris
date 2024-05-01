using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//?ｿｽO?ｿｽ?ｿｽ?ｿｽb?ｿｽh?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽL?ｿｽﾏ撰ｿｽ?ｿｽﾌよう?ｿｽﾉゑｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
//?ｿｽ?ｿｽ?ｿｽﾌ托ｿｽ?ｿｽ?ｿｽ?ｿｽ?い?ｿｽ?ｿｽﾈ機?ｿｽ\?ｿｽ?ｿｽﾇ会ｿｽ
public class Grid : MonoBehaviour
{
    private static int width = 10;
    private static int height = 20;
    private static Transform[,,] grid = new Transform[2,width, height];
    public float fallTime = 1f;
    public static int[] line = new int[2];
    
    //b?ｿｽ{?ｿｽ^?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ轤ｨ?ｿｽﾗ厄ｿｽ?ｿｽu?ｿｽ?ｿｽ?ｿｽb?ｿｽN?ｿｽ?ｿｽﾇ会ｿｽ
    void Update(){
    }
    //?ｿｽO?ｿｽ?ｿｽ?ｿｽb?ｿｽh?ｿｽﾉ擾ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
    public void write(int stage,int x,int y,Transform children){
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        grid[stage,x,y] = children;
    }
    //?ｿｽO?ｿｽ?ｿｽ?ｿｽb?ｿｽh?ｿｽﾌ値?ｿｽ?ｿｽﾇみ出?ｿｽ?ｿｽ
    public Transform read(int stage,int x,int y){
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        return grid[stage,x,y];
    }
    //?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽu?ｿｽ?ｿｽ?ｿｽb?ｿｽN?ｿｽi?ｿｽ?ｿｽ?ｿｽC?ｿｽ?ｿｽ?ｿｽj?ｿｽ?ｿｽ?ｿｽﾈゑｿｽ?ｿｽ?ｿｽ?ｿｽ`?ｿｽF?ｿｽb?ｿｽN
    public bool HasLine(int stage,int i)
    {
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        for (int j = 0; j < width; j++){
            if(grid[stage,j,i] == null)return false;
        }
        line[stage]++;
        return true;
    }
    //?ｿｽ?ｿｽ?ｿｽC?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
    public void DeleteLine(int stage,int i)
    {
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }

        for (int j = 0; j < width; j++)
        {
            if(grid[stage,j,i] == null)continue;
            Destroy(grid[stage,j,i].gameObject);
            Delete_object(stage,j,i);
        }
    }
    //?ｿｽg?ｿｽ?ｿｽ?ｿｽﾈゑｿｽ?ｿｽﾈゑｿｽ?ｿｽ?ｿｽ?ｿｽQ?ｿｽ[?ｿｽ?ｿｽ?ｿｽI?ｿｽu?ｿｽW?ｿｽF?ｿｽN?ｿｽg?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
    void Delete_object(int stage,int x,int y){
        bool exit = false;
        Transform parent = grid[stage,x,y].gameObject.transform.parent;
        grid[stage,x,y] = null;
        foreach(Transform children in parent){
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            if(stage == 0){
                roundX += 14;
            }else{
                roundX -= 10;
            }
            if(grid[stage,roundX,roundY] != null){
                exit = true;
                //Debug.Log(children.gameObject);
            }
        }
        if(!exit){
        Destroy(parent.gameObject);
        }
    }
    //?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽu?ｿｽ?ｿｽ?ｿｽb?ｿｽN?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
    public void CheckLines()
    {
        for (int stage = 0;stage < 2;stage++){
            line[stage] = 0;
            for (int i = height - 1; i >= 0; i--){
                if (HasLine(stage,i))
                {
                    DeleteLine(stage,i);
                    RowDown(stage,i);
                }
            }
            FindObjectOfType<GameManagement>().AddScore(stage,line[stage]);
        }  
    }
    //i?ｿｽ?ｿｽ?ｿｽA?ｿｽu?ｿｽ?ｿｽ?ｿｽb?ｿｽN?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
    public void RowDown(int stage,int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[stage,j, y] != null)
                {
                    grid[stage,j, y - 1] = grid[stage,j, y];
                    grid[stage,j, y] = null;
                    grid[stage,j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    //1?ｿｽﾂブ?ｿｽ?ｿｽ?ｿｽb?ｿｽN?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
    public void RowUp(int stage)
    {
        if (stage > 10 || stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        for (int y = height-1; y >= 0; y--){
            for (int j = 0; j < width; j++)
            {
                if (grid[stage,j, y] != null)
                {
                    grid[stage,j, y + 1] = grid[stage,j, y];
                    grid[stage,j, y] = null;
                    grid[stage,j, y + 1].transform.position += new Vector3(0, 1, 0);
                }
            }
        }
    }
    //?ｿｽw?ｿｽ閧ｳ?ｿｽ黷ｽ?ｿｽ?ｿｽ?ｿｽW?ｿｽﾌブ?ｿｽ?ｿｽ?ｿｽb?ｿｽN?ｿｽ?ｿｽ?ｿｽ尞懶ｿｽ?ｿｽ?ｿｽ?ｿｽ
    public void Delete_block(int stage,int x,int y){
        if (stage > 10 || stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        Destroy(grid[stage,x,y].gameObject);
        grid[stage,x,y] = null;
        
    }
}

