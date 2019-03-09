using P1M1.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1M1.Abstract
{
    public class BaseObject
    {
        public string Id { get; set; }

        private static int nextHash;
        protected int HashCode;

        protected void GenerateHashCode()
        {
            nextHash++;
            HashCode = nextHash;
        }

        public override int GetHashCode()
        {
            return HashCode;
        }

        public void Save()
        {
            Id = Guid.NewGuid().ToString();

            DB.Insert(this);
        }

        public void Delete()
        {
            DB.Delete(Id);

            Id = null;
        }
    }
}
