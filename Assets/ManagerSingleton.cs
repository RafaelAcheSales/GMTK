using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSingleton : Singleton<ManagerSingleton>
{
    public List<SkillsManager> skillsManagers = new List<SkillsManager>();

    private void Start() {
        foreach (Transform child in transform) {
            skillsManagers.Add(child.GetComponent<SkillsManager>());
        }
    }

    public bool isAnySkillActive(Skill.SkillType skillType) {
        foreach (SkillsManager skillsManager in skillsManagers) {
            if (skillsManager.IsSkillActive(skillType)) {
                return true;
            }
        }
        return false;
    }
}
