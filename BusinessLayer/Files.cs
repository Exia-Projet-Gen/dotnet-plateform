using DataLayer.Containers;
using DataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFInterfaces;

namespace BusinessLayer
{
    public class Files
    {
        private MappingFiles mappingFiles = new MappingFiles();

        public STG GetResult(STG message, string username)
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

            List<File> files = mappingFiles.SelectByUserAndFile(message.data[0].ToString(), username);

            if(files.Count == 0)
            {
                response.info = "Can't find file " + message.data[0].ToString();
                return response;
            }

            File file = files[0];

            response.statut_op = true;
            response.info = file.State;

            if(file.State == "finish")
            {
                response.data = new object[]
                {
                    file.Filename,
                    file.Key,
                    file.Text,
                    file.Percent,
                    file.Email
                };
            }

            return response;
        }

        public void createFileInDatabase(string file, string user)
        {
            if (!mappingFiles.FileExistForUser(file, user))
            {
                mappingFiles.Insert(new File(file, user, "progress"));
            }
            else
            {
                mappingFiles.CleanFile(file, user, "progress");
            }
        }

        public void StoreFileResult(string file, string user, STG message)
        {
            mappingFiles.FinishBF(new File(
                file,
                user,
                "finish",
                message.data[2].ToString(),
                message.data[1].ToString(),
                double.Parse(message.data[3].ToString()),
                message.data[4].ToString()
            ));
        }
    }
}
