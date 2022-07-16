using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsTracker : MonoBehaviour
{
    Text skillPointsText;
    void Start()
    {
        skillPointsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        skillPointsText.text = "Skill Points: " + SkillsManager.Instance.skillPoints;
    }
}
