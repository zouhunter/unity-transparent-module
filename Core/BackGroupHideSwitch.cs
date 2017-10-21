using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public class BackGroupHideSwitch : MonoBehaviour
{
    [System.Serializable]
    public class GroupItem
    {
        public string key;
        public Material worpmat;
        public GameObject[] worp;
        public RendererOperation renderOperate;
    }
    [SerializeField]
    private List<GroupItem> itemList = new List<GroupItem>();
    private string lastItem;
    private RendererOperation renderOperate;
    private Dictionary<string, GroupItem> operates;

    private void Start()
    {
        InitRenderOperate();
    }
    private void InitRenderOperate()
    {
        operates = new Dictionary<string, GroupItem>();
        foreach (var item in itemList)
        {
            var renderList = new List<Renderer>();
            foreach (var go in item.worp)
            {
               var rds = go.GetComponentsInChildren<Renderer>(true);
                if(rds != null)
                {
                    renderList.AddRange(rds);
                }
            }
            item.renderOperate = new RendererOperation(item.worpmat,renderList.ToArray());
            operates[item.key] = item;
        }
    }


    public void OnHideGroupChange(string arg0 = null)
    {
        string item = null;
        if (!string.IsNullOrEmpty(arg0))
        {
            var gitem = itemList.Find(x => x.key == arg0);
            if(gitem != null)
            {
                item = gitem.key;
            }
        }

        if (lastItem != null && lastItem != item)
        {
            ShowGroupItem(lastItem);
        }

        if (item != null && item != lastItem)
        {
            HideGroupItem(item);
        }

        lastItem = item;
    }
    private void ShowGroupItem(string item)
    {
        if(operates.ContainsKey(item))
        {
            operates[item].renderOperate.Recovery();
        }
    }
    public void HideGroupItem(string item)
    {
        if (operates.ContainsKey(item))
        {
            operates[item].renderOperate.WorpRenderers();
        }
    }
}