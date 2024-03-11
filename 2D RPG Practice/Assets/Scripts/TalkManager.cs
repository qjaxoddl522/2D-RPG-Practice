using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData; //���� �ʻ�ȭ ����

    public Sprite[] portraitArr; //�ʻ�ȭ �̹��� ����

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //�Ϲ� ��ȭ ������
        talkData.Add(1000, new string[] { "�ȳ�?|0", "����Ƽ ����� �Ǵ� �׳����� ȭ����!|2" });
        talkData.Add(2000, new string[] { "������?|1", "�׷��� �������� ��.|0", "��¸�ŭ ������ �͵� �����ϱ�...|2" });

        talkData.Add(100, new string[] { "����� �������ڴ�..." });
        talkData.Add(200, new string[] { "å�� �� ���̿� ��̿� ���ο� ���� �̾߱Ⱑ �����ִ�." });

        //����Ʈ ��ȭ ������(����Ʈ ��ȣ + NPC ID)
        talkData.Add(10 + 1000, new string[] { "ó�� ���� ���̳�?|1", "����Ƽ�� ���� �־�?|0", "������ ȣ���� ����! �� ����� �˷��ٰž�.|2" });
        talkData.Add(11 + 2000, new string[] { "����Ƽ�� ��췯 �Գ�?|0", "������ ��¥�� ����.|2", "������ ���� �ֿ����� �����!|3" });

        //�ʻ�ȭ ������
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex) //(���� id, n��° ���)
    {
        if (talkIndex == talkData[id].Length) {
            return null;
        }
        else {
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex) //(���� id, �ʻ�ȭ n��° ��)
    {
        return portraitData[id + portraitIndex];
    }
}
