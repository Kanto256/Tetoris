using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
//次に出るMinoを生成
public class SpawnNextMino : MonoBehaviour
{
    public GameObject[] NextMinos;
    public SpawnMino spawnmino;
    public bool exist = true;
    void Start(){
        exist = true;
    }
    public void NewNextMino()
    {
        //SpawnNextMinoの子オブジェクト(NextMino)を削除
        if(!spawnmino.gameOver)Instantiate(NextMinos[spawnmino.nextmino],transform.position,Quaternion.identity);
    }
    public bool read_exist(){
        return exist;
    }
    public void write_exist(bool exist1){
        exist = exist1;
    }
}