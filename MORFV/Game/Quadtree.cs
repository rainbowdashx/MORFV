using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace MORFV.Game
{


    public class Quadtree
    {

        private int MAX_OBJECTS = 10;
        private int MAX_LEVELS = 5;

        private int level;
        private List<Entity> objects;
        private Rect bounds;
        private Quadtree[] nodes;


        /*
         * Constructor
         */
        public Quadtree(int pLevel, Rect pBounds)
        {
            level = pLevel;
            objects = new List<Entity>();
            bounds = pBounds;
            nodes = new Quadtree[4];
        }

        public void Clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        public void Draw(CanvasDrawingSession canvas)
        {

            canvas.DrawRectangle(this.bounds, this.GetColor());
            for (int j = 0; j <this.objects.Count; j++)
            {
                canvas.DrawCircle(this.objects[j].Location, (float)this.objects[j].Radius, this.GetColor());
            }
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Draw(canvas);
                }

            }
        }


        private void split()
        {
            int subWidth = (int)(bounds.Width / 2);
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;

            nodes[0] = new Quadtree(level + 1, new Rect(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(level + 1, new Rect(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(level + 1, new Rect(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(level + 1, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        public List<Entity> retrieve(List<Entity> returnObjects, Entity item)
        {
            int index = getIndex(item);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].retrieve(returnObjects, item);
            }

            returnObjects.AddRange(objects);

            return returnObjects;
        }

        public void insert(Entity item)
        {
            if (!item.IsCollision)
            {
                return;
            }


            if (nodes[0] != null)
            {
                int index = getIndex(item);

                if (index != -1)
                {
                    nodes[index].insert(item);

                    return;
                }
            }

            objects.Add(item);

            if (objects.Count() > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (nodes[0] == null)
                {
                    split();
                }

                int i = 0;
                while (i < objects.Count())
                {
                    int index = getIndex(objects[i]);
                    if (index != -1)
                    {
                        nodes[index].insert(objects[i]);
                        objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        private int getIndex(Entity item)
        {
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (item.Location.Y - item.Radius < horizontalMidpoint && item.Location.Y + item.Radius < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (item.Location.Y - item.Radius > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (item.Location.X + item.Radius < verticalMidpoint && item.Location.X + item.Radius < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (item.Location.X - item.Radius > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        public Color GetColor()
        {
            Color color;
            switch (level)
            {
                case 0:
                    {
                        color = Colors.White;
                    }
                    break;
                case 1:
                    {
                        color = Colors.Orange;
                    }
                    break;
                case 2:
                    {
                        color = Colors.Blue;
                    }
                    break;
                case 3:
                    {
                        color = Colors.Green;
                    }
                    break;
                case 4:
                    {
                        color = Colors.Yellow;
                    }
                    break;
                case 5:
                    {
                        color = Colors.Pink;
                    }
                    break;
            }

            return color;
        }

    }
}
