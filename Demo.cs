using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {
    public BackGroupHideSwitch swh;
    private void OnGUI()
    {
        if(GUILayout.Button("Change"))
        {
            swh.OnHideGroupChange("tree");
        }
        if(GUILayout.Button("Reset"))
        {
            swh.OnHideGroupChange(null);
        }
    }
}
