using UnityEngine;
using UnityEditor;
using System.Collections;

public class AlphaNumericSort : BaseHierarchySort 
{
    public override int Compare(GameObject lhs, GameObject rhs)
    {
        if (lhs == rhs)
            return 0;
        else if (lhs == null)
            return -1;
        else if (rhs == null)
            return 1;
        else
            return EditorUtility.NaturalCompare(lhs.name, rhs.name);
    }
}
