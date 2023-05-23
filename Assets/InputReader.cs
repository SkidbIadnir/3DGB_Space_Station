using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;

public class InputReader : MonoBehaviour, InputPlayer.IMovementActions, InputPlayer.IHotbarManagerActions, InputPlayer.IUIActions, InputPlayer.IWeaponActions
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isRollingHash;
    int isPickingUpHash;
    int isChoppingHash;
    int isMiningHash;
    int isInteractingHash;
    int isUsingItemHash;
    int isWeaponDrawHash;

    bool isHotbarActive;
    bool isRolling;
    bool isInventory = false;

    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction<bool> runEvent = delegate { };
    public event UnityAction rollEvent = delegate { };
    public event UnityAction interactEvent = delegate { };
    public event UnityAction inventoryEvent = delegate { };
    public event UnityAction useItemEvent = delegate { };
    public event UnityAction drawWeaponEvent = delegate { };

    public event UnityAction<bool> openHotbarEvent = delegate { };
    public event UnityAction moveHotbarLeftEvent = delegate { };
    public event UnityAction moveHotbarRightEvent = delegate { };

    public event UnityAction<Vector2> navigateEvent = delegate { };
    public event UnityAction leftClickInventoryEvent = delegate { };
    public event UnityAction rightClickInventoryEvent = delegate { };
    public event UnityAction<Vector2> mousePosition = delegate { };
    public event UnityAction closeInventoryEvent = delegate { };
    public event UnityAction continueDialogue = delegate { };
    public event UnityAction switchToInventoryEvent = delegate { };
    public event UnityAction switchToItemBoxEvent = delegate { };
    public event UnityAction loadPreviousInventoryPageEvent = delegate { };
    public event UnityAction loadNextInventoryPageEvent = delegate { };
    public event UnityAction openMapEvent = delegate { };

    public event UnityAction moveWeaponEvent = delegate { };
    public event UnityAction rollWeaponEvent = delegate { };
    public event UnityAction inventoryWeaponEvent = delegate { };
    public event UnityAction sheathWeaponEvent = delegate { };
    public event UnityAction primaryAttackEvent = delegate { };
    public event UnityAction<bool> primaryAttackHoldEvent = delegate { };
    public event UnityAction secondaryAttackEvent = delegate { };
    public event UnityAction specialMoveEvent = delegate { };

    private InputPlayer input;
    private CinemachineInputProvider cameraInput;

    void Start()
    {
        cameraInput = FindObjectOfType<CinemachineInputProvider>();

        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isRollingHash = Animator.StringToHash("isRolling");
        isPickingUpHash = Animator.StringToHash("isPickingUp");
        isChoppingHash = Animator.StringToHash("isChopping");
        isMiningHash = Animator.StringToHash("isMining");
        isInteractingHash = Animator.StringToHash("isInteracting");
        isUsingItemHash = Animator.StringToHash("isUsingItem");
        isWeaponDrawHash = Animator.StringToHash("isWeaponDraw");
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new InputPlayer();
            input.Movement.Enable();
            input.HotbarManager.Enable();
            input.UI.Disable();
            input.Weapon.Disable();
            input.Movement.SetCallbacks(this);
            input.HotbarManager.SetCallbacks(this);
            input.UI.SetCallbacks(this);
            input.Weapon.SetCallbacks(this);

            this.openHotbarEvent += IsHotbarOpen;
            this.rollEvent += IsRollingTrue;
        }
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void DisableAllInput()
    {
        input.Movement.Disable();
        input.HotbarManager.Disable();
        input.UI.Disable();
        input.Weapon.Disable();

        this.openHotbarEvent -= IsHotbarOpen;
        this.rollEvent -= IsRollingTrue;
    }

    public void EnableUIInput()
    {
        cameraInput.enabled = false;
        input.Movement.Disable();
        input.HotbarManager.Disable();
        input.UI.Enable();
        input.Weapon.Disable();


        this.openHotbarEvent -= IsHotbarOpen;
        this.rollEvent -= IsRollingTrue;
    }

    public void DisableUIInput()
    {
        cameraInput.enabled = true;
        input.Movement.Enable();
        input.HotbarManager.Enable();
        input.UI.Disable();
        input.Weapon.Disable();

        this.openHotbarEvent += IsHotbarOpen;
        this.rollEvent += IsRollingTrue;
    }

    public void IsInventoryOpenOrClosed(bool value)
    {
        isInventory = value;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        runEvent.Invoke(context.ReadValueAsButton());
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && animator.GetBool(isRollingHash) == false && animator.GetBool(isInteractingHash) == false)
        {
            rollEvent.Invoke();
        }
    }
    private void IsRollingTrue()
    {
        isRolling = true;
    }
    private void IsRollingFalse()
    {
        isRolling = false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isHotbarActive
            && !isRolling && animator.GetBool(isInteractingHash) == false)
            interactEvent.Invoke();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            inventoryEvent.Invoke();
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isInventory == true)
        {
            leftClickInventoryEvent.Invoke();
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            rightClickInventoryEvent.Invoke();
        }
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            inventoryEvent.Invoke();
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        navigateEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {

    }

    public void OnCancel(InputAction.CallbackContext context)
    {

    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        mousePosition.Invoke(context.ReadValue<Vector2>());
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {

    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {

    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {

    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {

    }

    public void OnContinueDialogue(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isInventory == false)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    public void OnSwitchToInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            switchToInventoryEvent.Invoke();
        }
    }

    public void OnSwitchToItemBox(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            switchToItemBoxEvent.Invoke();
        }
    }

    public void OnLoadPreviousInventoryPage(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            loadPreviousInventoryPageEvent.Invoke();
        }
    }

    public void OnLoadNextInventoryPage(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            loadNextInventoryPageEvent.Invoke();
        }
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isHotbarActive && !isRolling && animator.GetBool(isUsingItemHash) == false)
            useItemEvent.Invoke();
    }

    public void OnMoveCamera(InputAction.CallbackContext context)
    {
        
    }

    public void OnOpenHotbar(InputAction.CallbackContext context)
    {
        openHotbarEvent.Invoke(context.ReadValueAsButton());
    }
    private void IsHotbarOpen(bool isActive)
    {
        isHotbarActive = isActive;
    }

    public void OnMoveHotbarLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isHotbarActive)
            moveHotbarLeftEvent.Invoke();
    }

    public void OnMoveHotbarRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isHotbarActive)
            moveHotbarRightEvent.Invoke();
    }

    public void OnOpenCloseMap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            openMapEvent.Invoke();
        }
    }

    public void OnDrawWeapon(InputAction.CallbackContext context)
    {
        if (animator.GetBool(isWeaponDrawHash) == false)
        {
            input.Movement.Disable();
            input.Weapon.Enable();
            drawWeaponEvent.Invoke();
        }
    }

    public void OnSheathWeapon(InputAction.CallbackContext context)
    {
        if (animator.GetBool(isWeaponDrawHash) == true)
        {
            input.Movement.Enable();
            input.Weapon.Disable();
            sheathWeaponEvent.Invoke();
        }
    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            primaryAttackEvent.Invoke();

    }

    public void OnSecondaryAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            secondaryAttackEvent.Invoke();
    }

    public void OnSpecialMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            specialMoveEvent.Invoke();
    }

    public void OnPrimaryAttackHold(InputAction.CallbackContext context)
    {
        primaryAttackHoldEvent.Invoke(context.ReadValueAsButton());
    }
}
