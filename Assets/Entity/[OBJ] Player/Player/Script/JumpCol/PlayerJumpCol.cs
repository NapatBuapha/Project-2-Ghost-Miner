using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerJumpCol : MonoBehaviour
{
    PlayerStateManager player;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D box;


    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStateManager>();
    }

    void Start()
    {
        player.isTouchingGround = true;
    }

    void Update()
    {
        player.isTouchingGround = Physics2D.OverlapAreaAll(box.bounds.min, box.bounds.max, groundLayer).Length > 0;
    }
}
