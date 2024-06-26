using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//?申O?申?申?申b?申h?申?申?申?申?申L?申��鐃�?申�����?申��鐃�?申?申?申?申?申?申
//?申?申?申��鐃�?申?申?申?��?申?申��@?申\?申?申��鐃�
public class Grid : MonoBehaviour
{
    private static int width = 10;
    private static int height = 20;
    private static Transform[,,] grid = new Transform[2,width, height];
    public float fallTime = 1f;
    public static int[] line = new int[2];
    
    //b?申{?申^?申?申?申?申?申?申?申?申?申?申?申���?申��鐃�?申u?申?申?申b?申N?申?申��鐃�
    void Update(){
    }
    //?申O?申?申?申b?申h?申��鐃�?申?申?申?申?申?申
    public void write(int stage,int x,int y,Transform children){
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        grid[stage,x,y] = children;
    }
    //?申O?申?申?申b?申h?申��l?申?申����o?申?申
    public Transform read(int stage,int x,int y){
        if (stage > 10|| stage == 0){
            stage = 0;
        }
        else{
            stage = 1;
        }
        return grid[stage,x,y];
    }
    //?申?申?申?申?申?申u?申?申?申b?申N?申i?申?申?申C?申?申?申j?申?申?申��鐃�?申?申?申`?申F?申b?申N
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
    //?申?申?申C?申?申?申?申?申?申?申?申
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
    //?申g?申?申?申��鐃�?申��鐃�?申?申?申Q?申[?申?申?申I?申u?申W?申F?申N?申g?申?申?申?申?申?申
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
    //?申?申?申?申?申?申u?申?申?申b?申N?申?申?申?申?申?申
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
    //i?申?申?申A?申u?申?申?申b?申N?申?申?申?申?申?申?申?申
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
    //1?申��u?申?申?申b?申N?申?申?申?申?申?申?申?申
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
    //?申w?申���?申���?申?申?申W?申��u?申?申?申b?申N?申?申?申���鐃�?申?申?申
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

