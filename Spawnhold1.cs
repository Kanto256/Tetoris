using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnhold1 : MonoBehaviour
{
    public SpawnMino1 sp;
    public GameObject[] Minos;
    public bool exist = false;
    public bool make = false;
    public int hold1 = -1;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        hold1 = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool read_exist(){
        return exist;
    }
    public void write_exist(bool exist1){
        exist = exist1;
    }
    public void hold(Vector3 position1){
        int a = 0;
        if(hold1 == -1){
            make = false;
            exist = true;
            position = position1;
            Instantiate(Minos[sp.hold],transform.position,Quaternion.identity);
            hold1 = sp.hold;
            FindObjectOfType<SpawnMino1>().NewMino();
            FindObjectOfType<SpawnNextMino1>().write_exist(false);
            FindObjectOfType<SpawnNextMino1>().NewNextMino();
            return;
        }
        else make = true;
        position = position1;
        Instantiate(Minos[sp.hold],transform.position,Quaternion.identity);
        FindObjectOfType<SpawnMino1>().NewMino();
        a = hold1;
        hold1 = sp.hold;
        sp.hold = a;
    }
}
