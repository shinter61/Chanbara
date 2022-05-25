using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    private int maxHP = 100;
    private int HP = 100;
    public GameObject HPUI;
    private Slider HPBar;
    private GameObject character;
    private Animator charaAnimator;
    // Start is called before the first frame update
    void Start()
    {
        HPBar = HPUI.transform.Find("HPBar").GetComponent<Slider>();
        HPBar.value = 1.0f;
        character = GameObject.Find("unitychan");
        charaAnimator = character.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon") && charaAnimator.GetFloat("Attack") >= 0.2f) {
            WeaponStatus script = other.gameObject.GetComponent<WeaponStatus>();
            HP -= script.Damage;
            HPBar.value = (float)HP / (float)maxHP;
        }
    }
}
