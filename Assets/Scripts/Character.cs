using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public FixedJoystick movement;
    public FloatingJoystick cameraMovement;
    public float cameraRotateSpeed = 3.0f;
    private GameObject mainCamera;
    private Vector3 offset;
    private Animator animator;
    private AnimatorStateInfo stateInfo;
    private float moveSpeed = 3.0f;
    private Vector3 beforePos;
    private Quaternion beforeRot;

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>(); 
        mainCamera = GameObject.Find("Main Camera");
        offset = transform.position - mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", movement.Vertical);
        Vector3 moveVec = Time.deltaTime * new Vector3(movement.Horizontal, 0, movement.Vertical) * moveSpeed;
        transform.Translate(moveVec);
        mainCamera.transform.position = transform.position - offset;

        // 二本指がPCで使えないので、開発中のみ左右キーでカメラを移動する
		float h = Input.GetAxis ("Horizontal");				// 入力デバイスの水平軸をhで定義
        if (h != 0) {
            mainCamera.transform.RotateAround(transform.position, Vector3.up, h * cameraRotateSpeed);
            offset = transform.position - mainCamera.transform.position;
            Vector3 newRotation = new Vector3(0, mainCamera.transform.eulerAngles.y, 0);
            transform.eulerAngles = newRotation;
        }

        Vector3 cameraRotateAngle = new Vector3(cameraMovement.Horizontal, 0, cameraMovement.Vertical);
        mainCamera.transform.RotateAround(transform.position, Vector3.up, cameraRotateAngle.x * cameraRotateSpeed);
        mainCamera.transform.RotateAround(transform.position, -Vector3.right, cameraRotateAngle.z * cameraRotateSpeed);

        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle")) { return; } // ここに到達直後はnormalizedTimeが"Idle"の経過時間を拾ってしまうので、他状態に遷移完了するまではreturnする。
        if (stateInfo.normalizedTime > 0.9f && stateInfo.IsName("Attack")) {
            animator.SetFloat("Attack", 0.0f);
            transform.position = beforePos; // アニメーション終了時にカメラの中央からずれるのでここで修正
            transform.rotation = beforeRot;
        }
    }

    public void Attack() {
        beforePos = transform.position;
        beforeRot = transform.rotation;
        animator.SetFloat("Attack", 0.2f);
    }
}
