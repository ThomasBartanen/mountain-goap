﻿// <copyright file="Goal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Represents a goal to be achieved for an agent.
    /// </summary>
    public class Goal {
        /// <summary>
        /// Name of the goal.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Desired world state to be achieved.
        /// </summary>
        internal readonly Dictionary<string, object> DesiredState = new();

        /// <summary>
        /// Weight to give the goal.
        /// </summary>
        internal readonly float Weight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Goal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        /// <param name="desiredState">Desired end state of the goal.</param>
        public Goal(string? name = null, float weight = 1f, Dictionary<string, object>? desiredState = null) {
            Name = name ?? $"Goal {Guid.NewGuid()}";
            Weight = weight;
            if (desiredState != null) DesiredState = desiredState;
        }
    }
}