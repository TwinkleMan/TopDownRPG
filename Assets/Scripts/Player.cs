using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    //private bool movingLeft;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //if (x < 0.0f) movingLeft = true;
        //if (x > 0.0f) movingLeft = false;

        UpdateMotor(new Vector3(x, y, 0));

        //MoveCrosshair();
    }

    public void SwapSprite(int scinID)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[scinID];
    }
    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++) 
            OnLevelUp();
    }
    public void Heal(int amount)
    {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += amount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        GameManager.instance.ShowText("+" + amount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
    }

    //private void MoveCrosshair()
    //{
    //    //Vector2 aim = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    //    Vector2 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    if (aim.magnitude > 0.0f)
    //    {
    //        aim.Normalize();
    //        aim *= 0.3f;

    //        Debug.Log($"movingLeft = {movingLeft.ToString()}");

            

    //        GameManager.instance.crosshair.transform.localPosition = aim;
    //    }
    //}
}
