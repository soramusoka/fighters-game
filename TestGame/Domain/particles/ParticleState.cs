﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public enum ParticleType { None, Enemy, Bullet, IgnoreGravity }

	public struct ParticleState
	{
		public Vector2 Velocity;
		public ParticleType Type;
		public float LengthMultiplier;

		public static void UpdateParticle(ParticleManager<ParticleState>.Particle particle)
		{
			var vel = particle.State.Velocity;
			float speed = vel.Length();

			Vector2.Add(ref particle.Position, ref vel, out particle.Position);

			float alpha = Math.Min(1, Math.Min(particle.PercentLife * 2, speed * 1f));
			alpha *= alpha;

			particle.Color.A = (byte)(255 * alpha);

			if (particle.State.Type == ParticleType.Bullet)
				particle.Scale.X = particle.State.LengthMultiplier * Math.Min(Math.Min(1f, 0.1f * speed + 0.1f), alpha);
			else
				particle.Scale.X = particle.State.LengthMultiplier * Math.Min(Math.Min(1f, 0.2f * speed + 0.1f), alpha);

			particle.Orientation = vel.ToAngle();

			var pos = particle.Position;
			int width = 550;
			int height = 800;

			if (pos.X < 0)
				vel.X = Math.Abs(vel.X);
			else if (pos.X > width)
				vel.X = -Math.Abs(vel.X);
			if (pos.Y < 0)
				vel.Y = Math.Abs(vel.Y);
			else if (pos.Y > height)
				vel.Y = -Math.Abs(vel.Y);

			if (particle.State.Type != ParticleType.IgnoreGravity)
			{
				var dPos = new Vector2(300, 500);
				float distance = dPos.Length();
				var n = dPos / distance;
				vel += 10000 * n / (distance * distance + 10000);

				if (distance < 400)
					vel += 45 * new Vector2(n.Y, -n.X) / (distance + 100);
			}

			if (Math.Abs(vel.X) + Math.Abs(vel.Y) < 0.00000000001f)
				vel = Vector2.Zero;
			else if (particle.State.Type == ParticleType.Enemy)
				vel *= 0.94f;
			else
				vel *= 0.96f + Math.Abs(pos.X) % 0.04f;

			particle.State.Velocity = vel;
		}
	}
}
