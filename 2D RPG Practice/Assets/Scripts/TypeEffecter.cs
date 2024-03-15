using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffecter : MonoBehaviour
{
    public int CharPerSeconds; //Ÿ�� �ӵ�
    public GameObject EndCursor;
    public bool isAnim; //�ִϸ��̼� ���������� Ȯ��

    string targetMsg; //���� �޽���
    TextMeshProUGUI msgText; //���� ǥ�õǴ� �޽���
    AudioSource audioSource;

    int index;
    float interval; //Ȯ���� �Ҽ����� ��� ����

    public void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if (isAnim) { //��ȭ ��ŵ
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart() //����
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        isAnim = true;
        Invoke("Effecting", interval); //1���ڰ� ������ ������
    }

    void Effecting() //���
    {
        //�޽��� ����
        if (msgText.text == targetMsg) {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; //���� �ε����� ���� ��������
        //����
        if (targetMsg[index] != ' ' && targetMsg[index] != '.') {
            audioSource.Play();
        }

        index++;

        Invoke("Effecting", interval);
    }

    void EffectEnd() //����
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
