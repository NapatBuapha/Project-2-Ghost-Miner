using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanternState
    {
        Attach,
        UnAttach,
        Hanging
    }

public class LanternCore : MonoBehaviour
{

    Rigidbody2D rb;
    public LanternState lanternState { get; private set; }
    [SerializeField] private Transform playerLanternPosition;
    ThrownManager thrownManager; //เชื่อมเข้ากับตัวเช็คการโยนสำหรับการสั่งให้มันเริ่มโยนได้
    float passableTime = 2;
    public bool pickAble { get; private set; }

    //สำหรับระบบแขวน

    Transform hangingPos;

    void Start()
    {
        pickAble = false;
        lanternState = LanternState.Attach;
        thrownManager = GameObject.Find("[MANAGE] LanternThrownManager").GetComponent<ThrownManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (lanternState)
        {
            case LanternState.Attach:
                AttachState();
                break;
            case LanternState.UnAttach:
                pickAble = true;
                break;
            case LanternState.Hanging:
                pickAble = true;
                HanggingState();
                break;
        }
    }

    void AttachState()
    {
        pickAble = false;
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Lantern"),
        LayerMask.NameToLayer("Player"),
        true);
        Vector2 targetPos = playerLanternPosition.position;
        rb.MovePosition(targetPos);
    }

    void HanggingState()
    {
        gameObject.transform.rotation = hangingPos.rotation;
        rb.MovePosition(hangingPos.position);
    }

    public void SetHaggingTransform(Transform tran_)
    {
        hangingPos = tran_;
    }

    public void SwitchState(LanternState state)
    {
        lanternState = state;
    }

    public IEnumerator PassableCounter()
    {
        Debug.Log("Still Passable");
        yield return new WaitForSeconds(passableTime);
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Lantern"),
        LayerMask.NameToLayer("Player"),
        false);
        Debug.Log("non Passable");

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pickAble)
        {
            SwitchState(LanternState.Attach);
        }
    }

}
