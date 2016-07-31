using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Windows.UI;
using Windows.UI.Xaml;

namespace MORFV.Game
{
    class Laser : Entity
    {
      

        public Laser(Vector2 Location, double Rotation) : base()
        {

            Random rnd = GameInstance.GetInstance().GetRandom();

            this.MaxHealth = 100;
            this.Health = this.MaxHealth;
            this.Radius = 1;
            this.Location = Location;
            this.Rotation = Rotation;
            this.Velocity = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation)) * 15;

            SetLifeSpan(5000);

        }


        public override void Update()
        {
            this.Location += this.Velocity;

         
            base.Update();
        }

        public override void Draw(CanvasDrawingSession canvas)
        {
            canvas.FillCircle(Location, (float)Radius, Colors.Yellow);
        }

        public override void OnCollision(Entity Other)
        {
            base.OnCollision(Other);

            
            if (Other is Asteroid)
            {
                this.IsPendingKill = true;
                Other.TakeDamage(5f);

                Random rnd = GameInstance.GetInstance().GetRandom();

                for (var i = 0; i < 5; i++)
                {

                    var d = this.Location - Other.Location;
                    
                    var ColAngle = Math.Atan2(d.Y, d.X);
                    var rndAngle = (rnd.NextDouble() * (0.8 - (-0.8)) + (-0.8));
                    var rndSpeed = (rnd.NextDouble() * (5 - 2) + 2);
                    
                    var p = new Particle(this.Location,1, ColAngle - rndAngle, rndSpeed,Colors.Cyan);
                    
                }
            }
        }


    }
}
