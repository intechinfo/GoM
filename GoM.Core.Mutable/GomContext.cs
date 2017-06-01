﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Mutable
{
    public class GoMContext : IGoMContext
    {
        public GoMContext()
        {
        }

        /// <summary>
        /// Creates a Mutable GoMContext from an existing IGoMContext, ie from an Immutable GoMContext
        /// </summary>
        /// <param name="context"></param>
        public GoMContext(IGoMContext context)
        {
            RootPath = context.RootPath;
            Repositories = (List<BasicGitRepository>)context.Repositories;
            Feeds = (List<PackageFeed>)context.Feeds;
        }

        public string RootPath { get; set; }

        public List<BasicGitRepository> Repositories { get; } = new List<BasicGitRepository>();

        public List<PackageFeed> Feeds { get; } = new List<PackageFeed>();

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => Repositories;

        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => Feeds;
    }
}
