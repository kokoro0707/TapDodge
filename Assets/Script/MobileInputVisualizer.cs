
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

using UnityEngine.InputSystem;

using UnityEngine.InputSystem.EnhancedTouch;



using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;



public class MobileInputVisualizer : MonoBehaviour

{

    [SerializeField] private float swipeDistance = 80f;

    [SerializeField] private LanePlayer player;



    private Vector2 startPosition;

    private Vector2 currentPosition;



    private bool isTouching;

    private string currentState = "None";



    void OnEnable()

    {

        EnhancedTouchSupport.Enable();

    }



    void OnDisable()

    {

        EnhancedTouchSupport.Disable();

    }



    void Update()

    {

#if UNITY_EDITOR

        UpdateMouseInput();

#else

        UpdateTouchInput();

#endif

    }



    void UpdateMouseInput()

    {

        if (Mouse.current.leftButton.wasPressedThisFrame)

        {

            startPosition = Mouse.current.position.ReadValue();

            currentPosition = startPosition;

            isTouching = true;

        }



        if (Mouse.current.leftButton.isPressed)

        {

            currentPosition = Mouse.current.position.ReadValue();

        }



        if (Mouse.current.leftButton.wasReleasedThisFrame)

        {

            currentPosition = Mouse.current.position.ReadValue();

            CheckGesture();

            isTouching = false;

        }

    }



    void UpdateTouchInput()

    {

        if (Touch.activeTouches.Count == 0) return;



        Touch touch = Touch.activeTouches[0];



        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)

        {

            startPosition = touch.screenPosition;

            currentPosition = startPosition;

            isTouching = true;

        }



        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)

        {

            currentPosition = touch.screenPosition;

        }



        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)

        {

            currentPosition = touch.screenPosition;

            CheckGesture();

            isTouching = false;

        }

    }



    void CheckGesture()

    {

        Vector2 diff = currentPosition - startPosition;



        if (diff.magnitude < swipeDistance)
        {

            currentState = "Tap";

            float screenX = currentPosition.x;


            if (screenX < Screen.width / 3f)
            {

                player.MoveToLane(0); // 左

            }

            else if (screenX < Screen.width * 2f / 3f)
            {

                player.MoveToLane(1); // 中央

            }

            else
            {

                player.MoveToLane(2); // 右

            }



            return;

        }



        currentState = "Swipe";

    }



    void OnGUI()

    {

        GUIStyle style = new GUIStyle(GUI.skin.label);

        style.fontSize = 32;

        style.normal.textColor = Color.white;



        GUI.Label(new Rect(20, 20, 500, 50), "State : " + currentState, style);

    }

}

