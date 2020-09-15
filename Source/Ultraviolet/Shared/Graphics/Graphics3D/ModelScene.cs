﻿using System;
using System.Collections.Generic;
using Ultraviolet.Core;

namespace Ultraviolet.Graphics.Graphics3D
{
    /// <summary>
    /// Represents a logically related collection of meshes within a model.
    /// </summary>
    public class ModelScene
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelScene"/> class.
        /// </summary>
        /// <param name="logicalIndex">The logical index of the scene within its parent model.</param>
        /// <param name="name">The scene's name.</param>
        /// <param name="nodes">The scene's list of nodes.</param>
        public ModelScene(Int32 logicalIndex, String name, IList<ModelNode> nodes = null)
        {
            this.LogicalIndex = logicalIndex;
            this.Name = name;
            this.Nodes = new ModelNodeCollection(nodes);

            foreach (var node in Nodes)
                node.SetParentModelScene(this);
        }

        /// <summary>
        /// Gets the logical index of the scene within its parent model.
        /// </summary>
        public Int32 LogicalIndex { get; }

        /// <summary>
        /// Gets the scene's name.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the <see cref="Model"/> that contains this scene.
        /// </summary>
        public Model ParentModel { get; private set; }

        /// <summary>
        /// Gets the scene's collection of nodes.
        /// </summary>
        public ModelNodeCollection Nodes { get; }

        /// <summary>
        /// Sets the scene's parent model.
        /// </summary>
        /// <param name="parent">The scene's parent model.</param>
        internal void SetParentModel(Model parent)
        {
            Contract.Require(parent, nameof(parent));

            if (this.ParentModel != null)
                throw new InvalidOperationException(UltravioletStrings.ModelParentLinkAlreadyExists);

            this.ParentModel = parent;

            foreach (var node in Nodes)
                node.SetParentModel(parent);
        }
    }
}