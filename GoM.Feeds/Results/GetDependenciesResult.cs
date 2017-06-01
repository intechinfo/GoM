﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GoM.Feeds.Results
{
    public class GetDependenciesResult
    {
        public readonly IEnumerable<TargetResult> Result;

        public readonly Exception Error;

        public bool Success => Result != null;

        public GetDependenciesResult(Exception rE, IEnumerable<TargetResult> r)
        {
            Debug.Assert( (rE == null) != (r == null));
            Result = r;
            Error = rE;
        }
    }
}