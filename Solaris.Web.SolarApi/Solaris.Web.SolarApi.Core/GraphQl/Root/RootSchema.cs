﻿using GraphQL;
using GraphQL.Types;

namespace Solaris.Web.SolarApi.Core.GraphQl.Root
{
    public class RootSchema : Schema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            Mutation = resolver.Resolve<RootMutation>();
        }
    }
}
