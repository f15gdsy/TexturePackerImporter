
There is a tutorial for this plugin here:
http://www.youtube.com/watch?v=CHQmvC1pqaY

http://www.texturepacker.com/
How to import texture sheets from Texture Packer

TexturePacker settings:
	Data Format:  Unity3D   (or JSON Hashtable, then change extension from .json to .txt so Unity picks it up as a text asset)
	Allow rotation is OK
	Everything else at your discretion
	I suggest keeping the output texture's size as Power of 2.
	
	
Unity process:
	Create a folder in your Assets/ directory for your imported sprites.
	Copy the TXT and Image file (PNG, TGA, etc) into that folder.
	Your paths should look something like:
		Assets/MySprite/MySprite.txt
		Assets/MySprite/MySprite.png
		
	
The TexturePivotParser is added by Yifeng, contact: f15gdsy@gmail.com
It's convenient when you need to set some (or all) of the frames in your sprite sheet a different pivot point,
 as TexturePacker doesn't provide that.

TexturePivotParser:
	Create a text file in json format containing the pivot data for the frames (name does not matter).
	Put it in the same folder as the sprite sheet.
	Your paths should look something like:
		Assets/MySprite/MySprite.txt
		Assets/MySprite/MySprite.png
		Assets/MySprite/MySpritePivot.txt
	Right click on the pivot file and choose PivotParser/Process for pivot.
	
	format of the pivot file:
		{
		"meta": {
			"image": "MySprite.png",	
		},
		"pivots": {
			"frame_0.png": "bottom left",
			"frame_1.png": "top left",
			"frame_2.png": "top right",
			"frame_3.png": "center"	
		}
		}
	
	Note: 
		The pivot value is the same as what you can set in the Sprite importer, but in lower case.
		However, the custom type is not supported.
			

	
Shaders:
	Transparent Unlit - 
		The default shader for all imported sprite sheets.
	Opaque Unlit - 
		nontransparent tintable shader great for drawing backgrounds that don't need alpha.  Very efficient.
	Vertex Color - 
		Does not have an inspector-tweakable color property.  All colors must be set by altering the colors[] or colors32[] array of a given mesh.  
		Supports both texture alpha and vertex color alpha.

			
			
	