using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ז��u���b�N�̐ݒ�Ȃ�
public class line : MonoBehaviour
{
    // Start is called before the first frame update
    //�g���Ȃ��Ȃ�����e�I�u�W�F�N�g������
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
    //�O���b�h�ɂ��ז��u���b�N�̈ʒu������������
    public void AddToGrid(){
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x)+14;
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            FindObjectOfType<Grid>().write(14,roundX,roundY,children);
        }
    }
}
