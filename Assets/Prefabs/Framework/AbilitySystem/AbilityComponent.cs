using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    [SerializeField] Ability[] InitialAbility;
    public delegate void OnNewAbilityAdded(Ability ability);
    public delegate void OnStaminaChange(float newAmount, float maxAmount);

    private List<Ability> abilities = new List<Ability>();
    public event OnNewAbilityAdded onNewAbilityAdded;
    public event OnStaminaChange onStaminaChange;

    [SerializeField] float stamina = 100;
    [SerializeField] float maxStamina = 100;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Ability ability in InitialAbility)
        {
            // abilities.Add(ability);
            GiveAbility(ability);
        }
    }

    void GiveAbility(Ability ability)
    {
        Ability newAbility = Instantiate(ability);
        newAbility.InitAbility(this);
        abilities.Add(newAbility);
        onNewAbilityAdded?.Invoke(newAbility);
    }
    // Update is called once per frame

    public void ActivateAbility(Ability abilityToActivate)
    {
        if (abilities.Contains(abilityToActivate))
        {
            abilityToActivate.ActivateAbility();
        }
    }

    float getStamina()
    {
        return stamina;
    }

    public bool TryConsumeStamina(float staminaToConsume)
    {
        if (stamina <= staminaToConsume) return false;
        onStaminaChange?.Invoke(stamina, maxStamina);
        return true;
    }
}
