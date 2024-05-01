using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class result : MonoBehaviour
{
    public GameManagement gm;
    private TextMeshProUGUI TM;
    // Start is called before the first frame update
    void Start()
    {
        TM = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        TM.text = "Time:" + gm.gameTime.ToString("0.00") + "\nPress A to Restart";
    }
}
