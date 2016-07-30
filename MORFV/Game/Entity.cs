using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MORFV.Game
{
    class Entity  : IDisposable
    {

        public Vector2 Location;
        public Vector2 Velocity;
        public double Radius;
        public double Health;
        public double MaxHealth;
        public double Momentum;
        public double Rotation;
        public bool IsPendingKill = false;

        protected readonly double PI2 = Math.PI*2;

        public Entity()
        {

        }
        
        public Entity(Vector2 Location,double Radius, double MaxHealth)
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

        public virtual void Update()
        {


            if (this.Location.X - this.Radius > GameInstance.GetInstance().GetArenaSize().X)
            {
                this.Location.X = (float)-this.Radius;
            }

            if (this.Location.Y - this.Radius > GameInstance.GetInstance().GetArenaSize().Y)
            {
                this.Location.Y = (float)-this.Radius;
            }

            if (this.Location.X + this.Radius < 0)
            {
                this.Location.X = GameInstance.GetInstance().GetArenaSize().X + (float)this.Radius;
            }
            if (this.Location.Y + this.Radius < 0)
            {
                this.Location.Y = GameInstance.GetInstance().GetArenaSize().Y + (float)this.Radius;
            }
        }

        public virtual void Draw(CanvasDrawingSession canvas) {

            canvas.DrawCircle(this.Location, (float)this.Radius, Colors.OrangeRed);
           
        }

        public void Dispose()
        {
            
        }
    }
}
