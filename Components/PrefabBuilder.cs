﻿using References;
using System;
using UnityEngine;

namespace PiTung.Components
{
    /// <summary>
    /// Represents the first step on building a custom component prefab.
    /// </summary>
    public static class PrefabBuilder
    {
        /// <summary>
        /// Returns a cube builder.
        /// </summary>
        public static CubeBuilder Cube => new CubeBuilder();

        /// <summary>
        /// Returns a custom GameObject builder.
        /// </summary>
        /// <param name="root">The function that returns the game object.</param>
        public static CustomBuilder Custom(Func<GameObject> root) => new CustomBuilder(root);
    }
    
    public abstract class Builder
    {
        internal BuildState State = new BuildState();

        public Builder AddComponent<T>()
        {
            State.Components.Add(typeof(T));

            return this;
        }
    }

    public class CustomBuilder : Builder
    {
        internal CustomBuilder(Func<GameObject> root)
        {
            State.Structure = new CustomStructureAtom(root);
        }

        public CustomBuilder AddInput(float x, float y, float z)
        {
            return AddInput(new Vector3(x, y, z));
        }
        public CustomBuilder AddInput(Vector3 position)
        {
            State.Atoms.Add(new InputPegAtom { Position = position });

            return this;
        }

        public CustomBuilder AddOutput(float x, float y, float z)
        {
            return AddOutput(new Vector3(x, y, z));
        }
        public CustomBuilder AddOutput(Vector3 position)
        {
            State.Atoms.Add(new OutputAtom { Position = position });

            return this;
        }
    }

    public class CubeBuilder : Builder
    {
        private IOMapAtom Atom = new IOMapAtom { Map = new IOMap() };

        internal CubeBuilder()
        {
            State.Structure = new CubeStructureAtom();
            State.Atoms.Add(Atom);
        }

        public CubeBuilder SetSide(CubeSide side, SideType what)
        {
            Atom.Map.SetSide(side, what);

            return this;
        }
    }
}
