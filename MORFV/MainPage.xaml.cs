using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using MORFV.Game;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409



namespace MORFV
{

  

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private bool bKeyDown = false;
        private Player player  = new Player(new Vector2(0, 0), 100);
      

      
        

        public MainPage()
        {
            this.InitializeComponent();
            Random rnd = GameInstance.GetInstance().GetRandom();

            GameInstance.GetInstance().crosshair= new Entity();
            GameInstance.GetInstance().crosshair.Radius = 10;


            for (int i = 0; i < 100; i++)
            {

                double x = rnd.NextDouble() * canvas.ActualWidth;
                double y = rnd.NextDouble() * canvas.ActualHeight;

                Asteroid monster = new Asteroid(new Vector2((float)x, (float)y), (rnd.NextDouble()*50)+50, 100);

            }


            UpdateArenaSize();

        }


        private void canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {

        }


        private void canvas_DrawAnimated(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {

            args.DrawingSession.Clear(Colors.Black);

            foreach (var item in GameInstance.GetInstance().entities.ToList())
            {
                if (!item.IsPendingKill)
                {
                    item.Update();
                    item.Draw(args.DrawingSession);
                }
                else
                {
                    item.Dispose();
                }
            }

            player.Update();
            player.Draw(args.DrawingSession);

            GameInstance.GetInstance().crosshair.Draw(args.DrawingSession);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }

        
        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {

           GameInstance.GetInstance().crosshair.Location = e.GetCurrentPoint(canvas).Position.ToVector2();

        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(canvas);
                if (ptrPt.Properties.IsLeftButtonPressed)
                {
                    GameInstance.GetInstance().bLeftKeyDown = true;
                    
                }
                if (ptrPt.Properties.IsRightButtonPressed)
                {
                    GameInstance.GetInstance().bRightKeyDown = true;
                }
            }
        }

        private void Grid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                GameInstance.GetInstance().bRightKeyDown = false;
                GameInstance.GetInstance().bLeftKeyDown = false;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateArenaSize();
        }


        private void UpdateArenaSize()
        {
            GameInstance.GetInstance().SetArenaSize(new Vector2((float)canvas.ActualWidth, (float)canvas.ActualHeight));
        }
    }
}
