using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Mapping;
using DataLayer.Containers;

namespace TestBDD
{
    class Program
    {
        static void Main(string[] args)
        {
            MappingUsers mappingUsers = new MappingUsers();

            List<User> users = mappingUsers.SelectAll();

            foreach(User user in users)
            {
                Console.WriteLine(user.ToString());
            }

            Console.ReadLine();
        }
    }
}
