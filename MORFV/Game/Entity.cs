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
    public class Entity  : IDisposable
    {

        public Vector2 Location;
        public Vector2 Velocity;
        public double Radius;
        public double Health;
        public double MaxHealth;
        public double Momentum;
        public double Rotation;
        public bool IsPendingKill = false;


        

        private DateTime lifeStart;
        private float lifeSpan;

        public bool IsCollision=true;



        protected readonly double PI2 = Math.PI*2;

        public Entity()
        {
            GameInstance.GetInstance().entities.Add(this);
            lifeStart = DateTime.Now;
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

            if (lifeSpan > 0 && (DateTime.Now - lifeStart).TotalMilliseconds > lifeSpan)
            {
                IsPendingKill = true;
            }
        }

        public virtual void Draw(CanvasDrawingSession canvas) {

            canvas.DrawCircle(this.Location, (float)this.Radius, Colors.OrangeRed);
           
        }

        public void SetLifeSpan(float value)
        {
            lifeSpan = value;
            lifeStart = DateTime.Now;
        }

        public void Dispose()
        {
            GameInstance.GetInstance().entities.Remove(this);
        }

        public virtual void OnCollision(Entity Other)
        {

        } 

        public virtual void TakeDamage(float amount)
        {

        }
        
        public bool CheckCollision(Entity Other)
        {

            if (this.Equals(Other))
            {
                return false;
            }

            
            if ((Other.Location - this.Location).Length() < (float)(this.Radius + Other.Radius))
            {
                this.OnCollision(Other);
                return true;
            }
            return false;
        }
    }
}
