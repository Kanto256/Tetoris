using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Mino�̋������L�q���Ă���
public class MIno : MonoBehaviour
{
    public float previousTime;
    //mino��������^�C��
    public float fallTime = 0.1f;
    public float start_time;
    //�E�����̃X�e�[�W�Ȃ̂���ݒ�
    private int stage;
    //�X�e�[�W�̑傫��
    private static int width = 10;
    private static int height  = 20;
    private bool[,] wii = new bool[2,5];
    public GameManagement game;
    //mino��]
    public Vector3 rotationPoint;
    void Start(){
        if(transform.position.x < 0){
            stage = 14;
            if(transform.position.x < -10)while(!ValidMovement())transform.position += new Vector3(1,0,0);
            else while(!ValidMovement())transform.position += new Vector3(-1,0,0);
        }
        else{
            stage = -10;
            if(transform.position.x < 15)while(!ValidMovement())transform.position += new Vector3(1,0,0);
            else while(!ValidMovement())transform.position += new Vector3(-1,0,0);
        }
        start_time = Time.time;
        fallTime = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        int i;
        if(transform.position.x > 0){
            i = 1;
        }
        else{
            i = 0;
        }
        if(!FindObjectOfType<GameManagement>().read_mino_exist(i)){ 
            FindObjectOfType<GameManagement>().write_mino_exist(i,true);
            Destroy(this.gameObject);
        }
        if(Input.GetKeyDown(KeyCode.E) && i == 0 && FindObjectOfType<SpawnMino>().read_count()){
            FindObjectOfType<SpawnMino>().write_exist(false);
            FindObjectOfType<Spawnhold>().write_exist(false);
            FindObjectOfType<Spawnhold>().hold(transform.position);
            Destroy(this.gameObject);
        }
        else if(Input.GetKeyDown(KeyCode.P) && i == 1){
            FindObjectOfType<SpawnMino1>().write_exist(false);
            FindObjectOfType<Spawnhold1>().write_exist(false);
            FindObjectOfType<Spawnhold1>().hold(transform.position);
            Destroy(this.gameObject);
        }
        if(!FindObjectOfType<GameManagement>().read_pause())MinoMovement();
    }
    private void MinoMovement(){
        int rad = 0;
        var sePlay = GameObject.Find("SePlay").GetComponent<SePlay>();
        if(transform.position.x < 0){
            stage = 14;
        }
        else{
            stage = -10;
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && stage < 0 || (Input.GetKeyDown(KeyCode.A)) && stage >= 0)
        {
            transform.position += new Vector3(-1,0,0);
            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
                return;
            }
            sePlay.Play("drop");
        }
        // �E���L�[�ŉE�ɓ���
        else if ((Input.GetKeyDown(KeyCode.RightArrow)) && stage < 0 || (Input.GetKeyDown(KeyCode.D)) && stage >= 0)
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(1, 0, 0);
                return;
            }
            sePlay.Play("drop");
        }
        // �����ŉ��Ɉړ������A�����L�[�ł��ړ�����
        else if ((Input.GetKeyDown(KeyCode.DownArrow))  && stage < 0 || (Input.GetKeyDown(KeyCode.S)) && stage >= 0|| Time.time-previousTime >= fallTime) 
        {
            previousTime = Time.time;
            MinoDown();
        }
        else if((Input.GetKeyDown(KeyCode.UpArrow)) && stage < 0 || (Input.GetKeyDown(KeyCode.W)) && stage >= 0)
        {
            //mino������L�[�������ĉ�]������
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
            rad += 1;
            if(!ValidMovement()){
                rad -= 1;
                transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                return;
            }
            sePlay.Play("drop");
        }
        else if((Input.GetKeyDown(KeyCode.RightShift)) && stage < 0 || (Input.GetKeyDown(KeyCode.Q)) && stage >= 0)
        {
            //mino������L�[�������ĉ�]������
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
            rad -= 1;
            if(!ValidMovement()){
                rad += 1;
                transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
                return;
            }
            sePlay.Play("drop");
        }
        else if((Input.GetKeyDown(KeyCode.I)) && stage < 0 || (Input.GetKeyDown(KeyCode.Space)) && stage >= 0)
        {
            if(Time.time - start_time > 0.5f){
            sePlay.Play("fall");
            //mino�����܂ŗ��Ƃ�
            while(MinoDown());
            sePlay.Play("drop");
            }
        }
        if(stage > 0){
            FindObjectOfType<SpawnMino>().write_position(transform.position);
            FindObjectOfType<SpawnMino>().write_rad(rad);
        }
        else{
            FindObjectOfType<SpawnMino1>().write_position(transform.position);
            FindObjectOfType<SpawnMino1>().write_rad(rad);
        }
        
    }
    //Mino��1������
    bool MinoDown(){
        transform.position += new Vector3(0, -1, 0);
        if (!ValidMovement()) //���ɂ����Ƃ��̏���
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                this.enabled = false;
                FindObjectOfType<Grid>().CheckLines();
                if(stage > 0)FindObjectOfType<SpawnMino>().write_exist(false);
                else FindObjectOfType<SpawnMino1>().write_exist(false);
                
                if(stage  > 10 || stage == 0){
                    FindObjectOfType<SpawnMino>().NewMino();
                    FindObjectOfType<SpawnNextMino>().write_exist(false);
                    FindObjectOfType<SpawnNextMino>().NewNextMino();
                }
                else {
                    FindObjectOfType<SpawnMino1>().NewMino();
                    FindObjectOfType<SpawnNextMino1>().write_exist(false);
                    FindObjectOfType<SpawnNextMino1>().NewNextMino();
                }
                
                return false;
            }
        return true;
    }
    //Grid��Mino�̈ʒu������������
    void AddToGrid()
    {
        bool gameover = false;
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x)+stage;
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            FindObjectOfType<Grid>().write(stage,roundX,roundY,children);

            //height-1 = 19�̂Ƃ���܂Ńu���b�N��������GameOver
            if(roundY >= height - 1)
            {
                //GameOver���\�b�h���Ăяo��
                gameover = true;
            }
        }
        if(gameover && transform.position.x < 0){
            FindObjectOfType<GameManagement>().GameOver(1);
        }
        else if(gameover && transform.position.x > 0){
            FindObjectOfType<GameManagement>().GameOver(0);
        }
    }
    //�����邩�ǂ����̊m�F
    bool ValidMovement()
    {
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x)+ stage;
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
