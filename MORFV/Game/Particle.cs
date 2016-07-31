using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Windows.UI;

namespace MORFV.Game
{
    class Particle : Entity
    {

        private Color color;


        public Particle(Vector2 Location, double Radius, double Rotation, double speed,Color color):base()
        {

            this.Radius = Radius;
            this.Location = Location;
            this.Rotation = Rotation;
            this.Velocity = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation)) * (float)speed;
            this.color = color;

            this.IsCollision = false;

            SetLifeSpan(700);
        }


        public override void Update()
        {
            this.Location += this.Velocity;
            base.Update();
        }

        public override void Draw(CanvasDrawingSession canvas)
        {
            canvas.FillCircle(this.Location, (float)this.Radius, this.color);

        }
    }
}
