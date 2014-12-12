﻿// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// THIS FILE IS NOT INTENDED TO BE EDITED. 
// 
// It has been imported using NuGet from the PocketContainer project (https://github.com/jonsequitur/PocketContainer). 
// 
// This file can be updated in-place using the Package Manager Console. To check for updates, run the following command:
// 
// PM> Get-Package -Updates

using System;
using System.Collections.Concurrent;

namespace Pocket
{
    internal partial class PocketContainer
    {
        /// <summary>
        /// Clones the container, allowing for selectively overriding registrations.
        /// </summary>
        public PocketContainer CreateOverrideContainer()
        {
            var fallback = this;

            var child = new PocketContainer
            {
                resolvers = new ConcurrentDictionary<Type, Func<PocketContainer, object>>(resolvers),
                strategyChain = strategyChain
            };

            return child.AddStrategy(t =>
            {
                // if the parent already has a registation, use it
                Func<PocketContainer, object> resolver;

                if (fallback.resolvers.TryGetValue(t, out resolver))
                {
                    return resolver;
                }

                return null;
            });
        }
    }
}