using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class optionsMenu : MonoBehaviour {

    public Dropdown qualitySetting;

    public void qualityChange()
	{
        if(qualitySetting.value == 0)
        {
            QualitySettings.SetQualityLevel(5, true);
		}
		else if(qualitySetting.value == 1)
		{
            QualitySettings.SetQualityLevel(3, true);
		}
		else if(qualitySetting.value == 2)
		{
            QualitySettings.SetQualityLevel(0, true);
        }
    }
}
