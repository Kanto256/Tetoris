using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class select : MonoBehaviour
{
    public GameObject[] panel;
    
    public static int[] indicate = new int[2];
    public bool[] decision = new bool[2];
    public GameObject ready;
    // Start is called before the first frame update
    void Start()
    {
        indicate[0] = 0;
        indicate[1] = 3;
        decision[0] = false;
        decision[1] = false;
        ready.gameObject.SetActive (false);
        //panel[0].GetComponent<Renderer>().material.color = new Color32(253,99,99,175);
        //panel[3].GetComponent<Renderer>().material.color = new Color32(105,236,255,175);
    }
    // Update is called once per frame
    void Update()
    {
        var SePlay = GameObject.Find("SePlay").GetComponent<SePlay>();
        if(Input.GetKeyDown(KeyCode.X)){
            SceneManager.LoadScene("start");
        }
        if(decision[0] && decision[1]){
            //StartCoroutine(Defeat());
            ready.gameObject.SetActive (true);
            
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
                SePlay.Play("drop");
                for(int f = 0;f < 2;f++){
                    switch (indicate[f]){
                        case 0:indicate[f] = 0;break;
                        case 1:indicate[f] = 2;break;
                        case 2:indicate[f] = 3;break;
                        case 3:indicate[f] = 1;break;
                    }
                }
                SceneManager.LoadScene("game");
            }
            else if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.RightShift)){
                SePlay.Play("drop");
                Debug.Log("canceled");
                ready.gameObject.SetActive (false);
                decision[0] = false;
                decision[1] = false;
                panel[indicate[0]].GetComponent<Renderer>().material.color = new Color32(253,99,99,175);
                panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(105,236,255,175);
            }
            return;
        }
        if(Input.GetKeyDown(KeyCode.W) || decision[0]){
            saymessage(0);
            panel[indicate[0]].GetComponent<Renderer>().material.color = new Color32(255,0,0,200);
            decision[0] = true;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || decision[1]){
            saymessage(1);
            panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(0,223,255,200);
            decision[1] = true;
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            decision[0] = false;
            panel[indicate[0]].GetComponent<Renderer>().material.color = new Color32(253,99,99,175);
            SePlay.Play("drop");
        }
        else if(Input.GetKeyDown(KeyCode.RightShift)){
            decision[1] = false;
            panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(105,236,255,175);
            SePlay.Play("drop");
        }

        if(Input.GetKeyDown(KeyCode.A) && (!decision[0])){
            panel[indicate[0]].GetComponent<Renderer>().material.color = new Color32(253,99,99,0);
            SePlay.Play("drop");
            dec_indicate(0);
        }
        else if(Input.GetKeyDown(KeyCode.D) && (!decision[0])){
            panel[indicate[0]].GetComponent<Renderer>().material.color = new Color32(253,99,99,0);
            SePlay.Play("drop");
            add_indicate(0);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && (!decision[1])){
            panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(253,99,99,0);
            SePlay.Play("drop");
            dec_indicate(1);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && (!decision[1])){
            panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(253,99,99,0);
            SePlay.Play("drop");
            add_indicate(1);
        }
        else return;
        panel[indicate[0]].GetComponent<Renderer>().material.color = new Color32(253,99,99,175);
        panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(105,236,255,175);
    }
    void add_indicate(int stage){
        if(indicate[stage]+1 == indicate[(stage+1)%2] && decision[(stage+1)%2]){
            indicate[stage] += 2;
            if(indicate[stage] > 3){
                indicate[stage] -= 2;
                if(stage == 0)panel[indicate[0]].GetComponent<Renderer>().material.color = new Color32(253,99,99,175);
                else panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(105,236,255,175);
            }
            
            return;
        }
        if(indicate[stage] >= 3)indicate[stage] = 3;
        else indicate[stage]++;
    }
    void dec_indicate(int stage){
        if(indicate[stage]-1 == indicate[(stage+1)%2] && decision[(stage+1)%2]){
            indicate[stage] -= 2;
            if(indicate[stage] < 0){
                indicate[stage] += 2;
                panel[indicate[1]].GetComponent<Renderer>().material.color = new Color32(253,99,99,0);
            }
            return;
        }
        if(indicate[stage] <= 0)indicate[stage] = 0;
        else indicate[stage]--;
    }
    void saymessage(int i){
        if(decision[i])return;
        var SePlay = GameObject.Find("SePlay").GetComponent<SePlay>();
        switch (indicate[i]){
            case 3:
                SePlay.Play("ducshy");
                break;
            case 2:
                SePlay.Play("nice");
                break;
            case 1:
                SePlay.Play("tasty");
                break;
            case 0:
                SePlay.Play("exellent");
                break;
        }
    }
    IEnumerator Defeat() 
{
    //終わるまで待ってほしい処理を書く
    //例：敵が倒れるアニメーションを開始
 
    //2秒待つ
    yield return new WaitForSeconds(1.5f);
    for(int f = 0;f < 2;f++){
                switch (indicate[f]){
                    case 0:indicate[f] = 0;break;
                    case 1:indicate[f] = 2;break;
                    case 2:indicate[f] = 3;break;
                    case 3:indicate[f] = 1;break;
                }
            }
    SceneManager.LoadScene("game");
    //再開してから実行したい処理を書く
    //例：敵オブジェクトを破壊
} 
}
