using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
//���ɏo��Mino�𐶐�
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
        //SpawnNextMino�̎q�I�u�W�F�N�g(NextMino)���폜
        if(!spawnmino.gameOver)Instantiate(NextMinos[spawnmino.nextmino],transform.position,Quaternion.identity);
    }
    public bool read_exist(){
        return exist;
    }
    public void write_exist(bool exist1){
        exist = exist1;
    }
}