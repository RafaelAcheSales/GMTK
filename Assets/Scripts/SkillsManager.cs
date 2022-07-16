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
        {Skill.SkillType.MoveSpeed, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().MultiplyMaxSpeed(a); return true; }},
        {Skill.SkillType.AditionalJump, (float a) => {  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().AddJump(); return true; }},

    };


    private void Start() {
        foreach (Skill skill in GetComponentsInChildren<Skill>()) 
            skills.Add(new Tuple<Skill.SkillType, Skill>(skill.skillType, skill));
        audioSource = GetComponent<AudioSource>();
    }
    //returns if any skill of type is active
    public bool IsSkillActive(Skill.SkillType skillType) {
        return skills.Any(tuple => tuple.Item1 == skillType && tuple.Item2.skillState == Skill.SkillState.Active);
    }

    public void ChangeSkill(int index, Skill newSkill) {
        Skill oldSkill = activeSkills[index];
        

    }

    


}
