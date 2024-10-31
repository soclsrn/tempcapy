using System.Collections;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    public Camera Map_cam;
    public float moveSpeed = 5f; // 기본 이동 속도
    public float dashDistance = 2f; // 대쉬 거리
    public float dashCooldown = 1f; // 대쉬 쿨타임
    private bool canDash = true;
    public bool canMove = true;
    public PhotonView pv;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Map_cam = FindAnyObjectByType<Camera>();
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            // 이동 입력 받기
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput.Normalize(); // 대각선 이동 속도 일정하게
            if (canMove)
            {
                moveVelocity = moveInput * moveSpeed;

                if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) // 대쉬 입력 감지
                {
                    Debug.Log("dash");
                    StartCoroutine(Dash());
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void Cam_setting(bool IsMove)
    {
        canMove = IsMove;
        if (IsMove)
        {
            Map_cam.DOOrthoSize(4f, 1f);
            Map_cam.GetComponent<CameraControler>().moveCam = true;
        }
        else
        {
            Map_cam.DOOrthoSize(10f, 1f);
            Map_cam.GetComponent<CameraControler>().moveCam = false;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;

        // 텔레포트 전 작아지는 애니메이션
        transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InOutQuad);
        transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.WorldAxisAdd);//.SetLoops(-1, LoopType.Incremental);

        yield return new WaitForSeconds(0.1f);

        // 텔레포트 위치 계산
        Vector2 dashPosition = new Vector2(transform.position.x, transform.position.y) + moveInput * dashDistance;

        // 플레이어 위치를 목표 위치로 순간 이동
        transform.position = dashPosition;

        // 텔레포트 후 커지는 애니메이션
        transform.DOScale(new Vector3(0.53f, 0.53f, 0.53f), 0.2f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.2f);

        // 쿨타임
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
