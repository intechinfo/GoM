﻿using GoM.Core; using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoM.Core; using System;

namespace GoM.Core.Persistence
{
    interface IPersistence
    {
        /// <summary>
        /// Create new gom project 
        /// in case a project already in current directory tree, retun false & out a path
        /// </summary>
        /// <param name="pathFound">path of existing gom projetc if found</param>
        /// <returns>true if init success</returns>
        bool TryInit ( out string pathFound );

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