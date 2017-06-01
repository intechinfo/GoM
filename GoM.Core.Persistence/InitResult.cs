using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Persistence
{
    public class InitResult
    {
        public InitResult(Exception error, List<string> GoMParentPaths, List<string> GoMChildrenPaths)
        {
            if ( GoMParentPaths == null ) throw new ArgumentNullException();
            if ( GoMChildrenPaths == null ) throw new ArgumentNullException();

            this.Error = error;
            this.GoMParentPaths = new List<string>( GoMParentPaths );
            this.GoMChildrenPaths = new List<string>( GoMChildrenPaths );
        }

        public bool Success {
            get { return Error == null && 
                    GoMParentPaths != null && 
                    GoMParentPaths.Count == 0 && 
                    GoMChildrenPaths != null && 
                    GoMChildrenPaths.Count == 0;
            }
        }

        public Exception Error { get; }

        public IReadOnlyCollection<string> GoMParentPaths { get; }

        public IReadOnlyCollection<string> GoMChildrenPaths { get; }
    }

}

