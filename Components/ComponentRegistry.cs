﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PiTung.Components
{
    public static class ComponentRegistry
    {
        internal static IDictionary<string, CustomComponent> Registry = new Dictionary<string, CustomComponent>();

        public static CustomComponent CreateNew<THandler>(string name, Builder builder) where THandler : UpdateHandler
        {
            if (Registry.TryGetValue(name, out var i) && i == null)
                Registry.Remove(name);

            var comp = new CustomComponent<THandler>(name, builder.State);
            Registry.Add(name, comp);

            return comp;
        }
    }
}
