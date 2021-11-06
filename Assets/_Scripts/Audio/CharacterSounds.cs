using System.Collections;
using Components;
using UnityEngine;

namespace _Scripts.Audio
{
    public class CharacterSounds : AudioPlayer
    {
        [field: SerializeField] private AudioClip pickupClip;
        [field: SerializeField] private AudioClip deathClip;
        [field: SerializeField] private AudioClip stepClip;
        [field: SerializeField] private AudioClip attackClip;
        
        [field: SerializeField] private float setSoundCooldown = 0.5f;

        private HealthComponent _healthComponent;
        private AttackComponent _attackComponent;

        private bool _stepSoundCooldown = false;
        private void OnEnable()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _attackComponent = GetComponent<AttackComponent>();
            
            _healthComponent.OnDeath += PlayDeathSound;
            _attackComponent.OnAttack += PlayAttackSound;
        }
        
        public void PlayStepSound()
        {
            if (_stepSoundCooldown == false)
            {
                PlayClipWithVariablePitch(stepClip);
                StartCoroutine(StepCooldown());
            }
        }

        public void PlayPickupItemSound()
        {
            PlayClip(pickupClip);
        }
        
        private void PlayDeathSound()
        {
            PlayClip(deathClip);
        }

        private void PlayAttackSound()
        {
            PlayClip(attackClip);
        }

        private IEnumerator StepCooldown()
        {
            _stepSoundCooldown = true;
            yield return new WaitForSeconds(setSoundCooldown);
            _stepSoundCooldown = false;
        }
    }
}