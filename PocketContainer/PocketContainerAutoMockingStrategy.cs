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
using System.Linq;
using Moq;

namespace Pocket
{
#if !SourceProject
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class PocketContainerAutoMockingStrategy
    {
        public static PocketContainer AutoMockInterfacesAndAbstractClasses(
            this PocketContainer container)
        {
            return container.AddStrategy(type =>
            {
                if (type.IsInterface || type.IsAbstract)
                {
                    var moqType = typeof (Mock<>).MakeGenericType(type);
                    return c =>
                    {
                        var mock = Activator.CreateInstance(moqType) as Mock;
                        mock.DefaultValue = DefaultValue.Mock;
                        return ((dynamic) mock).Object;
                    };
                }
                return null;
            });
        }
    }
}