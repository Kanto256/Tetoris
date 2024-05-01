using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//呼び出されるとお邪魔ブロックを生成する
public class AddLine : MonoBehaviour
{
    public int i = 0;
    public GameObject Line;
    public void NewLine(int line)
    {
        var SePlay = GameObject.Find("SePlay").GetComponent<SePlay>();
        switch(line){
            case 0: break;
            case 1: FindObjectOfType<Grid>().RowUp(14);
                    Instantiate(Line,transform.position,Quaternion.identity);
                    FindObjectOfType<line>().AddToGrid();
                    FindObjectOfType<Grid>().Delete_block(14,i,0);
                    SePlay.Play("ducshy");
                    break;
            case 2: for(int j = 0;j < 2;j++){
                        FindObjectOfType<Grid>().RowUp(14);
                        Instantiate(Line,transform.position,Quaternion.identity);
                        FindObjectOfType<line>().AddToGrid();
                        FindObjectOfType<Grid>().Delete_block(14,i,0);
                    }
                    SePlay.Play("nice");
                    i = Random.Range(0,10);
                    break;
            case 3: for(int j = 0;j < 3; j++){
                        if(j == 0 || j == 1)i = Random.Range(0,10);
                        FindObjectOfType<Grid>().RowUp(14);
                        Instantiate(Line,transform.position,Quaternion.identity);
                        FindObjectOfType<line>().AddToGrid();
                        FindObjectOfType<Grid>().Delete_block(14,i,0);
                    }
                    i = Random.Range(0,10);
                    SePlay.Play("tasty");
                    break;
            case 4: for(int j = 0;j < 4;j++){
                        i = Random.Range(0,10);
                        FindObjectOfType<Grid>().RowUp(14);
                        Instantiate(Line,transform.position,Quaternion.identity);
                        FindObjectOfType<line>().AddToGrid();
                        FindObjectOfType<Grid>().Delete_block(14,i,0);
                    }
                    i = Random.Range(0,10);
                    SePlay.Play("exellent");
                    break;
    }
    }
}

