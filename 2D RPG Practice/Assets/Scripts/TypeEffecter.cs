using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffecter : MonoBehaviour
{
    public int CharPerSeconds; //타자 속도
    public GameObject EndCursor;
    public bool isAnim; //애니메이션 실행중인지 확인

    string targetMsg; //원본 메시지
    TextMeshProUGUI msgText; //현재 표시되는 메시지
    AudioSource audioSource;

    int index;
    float interval; //확실한 소수값을 얻기 위함

    public void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if (isAnim) { //대화 스킵
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart() //시작
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        isAnim = true;
        Invoke("Effecting", interval); //1글자가 나오는 딜레이
    }

    void Effecting() //재생
    {
        //메시지 끝남
        if (msgText.text == targetMsg) {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; //현재 인덱스의 글자 가져오기
        //사운드
        if (targetMsg[index] != ' ' && targetMsg[index] != '.') {
            audioSource.Play();
        }

        index++;

        Invoke("Effecting", interval);
    }

    void EffectEnd() //종료
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
