using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public enum SlimeColor
{
  Red, Blue
}
public class TinySlimeControl : MonoBehaviour
{
  public SlimeColor slimeColor;
  private InputController inputs;
  public SpriteRenderer spriteRend;
  private Animator anim;
  private Rigidbody2D rb;
  private EdgeDetector edge;
  private const float slideForce = 700;
  // private  const float slideVelo = 3;
  private const float jumpVelocity = 5.5f;
  private const float dashVelocity = 8;
  private const float sideVeloLimit = 3;
  private const float slideVeloLimit = 3;
  private const float dashDelay = 0.5f;
  private const float wallJumpDelay = 0.5f;

  private float movePhase = 0;
  private bool jumpStock = true;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    edge = GetComponentInChildren<EdgeDetector>();
    anim = GetComponentInChildren<Animator>();
  }
  private void Start()
  {
    Time.timeScale = 1f;
    inputs = new InputController(controlSet);
  }
  private void Update()
  {
    UpdateControl();
    UpdateAnimParameters();
  }
  private void UpdateAnimParameters()
  {

    if (edge.HitBottom() && anim.GetFloat("prevVeloY") < -1)
    {
      // if(anim.GetFloat("prevVeloY") < -5)
      //   VibrateJoy(2);
      // else
      //   VibrateJoy(1);
      VibrateJoy(Mathf.Lerp(0, 1, (anim.GetFloat("prevVeloY")) / (-6)));
    }

    anim.SetFloat("veloX", Mathf.Abs(rb.velocity.x));
    anim.SetBool("onGround", edge.HitBottom());
    anim.SetFloat("prevVeloY", anim.GetFloat("veloY"));
    anim.SetFloat("veloY", rb.velocity.y);
    anim.SetBool("onWall", edge.HitLeft() || edge.HitRight());
  }

  public ControlSet controlSet;

  // bool prevIsLeft, prevIsRight, prevIsJump;
  private float dashStamp = 0;
  private bool CanDash()
  {
    if (dashStamp == 0)
      return true;
    if (Time.time - dashStamp > dashDelay)
    {
      return true;
    }
    return false;
  }
  private bool IsDashing()
  {
    return !CanDash();
  }

  private float wallJumpStamp = 0;
  private bool IsWallJumping()
  {
    if (wallJumpStamp == 0)
      return false;
    return !(Time.time - wallJumpStamp > wallJumpDelay);
  }

  private void WallJump(Vector2 velo)
  {
    wallJumpStamp = Time.time;
    anim.Play("Wall Jump");
    spriteRend.flipX = velo.x < 0;
    rb.velocity = velo;
  }

  private void Dash(Vector2 velo)
  {
    dashStamp = Time.time;
    rb.velocity = velo;
  }

  private void UpdateControl()
  {
    inputs.ExtractInputs();

    if (IsDashing())
      return;

    if (IsWallJumping())
      return;

    if (ControlDash())
      return;

    if (ControlJump())
      return;

    if (IsWallJumping())
      return;
    if (ControlMove())
      return;




    if (Mathf.Abs(rb.velocity.x) > sideVeloLimit)
    {
      TrimVeloX(sideVeloLimit);
    }

    if (edge.HitBottom() && Mathf.Abs(rb.velocity.x) > slideVeloLimit)
    {
      TrimVeloX(slideVeloLimit);
    }
  }

  private void TrimVeloX(float limit)
  {
    Vector2 velo = rb.velocity;
    if (velo.x > 0)
      velo.x = limit;
    else
      velo.x = -limit;
    rb.velocity = velo;
  }
  private bool ControlDash()
  {
    if (CanDash())
    {
      if (inputs.isDashLeft)
      {
        Dash(Vector2.left * dashVelocity);
        spriteRend.flipX = true;
        anim.Play("Dash Start");
        return true;
      }
      else if (inputs.isDashRight)
      {
        Dash(Vector2.right * dashVelocity);
        spriteRend.flipX = false;
        anim.Play("Dash Start");
        return true;
      }
      else if (inputs.isDashUp && edge.HitBottom())
      {
        //jump dash
        // Dash(2*Vector2.up*jumpVelocity);
        // return true;
      }
    }
    return false;
  }
  private bool OnAir()
  {
    return edge.NoHit();
  }
  private float airJumpStamp = -1;
  private const float airJumpDelay = 0.6f;
  private void AirJump()
  {
    Vector2 velo = rb.velocity;
    velo.y = jumpVelocity;
    rb.velocity = velo;
    // anim.Play("Jump",0,0);
    jumpStock = false;
  }
  private bool ControlJump()
  {
    if (inputs.isJump)
    {
      if (!OnAir() && edge.HitTop()==false)
        jumpStock = true;

      if (OnAir() && jumpStock && (Time.time - airJumpStamp) > airJumpDelay)
      {
        AirJump();
        return false;
      }

      print("before hit bottom");
      if (edge.HitBottom())
      {
        print("in hit bottom");
        if (inputs.isLeft)
        {
          rb.velocity = new Vector2(-jumpVelocity / Mathf.Sqrt(2), jumpVelocity);
        }
        else if (inputs.isRight)
        {
          rb.velocity = new Vector2(jumpVelocity / Mathf.Sqrt(2), jumpVelocity);
        }
        else
        {
          rb.velocity = Vector2.up * jumpVelocity;
        }

        airJumpStamp = Time.time;
        return false;
      }

      if (edge.HitLeft() && rb.velocity.y < -0.4f && !edge.HitTagLeft("TinySlime"))
      {
        WallJump(new Vector2(jumpVelocity / Mathf.Sqrt(2), jumpVelocity));
        return true;
      }

      if (edge.HitRight() && rb.velocity.y < -0.4f && !edge.HitTagRight("TinySlime"))
      {
        WallJump(new Vector2(-jumpVelocity / Mathf.Sqrt(2), jumpVelocity));
        return true;

      }
    }
    return false;
  }
  private bool ControlMove()
  {
    // float freq = 10;
    // movePhase += Time.deltaTime*freq;
    // float amp = 1;
    // float adjust = 0.3f+Mathf.Sin(movePhase)*amp;

    // if(edge.HitBottom() == false)
    //   adjust = 1;
    // if(adjust < 0){
    //   // adjust = 0*-Mathf.Sqrt(Mathf.Abs(adjust));
    //   // anim.SetFloat("veloX",0);
    // }
    float adjust = 1;

    if (inputs.isLeft)
    {
      // Vector2 velo = rb.velocity;
      // velo.x = -slideVelo;
      // rb.velocity = velo;

      if (rb.velocity.x > 0 && edge.HitBottom())
      {
        Vector2 velo = rb.velocity;
        velo.x = 0;
        rb.velocity = velo;
      }
      rb.AddForce(Vector2.left * slideForce * adjust * Time.deltaTime);
      // if(!IsWallJumping())
      spriteRend.flipX = true;

    }
    else if (inputs.isRight)
    {
      // Vector2 velo = rb.velocity;
      // velo.x = slideVelo;
      // rb.velocity = velo;
      if (rb.velocity.x < 0 && edge.HitBottom())
      {
        Vector2 velo = rb.velocity;
        velo.x = 0;
        rb.velocity = velo;
      }
      rb.AddForce(Vector2.right * slideForce * adjust * Time.deltaTime);

      // if(!IsWallJumping())
      spriteRend.flipX = false;
    }
    else
    {
      movePhase = 0;
    }

    return false;
  }

  void VibrateJoy(float level)
  {
    if (inputs.joy == null)
      return;
    int duration = (int)Mathf.Lerp(20, 60, level);
    float amp = level;
    inputs.joy.SetRumble(160, 320, amp, duration);
  }

  public SlimeColor getSlimeColor()
  {
    return slimeColor;
  }
}
