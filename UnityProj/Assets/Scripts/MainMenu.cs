using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	
	[System.Serializable]
	public class LevelDefinition {
		public string levelName;
		public string sceneName;
	}
	
	public LevelDefinition[] levelDefinitions;
	
	
	public Rect firstButtonRect = new Rect(0.2f, 0.5f, 0.3f, 0.1f);
	public GUIStyle buttonStyle;
	public Vector2 buttonRectOffsets = new Vector2(0.25f, 0.15f);
	public int numLevelsPerRow = 2;
	
	void OnGUI() {
		for (int i=0; i<levelDefinitions.Length; i++) {
			int row = i / numLevelsPerRow;
			int col = i % numLevelsPerRow;
			Rect buttonRect = new Rect(firstButtonRect);
			buttonRect.y += row * buttonRectOffsets.y;
			buttonRect.x += col * buttonRectOffsets.x;
			if (GUI.Button(ToScreenRect(buttonRect), levelDefinitions[i].levelName, buttonStyle)) {
				Application.LoadLevel(levelDefinitions[i].sceneName);
			}
		}
	}
	
	private Rect ToScreenRect(Rect relativeRect) {
		return new Rect(relativeRect.xMin * Screen.width, relativeRect.yMin * Screen.height,
			relativeRect.width * Screen.width, relativeRect.height * Screen.height); 
	}
	
	[ContextMenu("Defaultbuttonstyle")]
	void SetDefaultGuiSkin()
	{
		buttonStyle = GUI.skin.button;
	}
}
