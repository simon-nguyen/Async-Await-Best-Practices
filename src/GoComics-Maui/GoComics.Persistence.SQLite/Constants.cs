﻿using SQLite;

namespace GoComics.Persistence.SQLite
{
    // All the code in this file is included in all platforms.
    public static class Constants
    {
        public const string DatabaseFileName = "GoComics.db3";
        
        public const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
