using GoM.Core; using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.Core.Persistence
{
    public interface IPersistence
    {
        /// <summary>
        /// Create new gom project 
        /// in case a project already in current directory tree, return false & out a path
        /// </summary>
        /// <param name="currentPath">currentPath</param>
        /// <param name="pathFound">path of existing gom project if found</param>
        /// <returns>true if init success</returns>
        bool TryInit ( string currentPath, out string pathFound );

        /// <summary>
        /// Load from .gom folder in given path
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        IGoMContext Load ( string rootPath );

        /// <summary>
        /// Save info in a .gom path
        /// </summary>
        /// <param name="ctx"></param>
        void Save (IGoMContext ctx);


    }
}
