using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//A helper custom rectangle class

    struct Rectangle
    {
        public float X { get; set; }

        public float Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Vector2 Location { 
            get { return new Vector2(X, Y); } 
            set { this.X = value.X; this.Y = value.Y; } 
        }

        public Vector2 Dimensions
        {
            get { return new Vector2(Width, Height); }
            set { this.Width = (int)value.X; this.Height = (int)value.Y; }
        }
        public Rectangle(float x, float y, int width, int height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectangle(Vector2 position, Vector2 dimensions)
        {
            X = position.X;
            Y = position.Y;
            Width = (int)dimensions.X;
            Height = (int)dimensions.Y;
        }

        public Microsoft.Xna.Framework.Rectangle ToMonoRect()
        {
            return new Microsoft.Xna.Framework.Rectangle(Location.ToPoint(), Dimensions.ToPoint());
        }


    }
