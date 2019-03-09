using Newtonsoft.Json;
using P1M1.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1M1.Agents
{
    public class DB
    {
        public static string Path;

        private static List<BaseObject> _db;
        private static List<BaseObject> db
        {
            get
            {
                if (_db == null)
                {
                    _db = new List<BaseObject>();

                    FileStream stream = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader reader = new StreamReader(stream);

                    string json = reader.ReadToEnd();

                    reader.Close();

                    if (string.IsNullOrEmpty(json))
                        _db = new List<BaseObject>();
                    else
                        _db = JsonConvert.DeserializeObject<List<BaseObject>>(json);
                }

                return _db;
            }
        }

        private static void Sync()
        {
            string json = JsonConvert.SerializeObject(db);

            FileStream stream = new FileStream(Path, FileMode.Truncate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            
            writer.Write(json);

            writer.Flush();
            writer.Close();
            stream.Close();
        }

        public static BaseObject Find(string id)
        {
            return db.FirstOrDefault(o => o.Id == id);            
        }

        public static void Insert(BaseObject obj)
        {            
            db.Add(obj);

            Sync();
        }

        public static void Delete(string id)
        {
            BaseObject obj = Find(id);

            if (obj != null)
            {
                db.Remove(obj);

                Sync();
            }
        }
    }
}
