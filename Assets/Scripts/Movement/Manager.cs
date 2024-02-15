using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    private static Input input;
    public static void Init(PlayerMovement myPlayer)
    {
        input = new Input();

        input.InGame.Move.performed += ctx =>
        {
            myPlayer.SetMovementDirection(ctx.ReadValue<Vector2>());
        };

        input.InGame.Jump.performed += ctx =>
        {
            myPlayer.Jump();
        };

        input.InGame.Sprint.performed += ctx =>
        {
            myPlayer.Sprint();
        };

        input.InGame.Sprint.canceled += ctx =>
        {
            myPlayer.StopSprint();
        };
    }

    public static void SetGameControls()
    {
        
        input.InGame.Enable();

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
