using Dapper;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Workwise.Data
{
    public class BaseRepository
    {
        public async Task<T> QueryData<T>(string spName, DynamicParameters parameter)
        {
            using (var Connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Workwise;Integrated Security=True"))
            {
                parameter.Add("@Op_StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameter.Add("@Op_Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                var result = await Connection.QueryAsync<string>(sql: spName, parameter, commandType: CommandType.StoredProcedure);

                var fullResult = string.Concat(result);

                var Status = parameter.Get<String>("@Op_Status");
                var StatusCode = parameter.Get<int>("@Op_StatusCode");

                if (StatusCode == 0 && fullResult != null)
                {
                    //Log.Information($"result Status {Status} , StatusCode : {StatusCode} return : {fullResult}");
                    return JsonConvert.DeserializeObject<T>(fullResult.ToString());

                }
                else
                {
                    StatusCode = 1;
                    //Log.Error($"result Status : {Status} , StatusCode : {StatusCode} return : {fullResult}");
                    Exception exception = new Exception(Status);
                    throw exception;
                }
            }

        }
    }
}