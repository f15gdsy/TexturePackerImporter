using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public static class TexturePivotParser {

	[MenuItem("Assets/PivotParser/Process for Pivot")]
	static void ProcessForPivot () {
		TextAsset txt = (TextAsset)Selection.activeObject;

		string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(txt));
		string textureName = GetTextureName(txt.text);
		string texturePath = rootPath + "/" + textureName;

		TextureImporter textureImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;

		if (textureImporter.spriteImportMode.Equals(SpriteImportMode.Multiple)) {
			List<SpriteMetaData> sprites = new List<SpriteMetaData>(textureImporter.spritesheet);
			Dictionary<string, SpriteAlignment> pivotTable = GetPivotsDict(txt.text);

			for (int i=0; i<sprites.Count; i++) {
				SpriteMetaData sprite = sprites[i];
				string spriteName = sprite.name;
				if (pivotTable.ContainsKey(spriteName)) {
					sprite.alignment = (int) pivotTable[spriteName];
					sprites[i] = sprite;

				}
			}
			textureImporter.spritesheet = sprites.ToArray();
			textureImporter.textureType = TextureImporterType.Sprite;
			textureImporter.spriteImportMode =SpriteImportMode.Multiple;
			
			AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate );
		}
		else {
			Debug.LogError("Texture Pivot Parser: the corresponding sprite's Sprite Mode is not Multiple");
		}
	}

	private static Dictionary<string, SpriteAlignment> GetPivotsDict (string text) {
		Dictionary<string, SpriteAlignment> dict = new Dictionary<string, SpriteAlignment>();
		Hashtable table = text.hashtableFromJson();
		Hashtable pivotsTable = (Hashtable) table[("pivots")];

		foreach (string frameName in pivotsTable.Keys) {
			string pivotName = (string) pivotsTable[frameName];

			SpriteAlignment pivot = SpriteAlignment.Center;
	
			if (pivotName.Equals("top left")) {
				pivot = SpriteAlignment.TopLeft;
			}
			else if (pivotName.Equals("top")) {
				pivot = SpriteAlignment.TopCenter;
			}
			else if (pivotName.Equals("top right")) {
				pivot = SpriteAlignment.TopRight;
			}
			else if (pivotName.Equals("right")) {
				pivot = SpriteAlignment.RightCenter;
			}
			else if (pivotName.Equals("bottom right")) {
				pivot = SpriteAlignment.BottomRight;
			}
			else if (pivotName.Equals("bottom")) {
				pivot = SpriteAlignment.BottomCenter;
			}
			else if (pivotName.Equals("bottom left")) {
				pivot = SpriteAlignment.BottomLeft;
			}
			else if (pivotName.Equals("left")) {
				pivot = SpriteAlignment.LeftCenter;
			}
			else if (pivotName.Equals("center")) {
				pivot = SpriteAlignment.Center;
			}
			else {
				Debug.LogError("Texture Pivot Parser: Wrong pivot name!");
			}
			dict.Add(frameName, pivot);
		}
		return dict;
	}

	private static string GetTextureName (string text) {
		Hashtable table = text.hashtableFromJson();
		Hashtable metaTable = (Hashtable) table["meta"];
		return (string) metaTable["image"];
	}
}
