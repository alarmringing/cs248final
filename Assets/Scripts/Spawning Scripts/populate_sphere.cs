﻿using UnityEngine;
using System.Collections;


/* This script populates the surface of a sphere with the object
 * attached to the sphere. The number of objects on the planet can be defined.
 * The height and width of the object are randomly changed for each object.
 * The center of the object will be located on the surface of the sphere.
 */

public class populate_sphere : MonoBehaviour {

	public GameObject surface_object;
	public GameObject planet;

	public int object_number = 200;
	private int number_freq = 1024;


	// at run time
	public void Start () {

		// set the frequency bins 
		int freq_offset = number_freq / object_number;
		int freq_start = 0;
		int freq_end = freq_offset - 1;

		// sound is not synchronized for the objects and some still arent moving

		// set default colors
		int color = 0; 
		int numberColors = 5;
		Vector4[]  colors = new Vector4[numberColors];
		colors[0] =  new Vector4 (122/255.0f, 255/255.0f,0f,1f);
		colors[1] =  new Vector4 (31/255.0f, 196/255.0f, 244/255.0f,1f);
		colors[2] =  new Vector4 (0/255.0f, 61/255.0f, 244/255.0f,1f);
		colors[3] =  new Vector4 (45/255.0f, 44/255.0f, 155/255.0f,1f);
		colors[4] =  new Vector4 (0/255.0f, 116/255.0f, 188/255.0f,1f);


		// create object_number of objects
		for (int i = 0; i < object_number; i++) {

			/* CREATE NEW GAME OBJECT ON SURFACE OF SPHERE */

			// determine rotation and position of sphere
			Quaternion spawnRotation = Quaternion.identity;
			Vector3 spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x/2) + surface_object.transform.localScale.y * 0.5f) + planet.transform.position;

			// initatiate the object
			GameObject newObject = Instantiate(surface_object, spawnPosition, spawnRotation) as GameObject;

			// transform the object
			newObject.transform.LookAt(planet.transform);
			newObject.transform.Rotate(-90, 0, 0);

			/* RANDOMIZE THE WIDTH OF THE OBJECT */

			// randomly scale the size of the object
			float width_scale =  Random.Range(-10F, 10F);
			float height_scale = 0; //Random.Range (-1F, 1F);
			newObject.transform.localScale += new Vector3(width_scale, height_scale, width_scale);
			//newBuiding.transform.position -= new Vector3 (0, height_scale+1, 0);


			/* SET THE STRETCH FREQUENCY */

			// set the start and end frequencies for the cube to respond
			if (newObject.GetComponent<CubeStretch> () != null) {
				CubeStretch stretch_component = newObject.GetComponent<CubeStretch>();
				stretch_component.freq_begin = freq_start;
				stretch_component.freq_end = freq_end;
			}

			// increment the freq start and end
			freq_start += freq_offset;
			freq_end += freq_offset;


			/* SET THE COLOR */ 
			Renderer objectRender = newObject.GetComponent<Renderer> ();
			objectRender.material.color = colors[color];

			color++;
			if (color == numberColors -1) color = 0;
		}


	}
		
}