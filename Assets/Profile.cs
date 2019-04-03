using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Profile",menuName ="Scriptable/Profile")]
public class Profile : ScriptableObject
{

    public string ImageSource;
    public string Title;
    public string Description;

    public Profile() { }

    public Profile(string image_source,string title, string desctiption)
    {
        ImageSource = image_source;
        Title = title;
        Description = desctiption;
    }
}
