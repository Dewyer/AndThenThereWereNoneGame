using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShellControll : MonoBehaviour {

    public List<GameObject> AllShells;
    private List<GameObject> EmptyShells = new List<GameObject>();
    private List<GameObject> FullShells = new List<GameObject>();

    private int _shellCount;

    public int MaxShells;
    public int ShellCount
    {
        get
        {
            return _shellCount;
        }

        set
        {

            if (value <= EmptyShells.Count && value >= 0)
            {
                _shellCount = value;
                ShellCountChanged();
            }
        }
    }

    public void ShellCountChanged()
    {
        EmptyShells.ForEach(yy => yy.SetActive(true));
        EmptyShells.GetRange(0, ShellCount).ForEach(yy => yy.gameObject.SetActive(false));
        FullShells.ForEach(yy => yy.SetActive(false));
        FullShells.GetRange(0, ShellCount).ForEach(yy => yy.gameObject.SetActive(true));
    }

	// Use this for initialization
	void Start () {
        //AllShells = new List<GameObject>();
        //AllShells.AddRange(GameObject.FindGameObjectsWithTag("BulletShell").ToList().Where(x=>x.transform.parent==gameObject.transform));

        EmptyShells = AllShells.GetRange(0, AllShells.Count/2);
        FullShells = AllShells.GetRange(AllShells.Count / 2, AllShells.Count / 2);

        ShellCount = 0;
        MaxShells = EmptyShells.Count;
	}
	
}
