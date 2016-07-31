using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System.Numerics;
using Windows.UI;

namespace MORFV.Game
{
    class Asteroid : Entity
    {

        public Asteroid(Vector2 Location, double Radius, double MaxHealth):base()
        {
            Random rnd = GameInstance.GetInstance().GetRandom();

            this.MaxHealth = MaxHealth;
            this.Health = this.MaxHealth;
            this.Radius = Radius;
            this.Location = Location;
            this.Momentum = this.Radius * 0.002f;
            this.Rotation = rnd.NextDouble() * (Math.PI * 2);
            this.Velocity = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation)) * 1;

        }

        public override void Update()
        {
            this.Location += this.Velocity;
            this.Rotation += this.Momentum * 0.16;

            base.Update();


        }

        public override void Draw(CanvasDrawingSession canvas)
        {
            if (this.Radius > 25)
            {
                var pathBuilder = new CanvasPathBuilder(canvas);
                pathBuilder.BeginFigure(new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation)) * (float)this.Radius);
                for (int i = 0; i < 12; i++)
                {
                    pathBuilder.AddLine(new Vector2((float)Math.Cos((PI2 / 12 * i) + this.Rotation), (float)Math.Sin((PI2 / 12 * i) + this.Rotation)) * (float)this.Radius);
                }
                pathBuilder.EndFigure(CanvasFigureLoop.Closed);
                var geometry = CanvasGeometry.CreatePath(pathBuilder);
                canvas.DrawGeometry(geometry, this.Location, Colors.Red);
            }
            else
            {
                var pathBuilder = new CanvasPathBuilder(canvas);
                pathBuilder.BeginFigure(new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation)) * (float)this.Radius);
                for (int i = 0; i < 6; i++)
                {
                    pathBuilder.AddLine(new Vector2((float)Math.Cos((PI2 / 6 * i) + this.Rotation), (float)Math.Sin((PI2 / 6 * i) + this.Rotation)) * (float)this.Radius);
                }
                pathBuilder.EndFigure(CanvasFigureLoop.Closed);
                var geometry = CanvasGeometry.CreatePath(pathBuilder);
                canvas.DrawGeometry(geometry, this.Location, Colors.Red);
            }
        }

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);

            Health -= amount;
            if (Health <= 0)
            {
                IsPendingKill = true;
            }
        }

    }
}
