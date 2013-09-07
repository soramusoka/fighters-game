﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class SparkTile : BaseObject
	{
		public Boolean IsGood { get; set; }
		public int LiveTime { get; set; }
		public FontObject Info { get; set; }

		public SparkTile(Texture2D texture, SpriteFont font, int frameInterval, Boolean isGood)
			: base(texture, frameInterval)
		{
			LiveTime = 0;
			Info = new FontObject(font);
			Info.Color = isGood ? Color.Green : Color.Red;
		}

		public Boolean IsAlive()
		{
			return LiveTime > 0 ? true : false;
		}

		public override void Update(GameTime gameTime)
		{
			Position.Frame.Animate(gameTime, 0, 40, 10);
			_decreaseLive();
			base.Update(gameTime);
		}

		public void SetInfo(int value)
		{
			if (IsGood)
				Info.Text = "+" + value.ToString();
			else
				Info.Text = "-" + value.ToString();
		}

		public void SetPosition(float x, float y)
		{
			Position.MoveTo(x, y);
			Position.Set(x, y);
			Info.SetPosition(x + 70, y + 70);
		}

		protected void _decreaseLive()
		{
			LiveTime -= 1;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Info.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}
