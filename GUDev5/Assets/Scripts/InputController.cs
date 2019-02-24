using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlSet
{
  KeyboardLeft,
  KeyboardArrow,
  JoyRed,
  JoyBlue,
}

public class InputController{
  private ControlSet controlSet;
  public InputController(ControlSet controlSet){
    this.controlSet = controlSet;
    GetAccordingJoy();
  }
  public bool isLeft, isRight, isJump, isDashLeft, isDashRight, isDashUp, isUp;
  public Joycon joy;
  public void ExtractInputs()
  {
    bool isDash = false;
    switch (controlSet)
    {
      case ControlSet.KeyboardArrow:
        isLeft = Input.GetKey("left");
        isRight = Input.GetKey("right");
        isJump = Input.GetKey("up");
        isDash = Input.GetKey(KeyCode.RightShift);
        isDashUp = isDash & isJump;
        //not using is up
        break;
      case ControlSet.KeyboardLeft:
        isLeft = Input.GetKey("a");
        isRight = Input.GetKey("d");
        isJump = Input.GetKey("w");
        isDash = Input.GetKey(KeyCode.LeftShift);
        isDashUp = isDash & isJump;
        //not using is up
        break;
      case ControlSet.JoyRed:
        isRight = joy.GetStick()[1] > 0;
        isLeft = joy.GetStick()[1] < 0;
        isJump = joy.GetButton(Joycon.Button.DPAD_RIGHT);
        isDash = joy.GetButton(Joycon.Button.DPAD_UP);
        isUp = joy.GetStick()[0] < -0.85f;
        isDashUp = isDash && isUp;
        break;
      case ControlSet.JoyBlue:
        isRight = joy.GetStick()[1] < 0;
        isLeft = joy.GetStick()[1] > 0;
        isJump = joy.GetButton(Joycon.Button.DPAD_LEFT);
        isDash = joy.GetButton(Joycon.Button.DPAD_DOWN);
        isUp = joy.GetStick()[0] > 0.85f;
        isDashUp = isDash && isUp;
        break;
      default:
        Debug.LogError("no control set for " + controlSet);
        break;
    }

        isDashLeft = isDash && isLeft;
        isDashRight = isDash && isRight;
  }
  private void GetAccordingJoy()
  {
    joy = null;

    switch (controlSet)
    {
      case ControlSet.JoyBlue:
        joy = JoyconManager.Instance.j[1];
        if (!joy.isLeft)
          joy = JoyconManager.Instance.j[0];
        break;

      case ControlSet.JoyRed:
        joy = JoyconManager.Instance.j[0];
        if (joy.isLeft)
          joy = JoyconManager.Instance.j[1];
        break;
    }
  }
}
