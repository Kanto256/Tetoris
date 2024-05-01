using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SePlay : MonoBehaviour
{
        //�Đ����Ǘ�������ʉ��������Z�b�g����ϐ�
    [SerializeField]
    public AudioClip[] audioClips;

    public AudioSource audioSource;

    void Start()
    {
        //�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();
    }

    //���ʉ����Đ�����
    public void Play(string seName)
    {
        audioSource.volume = 1f;
        switch (seName)
        {
            case "drop":
                audioSource.PlayOneShot(audioClips[0]);
                break;
            case "click":
                audioSource.PlayOneShot(audioClips[1]);
                break;
            case "fall":
                audioSource.volume = 0f;
                audioSource.PlayOneShot(audioClips[2]);
                break;
            case "exellent":
                audioSource.PlayOneShot(audioClips[3]);
                break;
            case "tasty":
                audioSource.PlayOneShot(audioClips[4]);
                break;
            case "nice":
                audioSource.PlayOneShot(audioClips[5]);
                break;
            case "ducshy":
                audioSource.PlayOneShot(audioClips[6]);
                break;
            case "K0":
                audioSource.PlayOneShot(audioClips[7]);
                break;
            case "K1":
                audioSource.PlayOneShot(audioClips[8]);
                break;
            case "R0":
                audioSource.PlayOneShot(audioClips[9]);
                break;
            case "R1":
                audioSource.PlayOneShot(audioClips[10]);
                break;
            case "R2":
                audioSource.PlayOneShot(audioClips[11]);
                break;
            case "T0":
                audioSource.PlayOneShot(audioClips[12]);
                break;
            case "Y0":
                audioSource.PlayOneShot(audioClips[13]);
                break;
            case "Y1":
                audioSource.PlayOneShot(audioClips[14]);
                break;
            default:
                Debug.Log("sound error");
                break;
            //:
            //:
            //�ȉ����ʉ��̐������L�q
        }
    }
}
