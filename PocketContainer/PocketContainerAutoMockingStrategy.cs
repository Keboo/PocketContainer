﻿// THIS FILE IS NOT INTENDED TO BE EDITED. 
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
#if !RecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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