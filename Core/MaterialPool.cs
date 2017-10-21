using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 将贴图及颜色转换到另一个球
/// </summary>
public class MaterialWorp
{
    private Dictionary<Material, Material> m_Materials =  new Dictionary<Material, Material>();
    public Material worpMaterial { get; private set; }

    public MaterialWorp(Material worpMaterial)
    {
        this.worpMaterial = worpMaterial;
    }
    public Material Worp(Material mat)
    {
        if (!m_Materials.ContainsKey(mat))
        {
            Record(mat);
        }

        return m_Materials[mat];
    }
    public Material[] Worp(params Material[] mats)
    {
        var newMats = new Material[mats.Length];
        for (int i = 0; i < newMats.Length; i++)
        {
            if (!m_Materials.ContainsKey(mats[i])){
                Record(mats[i]);
            }
            newMats[i] = m_Materials[mats[i]];
        }
        return newMats;
    }

    private void Record(Material mat)
    {
        if (!m_Materials.ContainsKey(mat))
        {
            var newMat = new Material(worpMaterial);
            var oldTex = mat.GetTexture("_MainTex");
            var oldColor = mat.GetColor("_Color");
            newMat.SetTexture("_MainTex", oldTex);
            newMat.SetColor("_MainColor", oldColor);
            m_Materials.Add(mat, newMat);
        }
    }

  
}
