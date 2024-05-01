using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//êVÇµÇ¢MinoÇ∆ÇªÇÃâeÇê∂ê¨
public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;
    public GameObject[] Shadows;
    public int nextmino; //nextspawnminoÇ≈égóp
    public Vector3 position = new Vector3(-9,18,0);
    public int rad = 0;
    public bool exist = true;
    public int hold = 0;
    public int count = 0;
    public bool gameOver = false;
    public Spawnhold sp;
    public GameManagement gm;
    int i;

    void Start()
    {
        int a;
        exist = true;
        hold = 0;
        exist = true;
        count = 0;
        gameOver = false;
        switch (gm.character[0]){
            case 0:
                i = Random.Range(0,Minos.Length);
                break;
            case 1:
                a = Random.Range(0,Minos.Length+1);
                if(a == 7)i = 0;
                else i = a;
                break;
            case 2:
                i = Random.Range(0,Minos.Length);
                break;
            case 3:
                a = Random.Range(0,Minos.Length+1);
                if(a == 7)i = 5;
                else i = a;
                break;
        }
        NewMino();
        FindObjectOfType<SpawnNextMino>().NewNextMino(); 
    }
    public void write_exist(bool exist1){
        exist = exist1;
    }
    public bool read_exist(){
        return exist;
    }
    public void write_position(Vector3 position1){
        position = position1;
    }
    
    public Vector3 read_position(){
        return position;
    }
    public void write_rad(int rad1){
        rad = rad1;
    }
    public int read_rad(){
        return rad;
    }
    public bool read_count(){
        //return count == 0;
        return true;
    }
    public void NewMino()
    {
        gameOver = gm.gameOver;
        if(!gameOver){
        if(sp.make){
            sp.make = false;
            Instantiate(Minos[sp.hold1],sp.position,Quaternion.identity);
            Instantiate(Shadows[sp.hold1],sp.position,Quaternion.identity);
            nextmino = Random.Range(0,Minos.Length);
            count++;
            return;
        }
        count = 0;
        Instantiate(Minos[i],transform.position,Quaternion.identity);
        Instantiate(Shadows[i],transform.position,Quaternion.identity);
        nextmino = Random.Range(0,Minos.Length);
        hold = i;
        i = nextmino;
    }
    }
}
