﻿using References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PiTung.Components
{
    internal interface IAtom
    {
        void AddToGameObject(GameObject obj);
    }
    
    internal sealed class InputPegAtom : IAtom
    {
        public Vector3 Position;

        public void AddToGameObject(GameObject obj)
        {
            BuilderUtils.AddInputPeg(obj, Position);
        }
    }
    internal sealed class OutputAtom : IAtom
    {
        public Vector3 Position;

        public void AddToGameObject(GameObject obj)
        {
            BuilderUtils.AddOutputPeg(obj, Position);
        }
    }
    internal sealed class IOMapAtom : IAtom
    {
        public IOMap Map { get; set; }

        public void AddToGameObject(GameObject obj)
        {
            BuilderUtils.ApplyIOMap(obj, Map);
        }
    }

    internal interface IStructureAtom
    {
        GameObject GetRootObject();
    }

    internal sealed class CubeStructureAtom : IStructureAtom
    {
        public GameObject GetRootObject()
        {
            return GameObject.Instantiate(Prefabs.WhiteCube, new Vector3(-1000, -1000, -1000), Quaternion.identity);
        }
    }
    internal sealed class CustomStructureAtom : IStructureAtom
    {
        public Func<GameObject> Root { get; }

        public CustomStructureAtom(Func<GameObject> root)
        {
            this.Root = root;
        }

        public GameObject GetRootObject()
        {
            return this.Root();
        }
    }

    internal class BuildState
    {
        internal IList<IAtom> Atoms = new List<IAtom>();
        internal IList<Type> Components = new List<Type>();
        internal IStructureAtom Structure;

        internal GameObject BuildResult()
        {
            GameObject root = Structure.GetRootObject();

            foreach (var item in Atoms)
            {
                item.AddToGameObject(root);
            }

            foreach (var item in Components)
            {
                root.AddComponent(item);
            }

            return root;
        }
    }
}
