﻿using Microsoft.Extensions.Logging;
using System;

namespace BookClub.Entities
{
    public static class LoggerDefines
    {
        private static readonly Action<ILogger, Exception> _repoGetBooks;
        private static readonly Action<ILogger, string, Exception> _repoCallGetManyProc;
        
        static LoggerDefines()
        {
            _repoGetBooks = LoggerMessage.Define(LogLevel.Information, 0, 
                "Inside the repository about to call GetAllBooks.");

            _repoCallGetManyProc = LoggerMessage.Define<string>(LogLevel.Debug, DataEvents.GetMany, 
                "Debugging information for stored proc: {ProcName}");            
        }

        public static void RepoGetBooks(this ILogger logger)
        {
            _repoGetBooks(logger, null);
        }

        public static void RepoCallGetMany(this ILogger logger, string procName)
        {
            _repoCallGetManyProc(logger, procName, null);
        }        
    }
}
