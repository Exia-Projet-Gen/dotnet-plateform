using DataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Containers;
using WCFInterfaces;

namespace BusinessLayer
{
    public class Authentication
    {
        private MappingUsers mappingUsers;

        public Authentication()
        {
            mappingUsers = new MappingUsers();
        }

        public STG Signup(STG message)
        {
            string username = message.data[0].ToString();
            string email = message.data[0].ToString();
            string password = message.data[0].ToString();

            STG response = new STG()
            {
                statut_op = false,
                info = "",
                data = new object[0] { },
                operationname = message.operationname,
                tokenApp = "",
                tokenUser = message.tokenUser
            };

            // Verify opetion name
            if (message.operationname != "signup")
            {
                response.info = "Wrong operation name";
                return response;
            }

            // Check if email is taken
            if (mappingUsers.EmailExist(email))
            {
                response.info = "Email already exist";
                return response;
            }

            // Check if username is taken
            if (mappingUsers.UsernameExist(username))
            {
                response.info = "Username already exist";
                return response;
            }

            string hash = Hasher.Hash(password);

            User user = new User(username, email, hash);

            if(mappingUsers.Insert(user) == 0)
            {
                response.info = "Error while creating user";
                return response;
            }

            response.statut_op = true;
            response.info = "registered";
            return response;
        }

        public STG Login(STG message)
        {
            STG response = new STG()
            {
                statut_op = false,
                info = "",
                data = new object[0] { },
                operationname = message.operationname,
                tokenApp = "",
                tokenUser = message.tokenUser
            };

            // Verify opetion name
            if(message.operationname != "login")
            {
                response.info = "Wrong operation name";
                return response;
            }

            // Get User
            List<User> users = mappingUsers.SelectByUsername(message.data[0].ToString());

            // Can't find user
            if (users.Count == 0)
            {
                response.info = "Can't find user";
                return response;
            }

            User user = users[0];

            // Wrong password
            if (!Hasher.Verify(message.data[1].ToString(), user.Password))
            {
                response.statut_op = false;
                response.info = "Wrong password";
                return response;
            }

            string tokenUser = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            // Put new tokenUser in database
            if (mappingUsers.UpdateTokenUser(user.ID, tokenUser) == 0)
            {
                response.info = "Error while updating user";
                return response;
            }

            // Return message with the tokenUser
            response.statut_op = true;
            response.info = "logged";
            response.tokenUser = tokenUser;

            return response;
        }
    }
}
