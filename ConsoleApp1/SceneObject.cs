﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NewRaylibGame
{
    class SceneObject
    {
        protected SceneObject parent = null;
        protected List<SceneObject> children = new List<SceneObject>();
        public Matrix3 localTransform = new Matrix3();
        public Matrix3 globalTransform = new Matrix3();
        public Matrix3 LocalTransform
        {
            get { return localTransform; }
        }
        public Matrix3 GlobalTransform
        {
            get { return globalTransform; }
        }
        public SceneObject()
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }
            foreach (SceneObject so in children)
            {
                so.parent = null;
            }
        }
        public SceneObject Parent
        {
            get { return parent; }
        }
        public int GetChildCount()
        {
            return children.Count;
        }
        public SceneObject GetChild(int index)
        {
            return children[index];
        }
        public void AddChild(SceneObject child)
        {
            child.parent = this;
            children.Add(child);
        }
        public void RemoveChild(SceneObject child)
        {
            if (children.Remove(child) == true)
            {
                child.parent = null;
            }
        }
        public void UpdateTransform()
        {

            if (parent != null)
            {
                globalTransform = parent.globalTransform * localTransform;
            }
            else
                globalTransform = localTransform;

           foreach (SceneObject child in children)
                child.UpdateTransform();
        }
        public virtual void OnUpdate(float delatTime)
        {

        }
        public virtual void OnDraw()
        {

        }
        public void Update(float deltaTime)
        {
            // run OnUpdate behaviour
            OnUpdate(deltaTime);
            // update children
            foreach (SceneObject child in children)
            {
                child.Update(deltaTime);
            }
        }
        public void Draw()
        {
            // run OnDraw behaviour
            OnDraw();
            // draw children
            foreach (SceneObject child in children)
            {
                child.Draw();
            }
        }
        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x, y);
            UpdateTransform();
        }
        public void SetRotate(float radians)
        {
            localTransform.SetRotateZ(radians);
            UpdateTransform();
        }
        public void SetScale(float width, float height)
        {
            localTransform.SetScaled(width, height, 1);
            UpdateTransform();
        }
        public void Translate(float x, float y)
        {
            localTransform.Translate(x, y);

            UpdateTransform();
        }
        public void Rotate(float radians)
        {
            localTransform.RotateZ(radians);
            UpdateTransform();
        }
        public void Scale(float width, float height)
        {
            localTransform.Scale(width, height, 1);
            UpdateTransform();
        }
    }
}
