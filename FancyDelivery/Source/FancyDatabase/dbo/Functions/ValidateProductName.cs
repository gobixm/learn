using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString ValidateProductName(SqlString productName)
    {        
        return new SqlString (productName.Value.ToLower().Replace('\'','\"'));
    }
}
