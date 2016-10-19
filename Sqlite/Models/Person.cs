using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite.Models
{
  public  class Person 
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        [NotNull]
        public string UserName { get; set; }

        [NotNull]
        public int Age { get; set; }
        public string Address { get; set; }
        [Ignore]
        public string SomeProperty { get; set; }

    }
}
