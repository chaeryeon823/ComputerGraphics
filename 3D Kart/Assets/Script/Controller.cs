using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{

    Car player;
    Animator playerAni;
    bool onMove;
    public float playerSpeed=20.0f;
    public float rotateSpeed=30.0f;

    private Rigidbody playerRigidbody;

        // Start is called before the first frame update
    private void Start()
    {

        player = GameManager.instance.player;
        playerAni = player.GetComponent<Animator>();
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float input = Input.GetAxis("Vertical");
        Vector3 moveDistance = input * transform.forward * playerSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    void Rotate()
    {
        float input = Input.GetAxis("Horizontal");
        float turn = input * rotateSpeed * Time.deltaTime;

        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
    }


}
