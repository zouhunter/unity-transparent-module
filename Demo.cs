using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {
    public BackGroupHideSwitch swh;
    private void OnGUI()
    {
        if(GUILayout.Button("Change"))
        {
            swh.TransparentGroup("tree");
        }
        if(GUILayout.Button("Reset"))
        {
            swh.TransparentGroup(null);
        }
    }
}
