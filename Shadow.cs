using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Minoの影の挙動を記述
public class Shadow : MonoBehaviour
{    //ステージの大きさ
    private static int width = 10;
    private static int height  = 20;
    //mino回転
    public Vector3 rotationPoint;

    // Update is called once per frame
    void Update(){
        int rad = 0;
        if(transform.position.x < 0){
            if(!FindObjectOfType<SpawnMino>().read_exist()){
                FindObjectOfType<SpawnMino>().write_exist(true);
                Destroy(this.gameObject);
                return;
            }
            transform.position = FindObjectOfType<SpawnMino>().read_position();
            rad = FindObjectOfType<SpawnMino>().read_rad();
            if(rad > 0){
                for(int r = 0; r < rad; r++){
                //minoを上矢印キーを押して回転させる
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
                }
            }
            else{
                for(int r = 0; r < -rad; r++){
                //minoを上矢印キーを押して回転させる
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                }
            }
            FindObjectOfType<SpawnMino>().write_rad(0);
            while(MinoDown(14));
        }
        else{
            if(!FindObjectOfType<SpawnMino1>().read_exist()){
                FindObjectOfType<SpawnMino1>().write_exist(true);
                Destroy(this.gameObject);
                return;
            }
            transform.position = FindObjectOfType<SpawnMino1>().read_position();
            rad = FindObjectOfType<SpawnMino1>().read_rad();
            if(rad > 0){
                for(int r = 0; r < rad; r++){
                //minoを上矢印キーを押して回転させる
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
                }
            }
            else{
                for(int r = 0; r < -rad; r++){
                //minoを上矢印キーを押して回転させる
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                }
            }
            FindObjectOfType<SpawnMino1>().write_rad(0);
            while(MinoDown(-10));
        }

        
    }
    bool MinoDown(int stage){
        transform.position += new Vector3(0,-1,0);
        if (!ValidMovement(stage))
            {
                transform.position -= new Vector3(0, -1, 0);
                return false;
            }
        return true;
    }
    bool ValidMovement(int stage)
    {
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x)+stage;
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }
            if (FindObjectOfType<Grid>().read(stage,roundX,roundY) != null){
                return false;
            }
        }
        return true;
    }

}