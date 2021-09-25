using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTest : MonoBehaviour
{
    public float xInput;
    public float yInput;
    public bool isIdle;
    public bool isWalking;

    private void Update()
    {
        EventHandler.CallMovementEvent(xInput, yInput, isWalking, isIdle);
    }
}
