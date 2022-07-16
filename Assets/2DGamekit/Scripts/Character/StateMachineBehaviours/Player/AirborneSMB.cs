using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{
    public class AirborneSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateNoTransitionUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.UpdateFacing();
            m_MonoBehaviour.UpdateJump();
            m_MonoBehaviour.AirborneHorizontalMovement();
            m_MonoBehaviour.AirborneVerticalMovement();
            m_MonoBehaviour.CheckForGrounded();
            m_MonoBehaviour.CheckForHoldingGun();
            if(m_MonoBehaviour.CheckForMeleeAttackInput())
                m_MonoBehaviour.MeleeAttack ();
            m_MonoBehaviour.CheckAndFireGun ();
            m_MonoBehaviour.CheckForCrouching ();
            if (m_MonoBehaviour.CheckForJumpInput()) {
                if ( m_MonoBehaviour.StillHasJumps()) {
                    if (m_MonoBehaviour.IsGrabbingWall()) {
                        float movement = m_MonoBehaviour.StopGrabbingWall();
                        m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpSpeed);
                        m_MonoBehaviour.SetHorizontalMovement(movement * m_MonoBehaviour.wallJumpMultiplier);
                        // m_MonoBehaviour.RemoveJump();
                    } else {
                        if (SkillsManager.Instance.IsSkillActive(Skill.SkillType.DoubleJump))  {
                            
                            m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpSpeed);
                            m_MonoBehaviour.RemoveJump(); 

                        } else if (SkillsManager.Instance.IsSkillActive(Skill.SkillType.Glide)){
                            m_MonoBehaviour.StartGliding();
                        }

                    }

                } else if (SkillsManager.Instance.IsSkillActive(Skill.SkillType.Glide)) {
                    m_MonoBehaviour.StartGliding();
                }
            } 

                
                
            
            
            
        }
    }
}