using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentView : MonoBehaviour {

    public Profile mProfile;

    [SerializeField]
    private Image profileImage;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(GetImage());
        title.text = mProfile.Title;
        description.text = mProfile.Description;
    }

    IEnumerator GetImage()
    {
        WWW www = new WWW(mProfile.ImageSource);
        yield return www;
        profileImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
