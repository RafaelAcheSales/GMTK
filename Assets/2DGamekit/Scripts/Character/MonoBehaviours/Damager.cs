using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D
{
    public class Damager : MonoBehaviour
    {
        [Serializable]
        public class DamagableEvent : UnityEvent<Damager, Damageable>
        { }


        [Serializable]
        public class NonDamagableEvent : UnityEvent<Damager>
        { }

        //call that from inside the onDamageableHIt or OnNonDamageableHit to get what was hit.
        public Collider2D LastHit { get { return m_LastHit; } }

        public int damage = 1;
        public Vector2 offset = new Vector2(1.5f, 1f);
        public Vector2 size = new Vector2(2.5f, 1f);
        [Tooltip("If this is set, the offset x will be changed base on the sprite flipX setting. e.g. Allow to make the damager alway forward in the direction of sprite")]
        public bool offsetBasedOnSpriteFacing = true;
        [Tooltip("SpriteRenderer used to read the flipX value used by offset Based OnSprite Facing")]
        public SpriteRenderer spriteRenderer;
        [Tooltip("If disabled, damager ignore trigger when casting for damage")]
        public bool canHitTriggers;
        public bool disableDamageAfterHit = false;
        [Tooltip("If set, the player will be forced to respawn to latest checkpoint in addition to loosing life")]
        public bool forceRespawn = false;
        [Tooltip("If set, an invincible damageable hit will still get the onHit message (but won't loose any life)")]
        public bool ignoreInvincibility = false;
        public LayerMask hittableLayers;
        public DamagableEvent OnDamageableHit;
        public NonDamagableEvent OnNonDamageableHit;

        protected bool m_SpriteOriginallyFlipped;
        protected bool m_CanDamage = true;
        protected bool m_hasDamaged = false;
        protected ContactFilter2D m_AttackContactFilter;
        public Collider2D[] m_AttackOverlapResults = new Collider2D[10];
        protected Transform m_DamagerTransform;
        protected Collider2D m_LastHit;

        void Awake()
        {
            m_AttackContactFilter.layerMask = hittableLayers;
            m_AttackContactFilter.useLayerMask = true;
            m_AttackContactFilter.useTriggers = canHitTriggers;

            if (offsetBasedOnSpriteFacing && spriteRenderer != null)
                m_SpriteOriginallyFlipped = spriteRenderer.flipX;

            m_DamagerTransform = transform;
        }

        public void EnableDamage()
        {
            m_CanDamage = true;
        }

        public void DisableDamage()
        {
            m_CanDamage = false;
        }
        private void OnTriggerStay2D(Collider2D other) {
            if (!m_CanDamage)
                return;
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            m_AttackOverlapResults[0] = other;
            m_hasDamaged = true;
            
        }
        IEnumerator PauseDamage() {
            m_CanDamage = false;
            yield return new WaitForSeconds(2f);
            m_CanDamage = true;
        }

        void FixedUpdate()
        {
            if (!m_CanDamage) return;
            if (!m_hasDamaged) return;
            m_LastHit = m_AttackOverlapResults[0];
            Damageable damageable = m_LastHit.GetComponent<Damageable>();

            if (damageable)
            {
                OnDamageableHit.Invoke(this, damageable);
                damageable.TakeDamage(this, ignoreInvincibility);
                StartCoroutine(PauseDamage());
                if (disableDamageAfterHit)
                    DisableDamage();
                m_hasDamaged = false;
            }
            else
            {
                OnNonDamageableHit.Invoke(this);
            }
            
        }
    }
}
