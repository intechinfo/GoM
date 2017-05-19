﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoM.Core.Immutable.Visitors
{
    public class DetailRepositoryVisitor : Visitor
    {
        private GitRepository _detailed;

        public DetailRepositoryVisitor(GitRepository detailed)
        {
            _detailed = detailed ?? throw new ArgumentNullException(nameof(detailed));
        }

        public override BasicGitRepository Visit(BasicGitRepository basicRepository)
        {
            basicRepository = basicRepository.Details == _detailed ? basicRepository : BasicGitRepository.Create(_detailed);
            return base.Visit(basicRepository);
        }
    }

    public class DetailBranchVisitor : Visitor
    {
        GitBranch _detailed;

        public DetailBranchVisitor(GitBranch detailed)
        {
            _detailed = detailed ?? throw new ArgumentNullException(nameof(detailed));
        }

        public override BasicGitBranch Visit(BasicGitBranch basicBranch)
        {
            basicBranch = basicBranch.Details == _detailed ? basicBranch : BasicGitBranch.Create(_detailed);
            return base.Visit(basicBranch);
        }
    }

    public class UpdateRepositoryFieldsVisitor : Visitor
    {
        // Get target unique path or reference
        // Check if new path is not already taken

        readonly string _path;
        readonly Uri _url;
        readonly BasicGitRepository _target;

        public UpdateRepositoryFieldsVisitor(BasicGitRepository target, string path = null, Uri url = null)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            if (target != null && path == null) throw new ArgumentNullException($"{nameof(path)} and {nameof(url)} cannot both be null");
            _path = path;
            _url = url;
        }

        public override BasicGitRepository Visit(BasicGitRepository basicRepository)
        {
            if(basicRepository == _target)
            {
                string path = _path != null ? (_path != basicRepository.Path ? _path : basicRepository.Path) : basicRepository.Path;
                Uri url = _url != null ? (_url != basicRepository.Url ? _url : basicRepository.Url) : basicRepository.Url;
                basicRepository = basicRepository.Details == null ? BasicGitRepository.Create(path, url)
                    : BasicGitRepository.Create(GitRepository.Create(path, url, basicRepository.Details.Branches));
            }
            return base.Visit(basicRepository);
        }
    }
}
