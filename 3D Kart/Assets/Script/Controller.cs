using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour,IPointerUpHandler,IPointerDownHandler, IDragHandler
{
    public RectTransform pad;
    public RectTransform stick;
    Vector3 playerRotate;

    Car player;
    Animator playerAni;
    bool onMove;
    public float playerSpeed;

        // Start is called before the first frame update
    private void Start()
    {

        player = GameManager.instance.player;
        playerAni = player.GetComponent<Animator>();
        StartCoroutine("PlayerMove");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        stick.localPosition = Vector3.zero;
        playerRotate = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        stick.position = eventData.position;
        stick.localPosition = Vector3.ClampMagnitude(eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);

        playerRotate = new Vector3(0, stick.localPosition.x, 0).normalized;
    }

    public void OnMove()
    {
        StartCoroutine("Acceleration");
        onMove = true;
    }
    public void OffMove()
    {
        StartCoroutine("Braking");
    }
    IEnumerator PlayerMove()
    {
        while(true)
        {
            if(onMove)
            {
                player.transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
                if(Mathf.Abs(stick.localPosition.x)>pad.rect.width*0.2f)
                {
                    player.transform.Rotate(playerRotate * 30 * Time.deltaTime);
                }

                if (Mathf.Abs(stick.localPosition.x) <= pad.rect.width * 0.2f)
                    playerAni.Play("Ani_Forward");
                if (stick.localPosition.x > pad.rect.width * 0.2f)
                    playerAni.Play("Ani_Right");
                if (stick.localPosition.x < pad.rect.width * -0.2f)
                    playerAni.Play("Ani_Left");
            }
            if(!onMove)
            {
                playerAni.Play("Ani_Idle");
            }
            yield return null;
        }

    }

    IEnumerator Acceleration()
    {

        StopCoroutine("Braking");
        while(true)
        {
            playerSpeed += 7 * Time.deltaTime; //초당 7씩 증가

            if (playerSpeed >= player.carSpeed)
                playerSpeed -= 10 * Time.deltaTime;


            yield return null;

        }
    }

    IEnumerator Braking()
    {

        StopCoroutine("Acceleration");
        while (true)
        {
            playerSpeed -= 7 * Time.deltaTime;

            if(playerSpeed<=0)
            {
                playerSpeed = 0;
                onMove = false;
                StopCoroutine("Braking");
            }

            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            OnMove();
        if (Input.GetKeyUp(KeyCode.A))
            OffMove();
    }


}
