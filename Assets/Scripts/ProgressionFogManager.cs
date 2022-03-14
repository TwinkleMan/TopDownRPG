using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    //References
    public Grid fogGrid;
    public GameObject portal;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Unveil()
    {
        animator.SetBool("bossDefeated", true);
        fogGrid.gameObject.SetActive(false);
        portal.SetActive(true);
    }
}
