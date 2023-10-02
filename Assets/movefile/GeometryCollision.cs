using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeometryCollision
{
    public class Vec2
    {
        public float x, y;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + x + "," + y + ")";
        }

        public float Magnitude()
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        public Vec2 Plus(Vec2 rhs)
        {
            return new Vec2(x + rhs.x, y + rhs.y);
        }

        public void Add(Vec2 rhs)
        {
            x += rhs.x;
            y += rhs.y;
        }

        public Vec2 Minus(Vec2 rhs)
        {
            return new Vec2(x - rhs.x, y - rhs.y);
        }

        public void Subtract(Vec2 rhs)
        {
            x -= rhs.x;
            y -= rhs.y;
        }

        public Vec2 Times(float rhs)
        {
            return new Vec2(x * rhs, y * rhs);
        }

        public void Mul(float rhs)
        {
            x *= rhs;
            y *= rhs;
        }

        public void ClampToLength(float maxL)
        {
            float magnitude = Magnitude();
            if (magnitude > maxL)
            {
                x *= maxL / magnitude;
                y *= maxL / magnitude;
            }
        }

        public void SetToLength(float newL)
        {
            float magnitude = Magnitude();
            x *= newL / magnitude;
            y *= newL / magnitude;
        }

        public void Normalize()
        {
            float magnitude = Magnitude();
            if (magnitude != 0)
            {
                x /= magnitude;
                y /= magnitude;
            }
        }

        public Vec2 Normalized()
        {
            float magnitude = Magnitude();
            return (magnitude != 0) ? new Vec2(x / magnitude, y / magnitude) : new Vec2(0, 0);
        }

        public float DistanceTo(Vec2 rhs)
        {
            float dx = rhs.x - x;
            float dy = rhs.y - y;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }
    }

    public static Vec2 Interpolate(Vec2 a, Vec2 b, float t)
    {
        return a.Plus((b.Minus(a)).Times(t));
    }

    public static float Interpolate(float a, float b, float t)
    {
        return a + ((b - a) * t);
    }

    public static float Dot(Vec2 a, Vec2 b)
    {
        return a.x * b.x + a.y * b.y;
    }

    public class Circle
    {
        public Vec2 center;
        public float radius;
        public GameObject obj; 

        public Circle(float x, float y, float r, GameObject obj = null) 
        {
            this.center = new Vec2(x, y);
            this.radius = r;
            this.obj = obj; 
        }
    }


    public class Box
    {
        public Vec2 center;
        public float width, height;

        public Box(float cx, float cy, float w, float h)
        {
            this.center = new Vec2(cx, cy);
            this.width = w;
            this.height = h;
        }
    }



    public static bool CircleCircleCollision(Circle c1, Circle c2)
    {
        float distance = c1.center.DistanceTo(c2.center);
        float radiusSum = c1.radius + c2.radius;
        return distance <= radiusSum;
    }



    private static float Clamp(float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }

    public static bool CircleBoxCollision(Circle c, Box b)
    {
        Vec2 closest = new Vec2(
            Clamp(c.center.x, b.center.x - b.width / 2, b.center.x + b.width / 2),
            Clamp(c.center.y, b.center.y - b.height / 2, b.center.y + b.height / 2)
        );

        float distance = c.center.DistanceTo(closest);

        return distance <= c.radius;
    }

}


