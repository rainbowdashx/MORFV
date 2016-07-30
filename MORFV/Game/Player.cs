using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Windows.UI;

namespace MORFV.Game
{
    class Player : Entity
    {

        private double crossAngle;
        private double agility=1;

        public Player(Vector2 Location,double MaxHealth):base(Location,25,MaxHealth)
        {

        }

        public override void Update()
        {

            Vector2 diffVec = GameInstance.GetInstance().crosshair.Location - this.Location;

            //TO RADIANS
            this.Rotation = this.Rotation * 180 / Math.PI;

            this.crossAngle = Math.Atan2(diffVec.Y, diffVec.X);
            this.crossAngle = this.crossAngle * 180 / Math.PI;
            if (this.crossAngle < 0) this.crossAngle += 360;

            double da = this.crossAngle - this.Rotation;  //Dif Angle
            if (da < 0) da += 360;
            if (da > 180) da = -180 - (da - 180);

            if (Math.Abs(da) > this.agility)
            {
                if (da >= 0) this.Rotation += this.agility;
                if (da < 0) this.Rotation -= this.agility;
            }
            else
            {
                this.Rotation = this.crossAngle;
            }

            if (this.Rotation > 360) this.Rotation = 0;
            if (this.Rotation < 0) this.Rotation = 360;


            //BACK TO RADIANS
            this.Rotation = this.Rotation * Math.PI / 180;

            if (GameInstance.GetInstance().bRightKeyDown)
            {
                this.Velocity = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation)) * 1;
                this.Location += this.Velocity;
            }
        }

        public override void Draw(CanvasDrawingSession canvas)
        {

            

            var pathBuilder = new CanvasPathBuilder(canvas);

            
            Vector2 p1 = Vector2.Transform(new Vector2((float)this.Radius,0), Matrix3x2.CreateRotation((float)this.Rotation));
            Vector2 p2 = Vector2.Transform(new Vector2((float)-this.Radius, (float)-this.Radius), Matrix3x2.CreateRotation((float)this.Rotation));
            Vector2 p3 = Vector2.Transform(new Vector2((float)-this.Radius, (float)this.Radius), Matrix3x2.CreateRotation((float)this.Rotation));

            pathBuilder.BeginFigure(p1);
            pathBuilder.AddLine(p2);
            pathBuilder.AddLine(p3);
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);


            var geometry = CanvasGeometry.CreatePath(pathBuilder);
            

            
            canvas.DrawGeometry(geometry, this.Location, Colors.Green);
            
        }

    }
}
