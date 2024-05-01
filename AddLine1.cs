using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//呼び出されるとお邪魔ブロックを生成する
public class AddLine1 : MonoBehaviour
{
    public int i = 0;
    public GameObject Line1;
    public void NewLine(int line)
    {
        switch(line){
            case 0: break;
            case 1: FindObjectOfType<Grid>().RowUp(-10);
                    Instantiate(Line1,transform.position,Quaternion.identity);
                    FindObjectOfType<line1>().AddToGrid();
                    Debug.Log(i);
                    FindObjectOfType<Grid>().Delete_block(-10,i,0);
                    break;
            case 2: for(int j = 0;j < 2;j++){
                        FindObjectOfType<Grid>().RowUp(-10);
                        Instantiate(Line1,transform.position,Quaternion.identity);
                        FindObjectOfType<line1>().AddToGrid();
                        FindObjectOfType<Grid>().Delete_block(-10,i,0);
                    }
                    i = Random.Range(0,10);
                    break;
            case 3: for(int j = 0;j < 3; j++){
                        if(j == 0 || j == 1)i = Random.Range(0,10);
                        FindObjectOfType<Grid>().RowUp(-10);
                        Instantiate(Line1,transform.position,Quaternion.identity);
                        FindObjectOfType<line1>().AddToGrid();
                        FindObjectOfType<Grid>().Delete_block(-10,i,0);
                    }
                    i = Random.Range(0,10);
                    break;
            case 4: for(int j = 0;j < 4;j++){
                        i = Random.Range(0,10);
                        FindObjectOfType<Grid>().RowUp(-10);
                        Instantiate(Line1,transform.position,Quaternion.identity);
                        FindObjectOfType<line1>().AddToGrid();
                        FindObjectOfType<Grid>().Delete_block(-10,i,0);
                    }
                    i = Random.Range(0,10);
                    break;
    }
    }
}