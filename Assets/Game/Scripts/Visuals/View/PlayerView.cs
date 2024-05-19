using System;
using DI;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class PlayerView : MonoBehaviour
    {
        private PlayerUI playerUI;

        [Inject]
        private void Construct(PlayerUI playerUI)
        {
            this.playerUI = playerUI;
        }

        public void SetInitialHealth(string value)
        {
            playerUI.HealthUI.SetInitialHealth(value);
        }

        public void Draw(ComponentToDraw component, string value)
        {
            if (component == ComponentToDraw.HEALTH)
            {
                playerUI.HealthUI.DrawHealth(value);
                return;
            }

            if (!TryGetAbility(component, out AbilityUI ability))
            {
                return;
            }

            ability.Enable();
            ability.Draw(value);
        }

        public void Disable(ComponentToDraw component)
        {
            if (!TryGetAbility(component, out AbilityUI ability))
            {
                return;
            }

            ability.Disable();
        }

        private bool TryGetAbility(ComponentToDraw component, out AbilityUI ability)
        {
            AbilityUI[] abilities = playerUI.AbilityUis;
            foreach (AbilityUI abilityUI in abilities)
            {
                if (abilityUI.ComponentToDraw == component)
                {
                    ability = abilityUI;
                    return true;
                }
            }

            ability = default;
            return false;
        }
    }
}