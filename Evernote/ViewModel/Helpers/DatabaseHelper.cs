using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;
using SQLite;

namespace Evernote.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public static bool Insert<T>(T item)
        {
            using var conn = new SQLiteConnection(dbFile);
            conn.CreateTable<T>();
            var rows = conn.Insert(item);
            return rows > 0;
        }

        public static bool Update<T>(T item)
        {
            using var conn = new SQLiteConnection(dbFile);
            conn.CreateTable<T>();
            var rows = conn.Update(item);
            return rows > 0;
        }

        public static bool Delete<T>(T item)
        {
            using var conn = new SQLiteConnection(dbFile);
            conn.CreateTable<T>();
            var rows = conn.Delete(item);
            return rows > 0;
        }

        public static List<T> Read<T>() where T : new()
        {
            using var conn = new SQLiteConnection(dbFile);
            return conn.Table<T>().ToList();
        }
    }
}
