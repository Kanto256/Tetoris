using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Video;

public class GameManagement : MonoBehaviour
{
    // スコア関連
    //public TextMeshProUGUI scoreText;

    private int[] score = new int[2];
    private float[] previousTime = new float[2];
    private float deletatime;
    public int[] currentScore = new int[2];
    public int clearScore = 1500; //スコアがこの値になると終了
    private static int height = 20;
    //wii　リモコン関連
    //タイマー関連
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI[] character_names;
    public TextMeshProUGUI counter;
    public float gameTime = 0f;
    public int mode = 0;
    public bool[] mino_exist = new bool[]{true,true};
    public float seconds;
    public int[] character = new int[2];
    public int[] emotions = new int[2];
    private int testNum = 0;

    [SerializeField]
    private SoundMNG _sound;
    //UI関連
    public GameObject gamePauseUI;
    public GameObject[] characters;
    public GameObject[] namakubi;
    public GameObject[] holds;
    public GameObject countdown;
    public bool pause;
    int count = 3;
    public bool gameOver = false;
    public VideoClip videoClip;
    public GameObject screen;
    DateTime dt;
    int time;


    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = screen.AddComponent<VideoPlayer>();
        
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;
        videoPlayer.isLooping = true;
        videoPlayer.Pause();
        Initialize();
        gamePauseUI.gameObject.SetActive (false); //PauseUIの初期値を非アクティブに設定
        for(int i = 0;i < 4;i++)characters[i].gameObject.SetActive(false);
        character[0] = select.indicate[0];
        character[1] = select.indicate[1];
        previousTime[0] = Time.time;
        previousTime[1] = Time.time;
        holds[0].SetActive(true);
        holds[1].SetActive(true);
        deletatime = 2f;
        count = 3;
        gameTime = 0f;
        for(int i = 0;i < 2;i++){
            switch (character[i]){
                case 0:
                    character_names[i].text = "Koichi";
                    break;
                case 1:
                    character_names[i].text = "Rin";
                    break;
                case 2:
                    character_names[i].text = "Teppei";
                    break;
                case 3:
                    character_names[i].text = "Yucky";
                    break;  
            }
        }
        gameOver = false;
        Instantiate(namakubi[character[0]],new Vector3(-9,10,0),Quaternion.identity);
        Instantiate(namakubi[character[1]],new Vector3(15,10,0),Quaternion.identity);
        emotions[0] = 0;
        emotions[1] = 0;
        pause = true;
        countdown.gameObject.SetActive(true);
        dt = DateTime.Now;
        time = dt.Millisecond + dt.Second * 1000;
    }
    // Update is called once per frame
    // Use "mode" that control playmode; play alone or together

    void Update()
    {
        if(count != 11)counting();
        if(mode == 0)TimeManagement(); // play alone
         // play together
        if(Input.GetKeyDown(KeyCode.B)){
            if(mode >= 1)mode = 0;
            else mode += 1;
        }
        if(Time.time - previousTime[0] > deletatime)emotions[0] = 0;
        if(Time.time - previousTime[1] > deletatime)emotions[1] = 0;
        if(Input.GetKeyDown(KeyCode.X)){
            SceneManager.LoadScene("start");
            pause = true;
        }
        if (_sound.GetNowBgmState() == SoundMNG.BGM_STATE.END){
            _sound.StartSoundNum(-1);
        }
    }
    public bool read_pause(){
        return pause;
    }
    public int read_emotion(int stage){
        return emotions[stage];
    }
    public bool read_gameOver(){
        return gameOver;
    }
    public void write_mino_exist(int stage,bool exist1){
        mino_exist[stage] = exist1;
    }
    public bool read_mino_exist(int stage){
        return mino_exist[stage];
    }
    public void write_mode(int mode1){
        mode = mode1;
    }
    public int read_mode(){
        return mode;
    }
    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        score[0] = 0;
        score[1] = 0;
        gameTime = 0f;
    }

    public void TimeManagement()
    {
        gameTime += Time.deltaTime;
        seconds = (int)gameTime;
        timerText.text = "TIME:" + gameTime.ToString("0.00");
    }

    // スコアの追加
    public void AddScore(int stage,int line)
    {
        //scoreText.text = "Score: " + score[0].ToString();
        int baria = UnityEngine.Random.Range(0,3);
        Debug.Log(score[stage]);
        if(stage == 0){
            if(character[1] == 2 && baria == 0)line = 0;
            FindObjectOfType<AddLine1>().NewLine(line);
            if(line!=0){
                emotions[0] = 2;
                previousTime[0] = Time.time;
                emotions[1] = 1;
                previousTime[1] = Time.time;
            }
        }
        else {
            if(character[0] == 2 && baria == 0)line = 0;
            FindObjectOfType<AddLine>().NewLine(line);
            if(line!=0){
                emotions[0] = 1;
                previousTime[0] = Time.time;
                emotions[1] = 2;
                previousTime[1] = Time.time;
            }
        }
    }

    //GameOverしたときの処理
    public void GameOver(int stage)
    {
        int message;
        var sePlay = GameObject.Find("SePlay").GetComponent<SePlay>();
        Time.timeScale = 0f;
        characters[character[stage]].SetActive(true);
        _sound.SoundPause();
        switch (character[stage]){
            case 0:
                message = UnityEngine.Random.Range(0,2);
                sePlay.Play("K" + message.ToString());
                break;
            case 1:
                message = UnityEngine.Random.Range(0,3);
                sePlay.Play("R" + message.ToString());
                break;
            case 2:
                sePlay.Play("T0");
                break;
            case 3:
                message = UnityEngine.Random.Range(0,2);
                sePlay.Play("Y" + message.ToString());
                break;
        }
        holds[0].SetActive(false);
        holds[1].SetActive(false);
        character_names[0].text = "";
        character_names[1].text = "";
        gameOver = true;
        pause = true;
        //SceneManager.LoadScene("GameOverScene");
    }

    //GameClearしたときの処理
    public void GameClear()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        //SceneManager.LoadScene("ClearScene");
    }

    public void clear_stage(int stage){
        for(int a = 0; a < height; a++){
            FindObjectOfType<Grid>().DeleteLine(stage,a);
        }
        mino_exist[stage] = false;
        if(stage == 0){
            FindObjectOfType<SpawnMino>().write_exist(false);
        }
        else{
            FindObjectOfType<SpawnMino1>().write_exist(false);
        }
    }
    //GamePauseしたときの処理
    public void GamePause()
    {
        GamePauseToggle();
    }

    public void GamePauseToggle()
    {
        gamePauseUI.SetActive(!gamePauseUI.activeSelf);
        if(gamePauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }

        else
        {
            Time.timeScale = 1f;
        }
    }
    public void counting(){
        counter.text = count.ToString();
        if(count < 0){
            var videoPlayer = screen.GetComponent<VideoPlayer>();
            _sound.StartSoundNum(3);
            count = 11;
            Time.timeScale = 1f;
            pause = false;
            countdown.gameObject.SetActive(false);
            videoPlayer.Play();
            return;
        }
        dt = DateTime.Now;
        if(((dt.Millisecond + dt.Second * 1000) - time)  >= 1000){
            var SePlay = GameObject.Find("SePlay").GetComponent<SePlay>();
            count--;
            SePlay.Play("click");
            time = dt.Millisecond + dt.Second * 1000;
        }
        Time.timeScale = 0f;
    }
}