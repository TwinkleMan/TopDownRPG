using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    private Animator animator;
    public Sprite emptyChest;
    public int coinsAmount = 10;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void onCollect()
    {
        if (!collected)
        {
            collected = true;

            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coinsAmount;
            GameManager.instance.ShowText($"+ {coinsAmount} coins", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f) ;
            
            animator.SetBool("collected", true);

        }
    }
}
