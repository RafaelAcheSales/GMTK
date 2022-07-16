using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class SkillsManager : Singleton<SkillsManager>
{
    public List<Tuple<Skill.SkillType, Skill>> skills = new List<Tuple<Skill.SkillType, Skill>>();
    
    public int skillPoints = 0;

    public Color lockedColor;
    public Color unlockedColor;
    public Color activeColor;
    public AudioSource audioSource;
    private void Start() {
        foreach (Skill skill in GetComponentsInChildren<Skill>()) 
            skills.Add(new Tuple<Skill.SkillType, Skill>(skill.skillType, skill));
        audioSource = GetComponent<AudioSource>();
    }
    //returns if any skill of type is active
    public bool IsSkillActive(Skill.SkillType skillType) {
        return skills.Any(tuple => tuple.Item1 == skillType && tuple.Item2.skillState == Skill.SkillState.Active);
    }

    
    
    


}
