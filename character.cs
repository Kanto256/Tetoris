using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public GameObject[] images;
    public float previousTime;
    public float deletatime = 2f;
    public int rad = 0;
    private Vector3 rotationPoint;
    // Start is called before the first frame update
    void Start()
    {
        previousTime = Time.time;
        rad = 0;
        deletatime = 1.5f;
        transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-45);
    }

    // Update is called once per frame
    void Update()
    {
        int e;
        if(transform.position.x < 0){
            e = FindObjectOfType<GameManagement>().read_emotion(0);
        }
        else{
            e = FindObjectOfType<GameManagement>().read_emotion(1);
        }
        emotion(e);
        if(Time.time - previousTime >= deletatime){
            previousTime = Time.time;
            
            switch (rad % 4){
                case 0:
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
                    break;
                case 1:
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                    break;
                case 2:
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
                    break;
                case 3:
                    transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                    break;
            }
            rad++;
        }

    }
    void emotion(int e){
        for(int i = 0;i < 3; i++){
            if(i == e){
                images[i].SetActive(true);
            }
            else{
                images[i].SetActive(false);
            }
        }
    }
}
