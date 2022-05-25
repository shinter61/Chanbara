using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public FixedJoystick movement;
    public FloatingJoystick cameraMovement;
    public float cameraRotateSpeed = 3.0f;
    private GameObject mainCamera;
    private CharacterController characterController;
    private Animator animator;
    private AnimatorStateInfo stateInfo;
    private float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>(); 
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("Speed", movement.Vertical);
        transform.Translate(Time.deltaTime * new Vector3(movement.Horizontal, 0, movement.Vertical) * moveSpeed);
        mainCamera.transform.Translate(Time.deltaTime * new Vector3(movement.Horizontal, 0, movement.Vertical) * moveSpeed);

        Vector3 cameraRotateAngle = new Vector3(cameraMovement.Horizontal, 0, cameraMovement.Vertical);
        mainCamera.transform.RotateAround(transform.position, Vector3.up, cameraRotateAngle.x * cameraRotateSpeed);
        mainCamera.transform.RotateAround(transform.position, -Vector3.right, cameraRotateAngle.z * cameraRotateSpeed);

        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle")) { return; } // ここに到達直後はnormalizedTimeが"Idle"の経過時間を拾ってしまうので、他状態に遷移完了するまではreturnする。
        if (stateInfo.normalizedTime > 0.9f) {
            animator.SetFloat("Attack", 0.0f);
        }
    }

    public void Attack() {
        animator.SetFloat("Attack", 0.2f);
    }
}
