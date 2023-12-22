using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Child", fileName = "Child", order = 0)]
public class Child : ScriptableObject
{
    public int index;
    public string childName; // first name  + last name
    public bool onBlacklist;
}
