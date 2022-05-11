using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Player")]
    public Car player;
    public float baseSpeed;

    [Header("GameObj")]
    public Car[] car;
    public Transform[] target;

    private void Awake()
    {
       if(instance == null)
            instance = this;

        SpeedSet();
        player.player = true;
    }

    void SpeedSet()
    {
        for(int i=0;i<car.Length;i++)
        {
            car[i].carSpeed = Random.Range(baseSpeed, baseSpeed + 0.5f);
        }
    }

}
