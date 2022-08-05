using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;  //Our current focus: item, enemy, etc.
    bool isMoving = false;

    public LayerMask movementMask;  //The ground
    Camera cam;
    PlayerMotor motor;
    public CharacterCombat combat;

    public bool aggressiveStance = false;
    public Image aggressiveStanceUI;



    // Start is called before the first frame update
    void Start()
    {
        if (aggressiveStanceUI == null)
        {
            Debug.LogError("Image aggressiveStanceUI is missing. Connect it with UI/Bottom Left/Aggressive Stance");
        }
        if (combat == null)
        {
            combat = GetComponent<CharacterCombat>();
        }
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //This prevents player from moving when clicking inventory or gameobject.
        //Eventsystem current checks if the mouse is hovering over UI. Makes it so that
        //player doesn't move when UI is clicked.
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Move to where we click.
                motor.MoveToPoint(hit.point);
                //Stop focusing any objects;
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                //If we did set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        //WIll automatically attack if focus is enemy.
        if (focus != null && focus.tag == "Enemy")
        {
            EnemyController enemy = focus.GetComponent<EnemyController>();
            enemy.isEnemy = true;
            CharacterStats targetStats = focus.GetComponent<CharacterStats>();
            //Attack the target
            combat.Attack(targetStats);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            aggressiveStance = !aggressiveStance;
            Aggressive();
        }
    }

    //Sets an object as a focus when player clicks it. 
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
            //newFocus.OnFocused(transform);
        }
        //motor.FollowTarget(newFocus);
        newFocus.OnFocused(transform);


        //This is linked to the enenmy.
        if (newFocus.tag == "Enemy" || newFocus.tag == "Neutral")
        {
            GameManager.instance.targetUI.SetActive(true);
            GameManager.instance.targetText.text = newFocus.name;
        } else
        {
            GameManager.instance.targetUI.SetActive(false);
            GameManager.instance.targetText.text = "";
        }

        //This makes sure only neutral are able to be talked with.
        if (newFocus.tag == "Neutral" && !aggressiveStance && newFocus.GetComponent<DialogueTrigger>())
        {
            newFocus.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

        if (newFocus.tag == "Neutral" && aggressiveStance && Input.GetMouseButton(1))
        {
            newFocus.tag = "Enemy";
            newFocus.name = "Enemy";
            newFocus.GetComponent<EnemyController>().isEnemy = true;
        }
    }

    //Removes an object as a focus when player clicks it. 
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();

        GameManager.instance.targetUI.SetActive(false);
        GameManager.instance.targetText.text = "";
    }

    void Aggressive()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        if (aggressiveStance)
        {
            aggressiveStanceUI.color = Color.red;
            stats.damage.AddModifier(2);
            stats.armor.AddModifier(2);
        } else
        {
            aggressiveStanceUI.color = Color.white;
            stats.damage.RemoveModifier(2);
            stats.armor.RemoveModifier(2);
        }
    }
}