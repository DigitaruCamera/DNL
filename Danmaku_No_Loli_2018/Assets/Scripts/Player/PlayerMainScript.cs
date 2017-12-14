﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainScript : MonoBehaviour
	{
		public float speed = 11;
		public float focus = 3;
		public GameObject bullet;
		public GameObject bomb;
		public bool tire = false;
		double current_time;
		double delayed_time;
		public double delay = 25;
		double bomb_bar = 1;
		float angle = 0.05f;


		IEnumerator Start ()
		{
			while (true) {
				if (tire == true){
					Instantiate (bullet, new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z), transform.rotation);
					Instantiate (bullet, new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z), transform.rotation);
					Instantiate (bullet, new Vector3(transform.position.x + 0.6f, transform.position.y - 0.4f, transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - angle, transform.rotation.w));
					Instantiate (bullet, new Vector3(transform.position.x + 1f, transform.position.y - 0.5f, transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - angle, transform.rotation.w));
					Instantiate (bullet, new Vector3(transform.position.x - 0.6f, transform.position.y - 0.4f, transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + angle, transform.rotation.w));
					Instantiate (bullet, new Vector3(transform.position.x - 1f, transform.position.y - 0.5f, transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + angle, transform.rotation.w));
					//	Instantiate (bullet, transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - angle, transform.rotation.w));
					yield return new WaitForSeconds (0.05f);
				}
				else {
					yield return new WaitForSeconds (0.01f);
				}
			}
		}

		void mouvement()
		{
			//=======version souris=========
			var h = speed * Input.GetAxis ("Mouse X");
			transform.Translate(h/50, 0, 0);
			var l = speed * Input.GetAxis ("Mouse Y");
			transform.Translate(0, l/50, 0);
			//==============================
			//==version clavier et manette==
			float x = Input.GetAxisRaw ("Horizontal");
			float y = Input.GetAxisRaw ("Vertical");
			Vector2 direction = new Vector2 (x, y).normalized;
			GetComponent<Rigidbody2D>().velocity = direction * speed;
			//==============================
		}

		void action()
		{
			//============TIRE==============
			if (Input.GetButtonDown("Fire1"))
				tire = !tire;
			if (Input.GetButtonUp("Fire1"))
				tire = !tire;
			bomb_bar += Time.deltaTime / delay;
			//==============================
			//=============FOCUS============
			if (Input.GetButtonDown("Fire3")) {
				speed = speed / focus;
				angle = -0.05f;
			}
			if (Input.GetButtonUp("Fire3")) {
				speed = speed * focus;
				angle = 0.05f;
			}
			//==============================
			//============BOMB==============
//			GameObject.Find ("Image_bombprogress_fill").GetComponent <Image>().fillAmount = (float)bomb_bar;
			if (current_time >= delayed_time && Input.GetButtonDown("Fire2"))
			{
//				GameObject.Find ("game_controller").GetComponent<ui_score>().score -= 10;
				Instantiate (bomb, transform.position, transform.rotation);
				delayed_time = current_time + delay;
				bomb_bar = 0;
			}
			//==============================
		}

		void Update ()
		{
			current_time += Time.deltaTime;
			mouvement ();
			action ();
		}


		void OnTriggerEnter2D(Collider2D coll) {
			if (coll.gameObject.tag == "Enemy") {
				Destroy (gameObject);
			}
			if (coll.gameObject.tag == "Enemybullet") {
				Destroy (gameObject);
			}
		}

	}