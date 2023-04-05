using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigibody2d;
    private Touch touch;
    private GuiManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GuiManager>();
        rigibody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {

                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;

                rigibody2d.MovePosition((Vector2)transform.position + (touch.deltaPosition * 0.5f * Time.deltaTime));
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("End"))
        {
            gameManager.DisplayFinishScreen();
        }

    }
}
