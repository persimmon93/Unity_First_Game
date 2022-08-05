using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text textCoolDown;
    [SerializeField]
    private CharacterCombat combatData;

    // Start is called before the first frame update
    void Start()
    {
        imageCooldown.fillAmount = 0f;
        textCoolDown.text = Mathf.RoundToInt(combatData.attackCooldown).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        imageCooldown.fillAmount = Mathf.Clamp(1 / combatData.attackCooldown, 0, 1);
        if (Mathf.RoundToInt(combatData.attackCooldown) > 0)
        {
            //imageCooldown.fillAmount = combatData.attackCooldown;
            textCoolDown.text = Mathf.RoundToInt(combatData.attackCooldown).ToString();
        }
        else
        {
            //imageCooldown.fillAmount = 0;
            textCoolDown.text = "";
        }
    }
}
