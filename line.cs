using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//お邪魔ブロックの設定など
public class line : MonoBehaviour
{
    // Start is called before the first frame update
    //使われなくなったら親オブジェクトを消す
    void Update()
    {
        bool exit = false;
        foreach (Transform children in transform){
            if(children != null){
                exit = true;
            }
        }
        if(!exit){
            Destroy(this.gameObject);
        }
    }
    //グリッドにお邪魔ブロックの位置情報を書き込む
    public void AddToGrid(){
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x)+14;
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            FindObjectOfType<Grid>().write(14,roundX,roundY,children);
        }
    }
}
