﻿using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour, IPlayerEventTarget {

	public RectTransform maskRectTransform;
	private Player player;
	private float defaultWidth;
	// Use this for initialization

	public void UpdatePlayer() {
		if(this.player == null) {
			this.player = Player.Instance;
		}
		if(this.defaultWidth <=0 ) {
			this.defaultWidth = maskRectTransform.rect.width;
		}
		if(player.getMaxHealth() >0) {
			RectTransform newMaskRect = maskRectTransform;
			float newWidth = this.defaultWidth * ((float) player.getHealth()/(float) player.getMaxHealth());
			this.maskRectTransform.sizeDelta = new Vector2( newWidth, newMaskRect.sizeDelta.y);
		}
	}
}