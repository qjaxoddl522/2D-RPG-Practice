using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    float h;
    float v;
    int lastdir = 0; //���������� ���� ����Ű
    Vector3 dirVec; //�ٶ󺸴� ���⺤��
    GameObject scanObject; //�νĵ� ������Ʈ

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            lastdir = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            lastdir = 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            lastdir = 3;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            lastdir = 4;
        }

        if (h != 0 && v != 0) { //���������� ���� ����Ű�� �ƴ� ���� ����
            if ((lastdir == 1) || (lastdir == 3)) {
                h = 0;
            }
            else {
                v = 0;
            }
        }

        //�ν� ���͸� ���� ���� ����
        if (v == 1) {
            dirVec = Vector3.up;
        }
        else if (v == -1) {
            dirVec = Vector3.down;
        }
        else if (h == -1) {
            dirVec = Vector3.left;
        }
        else if (h == 1) {
            dirVec = Vector3.right;
        }

        //������Ʈ ��ĵ(�����̽���)
        if (Input.GetButtonDown("Jump") && scanObject != null) {
            Debug.Log("Scanned : " + scanObject.name);
        }

        //�ִϸ��̼�
        if (anim.GetInteger("hAxisRaw") != h) { //�ִϸ��̼��� ���� �ٸ��� isChange�� �ѹ��� �����ϰ� ���� ��ġ��Ŵ
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v) {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else {
            anim.SetBool("isChange", false);
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v) * Speed;

        //����Ȯ�� ���̱׸���
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
        }
        else {
            scanObject = null;
        }
    }
}
