using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class cooldown_example : MonoBehaviour
{

    private SpriteRenderer s_render;
    [SerializeField] private float red_colorStay = 1f;
    public float currentTimeInGame;
    public float lastTimeDmgTaken;


    private void Awake()
    {
        s_render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        change_color();
    }

    private void change_color()
    {
        currentTimeInGame = Time.time;

        if (currentTimeInGame > lastTimeDmgTaken + red_colorStay)
        {
            if (s_render.color != Color.white) s_render.color = Color.white;
        }
    }



    public void take_damage()
    {
        s_render.color = Color.red;
        lastTimeDmgTaken = Time.time;

    }

    private void off_dmg()
    {
        s_render.color = Color.white;
    }
}
