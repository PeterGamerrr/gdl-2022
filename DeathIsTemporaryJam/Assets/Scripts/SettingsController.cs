using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour // I KNOW THIS CODE AINT DRY DONT HIT ME PLS.
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject cancel;
    [SerializeField] private GameObject save;
    [SerializeField] private string waitingForInputText = "Waiting...";
    
    [Header("Forwards")]
    [SerializeField] private Button forwardsButton;
    [SerializeField] private InputActionReference forwardsAction;
    [SerializeField] private TMP_Text forwardsDisplay;
    public void RebindForwards()
    {
        startRebinding(forwardsButton, forwardsAction, forwardsDisplay,2);
    }
    
    [Header("Backwards")]
    [SerializeField] private Button backwardsButton;
    [SerializeField] private InputActionReference backwardsAction;
    [SerializeField] private TMP_Text backwardsDisplay;
    public void RebindBackwards()
    {
        startRebinding(backwardsButton,backwardsAction,backwardsDisplay,3);
    }
    
    [Header("Left")]
    [SerializeField] private Button leftButton;
    [SerializeField] private InputActionReference leftAction;
    [SerializeField] private TMP_Text leftDisplay;
    public void RebindLeft()
    {
        startRebinding(leftButton,leftAction,leftDisplay,4);
    }
    
    [Header("Right")]
    [SerializeField] private Button rightButton;
    [SerializeField] private InputActionReference rightAction;
    [SerializeField] private TMP_Text rightDisplay;
    public void RebindRight()
    {
        startRebinding(rightButton,rightAction,rightDisplay,5);
    }
    
    [Header("Pause")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private TMP_Text pauseDisplay;
    public void RebindPause()
    {
        startRebinding(pauseButton, pauseAction, pauseDisplay);
    }
    
    [Header("Attack")]
    [SerializeField] private Button attackButton;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private TMP_Text attackDisplay;
    public void RebindAttack()
    {
        startRebinding(attackButton,attackAction,attackDisplay);
    }

    [Header("Special1")]
    [SerializeField] private Button special1Button;
    [SerializeField] private InputActionReference special1Action;
    [SerializeField] private TMP_Text special1Display;
    public void RebindSpecial1()
    {
        startRebinding(special1Button,special1Action,special1Display);
    }
    
    [Header("Special2")]
    [SerializeField] private Button special2Button;
    [SerializeField] private InputActionReference special2Action;
    [SerializeField] private TMP_Text special2Display;
    public void RebindSpecial2()
    {
        startRebinding(special2Button,special2Action,special2Display);
    }
    
    
    [Header("Special3")]
    [SerializeField] private Button special3Button;
    [SerializeField] private InputActionReference special3Action;
    [SerializeField] private TMP_Text special3Display;
    public void RebindSpecial3()
    {
        startRebinding(special3Button,special3Action,special3Display);
    }
    private void startRebinding(Button button, InputActionReference action, TMP_Text display, int bindingIndex = -1)
    {
        button.interactable = false;
        display.text = waitingForInputText;
        playerInput.SwitchCurrentActionMap("UI");
        action.action.PerformInteractiveRebinding(bindingIndex)
            // .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                display.text = InputControlPath.ToHumanReadableString(
                    action.action.bindings[bindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice);
                
                operation.Dispose();
                button.interactable = true;

                playerInput.SwitchCurrentActionMap("Player");
            })
            .Start();
    }
}
