using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//?��O?��?��?��b?��h?��?��?��?��?��L?��ϐ�?��̂悤?��ɂ�?��?��?��?��?��?��
//?��?��?��̑�?��?��?��?��?��?��ȋ@?��\?��?��ǉ�
public class Grid : MonoBehaviour
{
    private static int width = 10;
    private static int height = 20;
    private static Transform[,,] grid = new Transform[2,width, height];
    public float fallTime = 1f;
    public static int[] line = new int[2];
    
    //b?��{?��^?��?��?��?��?��?��?��?��?��?��?��炨?��ז�?��u?��?��?��b?��N?��?��ǉ�
    void Update(){
    }
    //?��O?��?��?��b?��h?��ɏ�?��?��?��?��?��?��
    public void write(int stage,int x,int y,Transform children){
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        grid[stage,x,y] = children;
    }
    //?��O?��?��?��b?��h?��̒l?��?��ǂݏo?��?��
    public Transform read(int stage,int x,int y){
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        return grid[stage,x,y];
    }
    //?��?��?��?��?��?��u?��?��?��b?��N?��i?��?��?��C?��?��?��j?��?��?��Ȃ�?��?��?��`?��F?��b?��N
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
    //?��?��?��C?��?��?��?��?��?��?��?��
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
    //?��g?��?��?��Ȃ�?��Ȃ�?��?��?��Q?��[?��?��?��I?��u?��W?��F?��N?��g?��?��?��?��?��?��
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
    //?��?��?��?��?��?��u?��?��?��b?��N?��?��?��?��?��?��
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
    //i?��?��?��A?��u?��?��?��b?��N?��?��?��?��?��?��?��?��
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
    //1?��u?��?��?��b?��N?��?��?��?��?��?��?��?��
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
    //?��w?��肳?��ꂽ?��?��?��W?��̃u?��?��?��b?��N?��?��?������?��?��?��
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

