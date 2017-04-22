﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class P1_Controls : MonoBehaviour {

	public static int MoveSpeed = 4;

	public static float Health = 1;
	bool isHoldingRB;
	bool isHoldingLB;

	public   Slider P1_HealthSlider;

	public static int NumberOfShields =2;

	public GameObject Bullet;

	public Transform ShootingPoint;

	public GameObject ShieldPrefab;

	public Transform ShieldPoint;

	public Button Shield1_Button;

	

	List<float> BoosterXPos = new List<float>();

	Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
			rb2d = GetComponent<Rigidbody2D>();	
			BoosterXPos.Add(-5f);
			BoosterXPos.Add(5f);
			NumberOfShields = 2;
			Health =1;
	}
	
	// Update is called once per frame
	void Update () {
		Loop ();
		HealthStatus ();
		Movement ();
					Debug.Log("Shield :" + Health);
					

		   if (NumberOfShields<=3)
        {
            Shield1_Button.interactable = true;
        }
        else
        {
            Shield1_Button.interactable = false;
        }

	}
	public void OnPointUpRightButton(){
			isHoldingRB = false;	 
		}
		public void onPointerDownRightButton () {
			isHoldingRB=true;
		}

	public void OnPointUpLeftButton(){
				isHoldingLB = false;	 
			}
	public void onPointerDownLeftButton () {
				isHoldingLB=true;
			}
	public void Shoot () {
		GameObject BulletClone;
		BulletClone = Instantiate(Bullet,ShootingPoint.position,Quaternion.Euler(0,0,90f)) as GameObject;
		}


	public void Shield () {
		
		if(NumberOfShields <= 3) {
		GameObject ShieldClone;
		ShieldClone = Instantiate(ShieldPrefab,ShieldPoint.position,Quaternion.identity) as GameObject;
		NumberOfShields++;
        Shield1_Button.interactable = true;
        }
        else{
            Shield1_Button.interactable = false;
        }

    }

	public void HealthStatus () {
 		P1_HealthSlider.value = Health;
		if(Health <=0) {
			GameManager.GameOver.SetActive(true);
			GetComponent<SpriteRenderer>().DOFade(0,2f);
			if( GameManager.GameOverText!= null)
				GameManager.GameOverText.text = "You Lose!";
			GameManager.GameOn.SetActive(false);
			//Time.timeScale=0;
		}}

		public void Loop () {
			if(transform.position.x <= -3) {
				transform.position = new Vector2(-(transform.position.x+0.1f),transform.position.y);
			} else if (transform.position.x >= 3 ) {
				transform.position = new Vector2(-(transform.position.x - 0.1f),transform.position.y);
		
		}}
		public void Movement () {

		if(isHoldingRB) {
					rb2d.velocity = new Vector2(MoveSpeed,rb2d.velocity.y);
					transform.localScale = new Vector3(1.5f,1.5f,1.5f);
					GetComponent<Animator>().SetBool("Player1_isRunning",true);
		} else if(isHoldingLB) {
					rb2d.velocity = new Vector2(-MoveSpeed,rb2d.velocity.y);
					transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
					GetComponent<Animator>().SetBool("Player1_isRunning",true);
		} else {
					rb2d.velocity = new Vector2(0,0);
					GetComponent<Animator>().SetBool("Player1_isRunning",false);
		}}
}

