using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject unityChan;
    private float moveSpeed = 5.0f;
    private float rotateSpeed = 10.0f;
    private Animator animator;
    private float FingerPosX0; //タップし、指が画面に触れた瞬間の指のx座標
    private float FingerPosY0; //タップし、指が画面に触れた瞬間の指のy座標
    private float preAngle;
    private float FingerPosXNow; //現在の指のx座標
    private float FingerPosYNow; //現在の指のy座標
    private float PosDiff=0.5f; //座標の差のいき値
    private float buttonWidth = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
		animator = unityChan.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FingerPosX0 = 290;
            FingerPosY0 = 100;
            preAngle = (float)unityChan.transform.localEulerAngles.y;
        }
        
        if (Input.GetMouseButton(0))
        {
            FingerPosXNow = Input.mousePosition.x;
            FingerPosYNow = Input.mousePosition.y;
            if (FingerPosXNow >= FingerPosX0 + buttonWidth || FingerPosXNow <= FingerPosX0 - buttonWidth ||
                FingerPosYNow >= FingerPosY0 + buttonWidth || FingerPosYNow <= FingerPosY0 - buttonWidth
            ) { return; }
            
            float diffX = 0.0f, diffY = 0.0f;
            diffX = FingerPosXNow - FingerPosX0;
            diffY = FingerPosYNow - FingerPosY0;
            diffX = System.Math.Max(System.Math.Min(diffX, PosDiff), -PosDiff);
            diffY = System.Math.Max(System.Math.Min(diffY, PosDiff), -PosDiff);

            unityChan.transform.Rotate(0, Time.deltaTime * diffY * rotateSpeed, 0);

            animator.SetFloat("Speed", 0.2f);

            unityChan.transform.Translate(Time.deltaTime * new Vector3(diffX, 0, diffY) * moveSpeed);
        }
    }
}
