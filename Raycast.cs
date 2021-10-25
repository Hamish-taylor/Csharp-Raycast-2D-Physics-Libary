using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Raycast
    {
        public static bool RayVsRect(Vector2 origin, Vector2 direction, Rectangle target, ref Vector2 contact_point, ref Vector2 contact_normal, ref float t_hit_near)
        {

            contact_normal = Vector2.Zero;
            contact_point = Vector2.Zero;

            Vector2 invdir = new Vector2(1 / direction.X, 1 / direction.Y);

            Vector2 t_near = (target.Location - origin) * invdir;// / direction;

            Vector2 t_far = new Vector2((target.X + target.Width - origin.X) / direction.X, (target.Y + target.Height - origin.Y) / direction.Y);


            //Console.WriteLine("Tnear : " + t_near);
            //Console.WriteLine("Tfar : " + t_far);

            if (Double.IsNaN(t_near.X) || Double.IsNaN(t_near.Y)) return false;
            if (Double.IsNaN(t_far.X) || Double.IsNaN(t_far.Y)) return false;



            if (t_near.X > t_far.X) Swap(ref t_near.X, ref t_far.X);
            if (t_near.Y > t_far.Y) Swap(ref t_near.Y, ref t_far.Y);

            if (t_near.X > t_far.Y || t_near.Y > t_far.X) return false;

            t_hit_near = Math.Max(t_near.X, t_near.Y);
            double t_hit_far = Math.Min(t_far.X, t_far.Y);

            //stopping collisions in the negitive direction
            if (t_hit_far < 0) return false;

            contact_point = origin + t_hit_near * direction;

            if (t_near.X > t_near.Y)
                if (direction.X < 0)
                    contact_normal = new Vector2(1, 0);
                else
                    contact_normal = new Vector2(-1, 0);
            else if (t_near.X < t_near.Y)
                if (direction.Y < 0)
                    contact_normal = new Vector2(0, 1);
                else
                    contact_normal = new Vector2(0, -1);

            return true;
        }

        public static bool DynamicRectVsRect(Rectangle main, Vector2 mainVel, Rectangle target, Vector2 targetVel, ref Vector2 contact_point, ref Vector2 contact_normal, ref float contact_time, double gameTime)
        {
            if (mainVel.X == 0 && mainVel.Y == 0)
                return false;

            Rectangle expTarget = new Rectangle();
            expTarget.Location = target.Location - new Vector2(main.Width / 2, main.Height / 2);
            expTarget.Dimensions = target.Dimensions + main.Dimensions;

            if (RayVsRect(main.Location + new Vector2(main.Dimensions.X / 2, main.Dimensions.Y / 2), mainVel * (float)gameTime, expTarget, ref contact_point, ref contact_normal, ref contact_time))
            {
                Console.WriteLine(contact_time);
                return (contact_time >= 0 && contact_time < 1.0f);

            }

            return false;
        }
       static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }

