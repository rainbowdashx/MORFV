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
    class Laser:Entity
    {
        private DispatcherTimer dispatcherTimer;

        public Laser(Vector2 Location,double Rotation):base()
        {

            Random rnd = GameInstance.GetInstance().GetRandom();

            this.MaxHealth = 100;
            this.Health = this.MaxHealth;
            this.Radius = 5;
            this.Location = Location;
            this.Rotation = Rotation;
            this.Velocity = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation)) * 15;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Laser_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }


        public override void Update()
        {
            this.Location += this.Velocity;
            base.Update();
        }

        public override void Draw(CanvasDrawingSession canvas)
        {
            canvas.FillCircle(Location, (float)Radius, Colors.Cyan);
        }

        private void Laser_Tick(object sender, object e)
        {
            dispatcherTimer.Stop();
            IsPendingKill = true;
            
        }
    }
}
