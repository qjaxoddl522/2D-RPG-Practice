using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    float h;
    float v;
    int lastdir = 0; //마지막으로 누른 방향키
    Vector3 dirVec; //바라보는 방향벡터
    GameObject scanObject; //인식된 오브젝트

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

        if (h != 0 && v != 0) { //마지막으로 누른 방향키가 아닌 쪽은 무시
            if ((lastdir == 1) || (lastdir == 3)) {
                h = 0;
            }
            else {
                v = 0;
            }
        }

        //인식 벡터를 위한 현재 방향
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

        //오브젝트 스캔(스페이스바)
        if (Input.GetButtonDown("Jump") && scanObject != null) {
            Debug.Log("Scanned : " + scanObject.name);
        }

        //애니메이션
        if (anim.GetInteger("hAxisRaw") != h) { //애니메이션의 값과 다르면 isChange로 한번만 실행하고 값을 일치시킴
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

        //벡터확인 레이그리기
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
