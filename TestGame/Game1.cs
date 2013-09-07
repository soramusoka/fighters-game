﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TestGame.Domain;
using TestGame.Controllers;
#endregion

namespace TestGame
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		LevelController _level;

		public static ParticleManager<ParticleState> ParticleManager { get; private set; }

		public Game1()
			: base()
		{
			ParticleManager = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.SetResoluton(550, 800, false);
			this.IsMouseVisible = true;

			Window.SetPosition(new Point(200, 100));

			IoC.Init(Content);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_level = IoC.GetSingleton<LevelController>();
			_level.Init("level1");

		}

		protected override void Update(GameTime gameTime)
		{
			_level.Update(gameTime);
			
			//движение по кругу
			//alpha += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			//var	x = 55 + 3 * Math.Sin(alpha);
			//var y = 50 + 3 * Math.Cos(alpha);
			ParticleManager.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================

			_level.Draw(spriteBatch);
			ParticleManager.Draw(spriteBatch);

			//=====================
			spriteBatch.End();
			base.Draw(gameTime);
		}

		public T Get<T>(String path)
		{
			return Content.Load<T>(path);
		}

		#region Init
		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void UnloadContent()
		{
			//
		}

		protected void SetResoluton(int w, int h, Boolean isFullScreen)
		{
			graphics.PreferredBackBufferWidth = w;
			graphics.PreferredBackBufferHeight = h;

			if (graphics.IsFullScreen != isFullScreen)
			{
				graphics.ToggleFullScreen();
			}

			graphics.ApplyChanges();
		}
		#endregion
	}
}
