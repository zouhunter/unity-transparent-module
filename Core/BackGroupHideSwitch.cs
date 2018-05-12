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
    private List<string> transparentItems = new List<string>();
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
                if (rds != null)
                {
                    renderList.AddRange(rds);
                }
            }
            item.renderOperate = new RendererOperation(item.worpmat, renderList.ToArray());
            operates[item.key] = item;
        }
    }

    /// <summary>
    /// 透明指定模块
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="autoShow"></param>
    public void TransparentGroup(string item, bool autoShow = false)
    {
        if (!ContainModule(item)) return;

        if (autoShow)
        {
            ReverLastGroups();
        }

        if (item != null)
        {
            TransparentGroupItem(item);
        }
    }

    /// <summary>
    /// 透明除指定模块所有模块
    /// </summary>
    /// <param name="arg0"></param>
    public void TranparentExceptGroup(string key)
    {
        if (!ContainModule(key)) return;

        for (int i = 0; i < itemList.Count; i++)
        {
            var item = itemList[i].key;
            if (item != key)
            {
                TransparentGroupItem(itemList[i].key);
            }
            else
            {
                RevertGroupItem(key);
            }
        }
    }

    /// <summary>
    /// 显示所有被透明模块
    /// </summary>
    public void ReverLastGroups()
    {
        foreach (var item in transparentItems)
        {
            RevertGroupItem(item);
        }
        transparentItems.Clear();
    }

    private bool ContainModule(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            var gitem = itemList.Find(x => x.key == key);
            if (gitem != null)
            {
                return true;
            }
        }
        return false;
    }

    private void RevertGroupItem(string item)
    {
        if (operates.ContainsKey(item))
        {
            operates[item].renderOperate.Recovery();
        }
        if (transparentItems.Contains(item))
        {
            transparentItems.Remove(item);
        }
    }

    public void TransparentGroupItem(string item)
    {
        if (operates.ContainsKey(item))
        {
            operates[item].renderOperate.WorpRenderers();
        }
        if (!transparentItems.Contains(item))
        {
            transparentItems.Add(item);
        }
    }
}