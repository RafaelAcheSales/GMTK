using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Gamekit2D;
public class SkillsManager : Singleton<SkillsManager>
{
    public List<Tuple<Skill.SkillType, Skill>> skills = new List<Tuple<Skill.SkillType, Skill>>();
    public List<Skill> activeSkills = new List<Skill>();
    
    public int skillPoints = 0;

    public Color lockedColor;
    public Color unlockedColor;
    public Color activeColor;
    public AudioSource audioSource;

    Dictionary<Skill.SkillType, Func<float, bool>> skillActivationCallbacks = new Dictionary<Skill.SkillType, Func<float, bool>>() {
        {Skill.SkillType.MoveSpeed, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().IncreaseMaxSpeed(a); return true; }},
        {Skill.SkillType.AditionalJump, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().AddJump(); return true; }},
        {Skill.SkillType.Dash, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().ReduceDashCooldown(a); return true; }},
        {Skill.SkillType.Shield, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().ReduceShieldCoolDown(a); return true; }},
        {Skill.SkillType.WallGrab, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().IncreaseWallGrabDuration(a); return true; }},
    };

    Dictionary<Skill.SkillType, Func<float, bool>> skillDeactivationCallbacks = new Dictionary<Skill.SkillType, Func<float, bool>>() {
        {Skill.SkillType.MoveSpeed, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().ReduceMaxSpeed(a); return true; }},
        {Skill.SkillType.AditionalJump, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().RemoveJump(); return true; }},
        {Skill.SkillType.Dash, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().IncreaseDashCooldown(a); return true; }},
        {Skill.SkillType.Shield, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().IncreaseShieldCoolDown(a); return true; }},
        {Skill.SkillType.WallGrab, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().ReduceWallGrabDuration(a); return true; }},
    };


    private void Start() {
        foreach (Skill skill in GetComponentsInChildren<Skill>()) 
            skills.Add(new Tuple<Skill.SkillType, Skill>(skill.skillType, skill));
        audioSource = GetComponent<AudioSource>();
        activeSkills.Add(null);
        activeSkills.Add(null);
        activeSkills.Add(null);
    }
    //returns if any skill of type is active
    public bool IsSkillActive(Skill.SkillType skillType) {
        return skills.Any(tuple => tuple.Item1 == skillType && tuple.Item2.skillState == Skill.SkillState.Active);
    }

    public void ChangeSkill(int index, int newSkillIndex) {
        try
        {
            Skill oldSkill = activeSkills[index];
            skillDeactivationCallbacks[oldSkill.skillType](2f);
            oldSkill.skillState = Skill.SkillState.Locked;
            
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        
        Skill newSkill = skills[newSkillIndex].Item2;
        activeSkills[index] = newSkill;
        newSkill.skillState = Skill.SkillState.Active;
        try {
            skillActivationCallbacks[newSkill.skillType](2f);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    


}
