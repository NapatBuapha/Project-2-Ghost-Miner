using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum LanternState
{
    Attach,
    UnAttach,
    Hanging,
    Floating,
    Returning
}

public enum LightState
{
    Normal,
    OverShine
}

public class LanternCore : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator; //For haslantern bool
    public GameObject sprite;
    Rigidbody2D rb;
    public LanternState lanternState { get; private set; }
    public LightState lightState { get; private set; }
    [SerializeField] private Transform playerLanternPosition;
    ThrownManager thrownManager; //เชื่อมเข้ากับตัวเช็คการโยนสำหรับการสั่งให้มันเริ่มโยนได้
    float passableTime = 2f;
    float floatingTime = 5f;
    [SerializeField] private float floatingSpeed = 5f;
    public bool pickAble { get; private set; }
    Collider2D col;

    [SerializeField] private LightArea lampLight;
    float baseLightRadius;
    float baseGra;
    [SerializeField] int targetLayer;

    Transform hangingPos;

    [Header("Void Control")]

    [SerializeField] float voidStopDistance = 10;
    [SerializeField] float voidDelayedTime = 3;

    float soundDelayed;

    void Start()
    {
        pickAble = false;
        lanternState = LanternState.Attach;
        thrownManager = GameObject.Find("[MANAGE] LanternThrownManager").GetComponent<ThrownManager>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        baseGra = rb.gravityScale;
        baseLightRadius = lampLight.radius;
    }


    void Update()
    {
        switch (lanternState)
        {
            case LanternState.Floating:
                pickAble = true;
                FloatingState();
                break;
            case LanternState.Attach:
                AttachState();
                break;
            case LanternState.UnAttach:
                sprite.SetActive(true);
                pickAble = true;
                break;
            case LanternState.Hanging:
                pickAble = true;
                HanggingState();
                break;
            case LanternState.Returning:
                ReturnToPlayerState();
                break;
        }

        if (lanternState == LanternState.Attach)
        {
            sprite.SetActive(false);
            playerAnimator.SetBool("HasLamp", true);
        }
        else
        {
            sprite.SetActive(true);
            playerAnimator.SetBool("HasLamp", false);
        }


        switch (lightState)
        {
            case LightState.OverShine:
                OverShineState();
                break;
            default:
                NormalState();
                break;
        }

        if (soundDelayed > 0)
        {
            soundDelayed -= Time.deltaTime;
        }
    }

    #region Lantern State
    void FloatingState()
    {
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Lantern"),
        LayerMask.NameToLayer("Player"),
        false);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        col.isTrigger = true;


        transform.rotation = Quaternion.Lerp(
        transform.rotation,
        Quaternion.identity,
        Time.deltaTime * 25f);
    }

    void ReturnToPlayerState()
    {
         SwitchState(LightState.Normal);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        col.isTrigger = true;

        Vector2 direction = playerLanternPosition.position - transform.position;
        float distance = direction.magnitude;
        rb.AddForce(direction.normalized * (distance + floatingSpeed) * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
        transform.rotation = Quaternion.identity;
    }

    void HanggingState()
    {
        Physics2D.IgnoreLayerCollision(
        LayerMask.NameToLayer("Lantern"),
        LayerMask.NameToLayer("Player"),
        false);

        gameObject.transform.rotation = hangingPos.rotation;
        rb.MovePosition(hangingPos.position);
        SwitchState(LightState.OverShine);

        //Stop The void
        GameObject movingV = GameObject.FindWithTag("MovingVoid");

        if (Vector3.Distance(transform.position, movingV.transform.position) < voidStopDistance)
        {
            MovingVoid moVoid = movingV.GetComponent<MovingVoid>();
            moVoid.counter = 3;
        }

    }
    public void SetHaggingTransform(Transform tran_)
    {
        hangingPos = tran_;
    }

    #endregion
    #region Light State
    void OverShineState()
    {
        lampLight.UpdateLight(baseLightRadius * 3);
    }
    void NormalState()
    {
        lampLight.UpdateLight(baseLightRadius);
    }
    #endregion


    public void SwitchState(LanternState state)
    {
        soundDelayed = 1;
        lanternState = state;
    }

    public void SwitchState(LightState state)
    {
        lightState = state;
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

    IEnumerator FloatingCounter()
    {
        yield return new WaitForSeconds(floatingTime);
        if (lanternState == LanternState.Floating)
        {
            rb.gravityScale = baseGra;
            SwitchState(LanternState.Attach);
            col.isTrigger = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pickAble)
        {
            Pickup();
        }

        if (collision.gameObject.layer == targetLayer)
        {   
            if (lanternState == LanternState.UnAttach && soundDelayed < 0)
                AudioManager.PlaySound(SoundType.LANTERN_Drop, 0.4f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pickAble)
        {
            Pickup();
        }
    }

    void Pickup()
    {
        rb.gravityScale = baseGra;
        StopCoroutine(FloatingCounter());
        SwitchState(LanternState.Attach);
        SwitchState(LightState.Normal);
        col.isTrigger = false;
    }
    

}
