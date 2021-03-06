﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class P2_Controls : MonoBehaviour
{

    public static int MoveSpeed = 4;
    public static float Health = 1;
    Rigidbody2D rb2d;
    public GameObject ShootingPoint;
    public GameObject Bullet;
    public GameObject ShieldPrefab;
    public Transform ShieldPoint;
    public Slider HealthSlider;
    bool isHoldingRB;
    bool isHoldingLB;

    Vector3 StartPos;
    public static int NumberOfShields;
    public Button Shield2_Button;
    List<float> BoosterXPos = new List<float>();


    void Start()
    {
        MoveSpeed = 4;
        rb2d = GetComponent<Rigidbody2D>();
        BoosterXPos.Add(-5f);
        BoosterXPos.Add(5f);
        NumberOfShields = 2;
        Health = 1;
        StartPos = transform.position;
    }

    void Update()
    {
        if (P1_Controls.Health <= 0)
            rb2d.velocity = new Vector2(0, 0);
        Loop();
        HealthStatus();
        Movement();
        ShieldButtonStatus();
        TimeUp();
    }

    public void OnPointUpRightButton()
    {
        isHoldingRB = false;
    }
    public void onPointerDownRightButton()
    {
        isHoldingRB = true;
    }

    public void OnPointUpLeftButton()
    {
        isHoldingLB = false;
    }
    public void onPointerDownLeftButton()
    {
        isHoldingLB = true;
    }

    public void Shield()
    {
        if (NumberOfShields <= 3)
        {
            GameObject ShieldClone;
            ShieldClone = Instantiate(ShieldPrefab, ShieldPoint.position, Quaternion.identity) as GameObject;
            NumberOfShields++;
            Shield2_Button.interactable = true;
        }
        else
        {
            Shield2_Button.interactable = false;
        }
    }
    public void Shoot()
    {
        GameObject BulletClone;
        BulletClone = Instantiate(Bullet, ShootingPoint.transform.position, Quaternion.Euler(0, 0, -90f)) as GameObject;
    }

    public void HealthStatus()
    {
        HealthSlider.value = Health;
        if (Health <= 0)
        {
            GameManager.gameOver = true;
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            GameManager.GameOver.SetActive(true);
            GetComponent<SpriteRenderer>().DOFade(0, 1f);
            if (GameManager.P1_GameOverText != null)
            {
                GameManager.P1_GameOverText.text = "You Win!";
            }
            if (GameManager.P2_GameOverText != null)
            {
                GameManager.P2_GameOverText.text = "You Lose!";
            }
            GameManager.GameOn.SetActive(false);
        }
    }

    public void Movement()
    {
        if (isHoldingRB)
        {
            rb2d.velocity = new Vector2(MoveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
        }
        else if (isHoldingLB)
        {
            rb2d.velocity = new Vector2(-MoveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetBool("P2_isRunning", false);
        }
    }
    public void Loop()
    {
        if (transform.position.x <= -3)
        {
            transform.position = new Vector2(-(transform.position.x + 0.1f), transform.position.y);
        }
        else if (transform.position.x >= 3)
        {
            transform.position = new Vector2(-(transform.position.x - 0.1f), transform.position.y);
        }
    }

    void ShieldButtonStatus()
    {
        if (NumberOfShields <= 3)
        {
            Shield2_Button.interactable = true;
        }
        else
        {
            Shield2_Button.interactable = false;
        }
    }
    public void TimeUp()
    {
        if (GameManager.timeLeft <= 0)
        {
            GetComponent<Animator>().SetBool("P2_isRunning", false);
            rb2d.velocity = new Vector2(0, 0);
            transform.position = StartPos;
            GetComponent<Animator>().SetBool("P2_invoking", true);
        }
    }

}

